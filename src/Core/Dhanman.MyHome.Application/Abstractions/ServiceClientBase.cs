using Microsoft.AspNetCore.Http;

namespace Dhanman.MyHome.Application.Abstractions;
 
public class ServiceClientBase
{
    #region Properties
    private readonly IHttpContextAccessor _httpContextAccessor;
    #endregion

    public ServiceClientBase(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    #region Methods
    public string GetBearerToken()
    {
        var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();

        if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
        {
            return authorizationHeader.Substring("Bearer ".Length).Trim();
        }

        return null;
    }
    #endregion
}