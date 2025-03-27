using Dhanman.MyHome.Application.Abstractions;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Dhanman.MyHome.Application.Services;

public class FirebaseNotificationService : IFirebaseService
{
    #region Properties
    private readonly HttpClient _httpClient;
    private readonly FirebaseAuth _firebaseAuth;
    #endregion

    #region Constructer
    public FirebaseNotificationService(HttpClient httpClient, FirebaseAuth firebaseAuth)
    {
        _httpClient = httpClient;
        _firebaseAuth = firebaseAuth;
    }
    #endregion

    #region Methods
    public async Task SendNotificationAsync(string fcmToken, string title, string body, object data = null)
    {
        var accessToken = await _firebaseAuth.GetAccessTokenAsync();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var messageData = new Dictionary<string, object>();

        if (data != null)
        {
            // Check if it's already a dictionary
            if (data is IDictionary<string, object> dictionaryData)
            {
                messageData = dictionaryData.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            }
            else
            {
                // Handle other data types, e.g., cast them to string if necessary
                messageData["guestId"] = data.ToString();  // Ensure all data is serialized as a string
            }
        }

        var message = new
        {
            message = new
            {
                token = fcmToken,
                notification = new { title, body },
                data = messageData
            }
        };

        var json = JsonSerializer.Serialize(message);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("https://fcm.googleapis.com/v1/projects/dwarpal-5c7d6/messages:send", content);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"FCM Error: {error}");
        }
    }
    #endregion

}
