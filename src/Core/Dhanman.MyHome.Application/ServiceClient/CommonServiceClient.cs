﻿using Dhanman.MyHome.Application.Abstractions;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Dhanman.MyHome.Application.ServiceClient;
 
public class CommonServiceClient : ServiceClientBase, ICommonServiceClient
{
    #region Properties
    private readonly HttpClient _httpClient;
    private readonly string _commonBaseUrl;

    #endregion

    #region Constructors
    public CommonServiceClient(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        _httpClient = httpClient;
        if (configuration != null && !String.IsNullOrEmpty(configuration["ApiSettings:CommonServiceBaseAddress"]))
        {
            _commonBaseUrl = configuration["ApiSettings:CommonServiceBaseAddress"];
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }
        else
        {
            _commonBaseUrl = string.Empty;
        }
    }
    #endregion

    #region Methods
    private async Task<string> SendHttpRequestAsync(HttpMethod method, string url, string content)
    {
        HttpRequestMessage request = new HttpRequestMessage(method, url);
        request.Headers.Add("accept", "text/plain");

        var token = GetBearerToken();
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        request.Content = new StringContent(content, Encoding.UTF8, "application/json");
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        try
        {
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException e)
        {
            // Log the error with detailed information
            Console.WriteLine($"Request URL: {url}");
            Console.WriteLine($"Request Content: {content}");
            Console.WriteLine($"HTTP Request Exception: {e.Message}");
            throw;
        }
    }

    public async Task<string> GetDataFromCommonServiceAsync(string endpoint)
    {
        var token = GetBearerToken();
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> CreateCustomerAsync(CustomerDto customer)
    {
        var jsonObject = new
        {
            customerId = customer.Id,
            name = customer.Name,
            companyId = customer.CompanyId,
            createdBy = customer.CreatedBy,
        };

        string json = JsonConvert.SerializeObject(jsonObject);
        string fullUrl = $"{_commonBaseUrl}{ApiClientRoutes.Customers.CreateCommonCustomer}";

        // Log the request
        Console.WriteLine($"Request URL: {fullUrl}");
        Console.WriteLine($"Request Content: {json}");
        var response = await SendHttpRequestAsync(HttpMethod.Post, fullUrl, json);

        // Log the response
        Console.WriteLine($"Response: {response}");

        return response;
    }

    public async Task<string> CreateUserAsync(UserDto user)
    {
        var jsonObject = new
        {
            userId = user.Id,
            companyId = user.CompanyId,
            firstName = user.FirstName,
            lastName = user.LastName,
            email = user.Email,
            phoneNumber = user.PhoneNumber,
        };

        string json = JsonConvert.SerializeObject(jsonObject);
        string fullUrl = $"{_commonBaseUrl}{ApiClientRoutes.User.CreateUser}";

        // Log the request
        Console.WriteLine($"Request URL: {fullUrl}");
        Console.WriteLine($"Request Content: {json}");
        var response = await SendHttpRequestAsync(HttpMethod.Post, fullUrl, json);

        // Log the response
        Console.WriteLine($"Response: {response}");

        return response;
    }

    #endregion

}