using Dhanman.MyHome.Application.Abstractions;
using Dhanman.MyHome.Application.Enums;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Dhanman.MyHome.Application.Services;

public class FirebaseNotificationService : IFirebaseService
{
    private readonly HttpClient _httpClient;
    private readonly FirebaseAuth _firebaseAuth;

    public FirebaseNotificationService(HttpClient httpClient, FirebaseAuth firebaseAuth)
    {
        _httpClient = httpClient;
        _firebaseAuth = firebaseAuth;
    }

    /// <summary>
    /// Sends a standardized notification message to a device.
    /// </summary>
    /// <param name="fcmToken">The device's FCM token.</param>
    /// <param name="type">The type of notification.</param>
    /// <param name="title">Notification title.</param>
    /// <param name="body">Notification body.</param>
    /// <param name="payload">Strongly-typed payload object, serialized to JSON.</param>
    /// <param name="version">Envelope version, default is "1.0".</param>
    public async Task SendNotificationAsync(
    IEnumerable<string> fcmTokens,
        FirebaseMessageType type,
        string title,
        string body,
        object payload = null,
        string version = "1.0"
    )
    {
        var accessToken = await _firebaseAuth.GetAccessTokenAsync();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        // Envelope with type, version, and payload
        var messageEnvelope = new Dictionary<string, object>
        {
            ["type"] = type.ToString(),
            ["version"] = version,
            ["title"] = title,
            ["body"] = body,
            //      ["payload"] = payload ?? new { }
        };

        // Flatten payload object -> string values
        if (payload != null)
        {
            foreach (var prop in payload.GetType().GetProperties())
            {
                var value = prop.GetValue(payload)?.ToString() ?? string.Empty;
                messageEnvelope[prop.Name] = value;
            }
        }

        var errors = new List<string>();

        foreach (var fcmToken in fcmTokens)
        {
            var message = new
            {
                message = new
                {
                    token = fcmToken,
                    notification = new { title, body },
                    data = messageEnvelope
                }
            };

            var json = JsonSerializer.Serialize(message);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://fcm.googleapis.com/v1/projects/dwarpal-5c7d6/messages:send", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                errors.Add($"Token: {fcmToken}, Error: {error}");
            }
        }

        if (errors.Any())
        {
            // You can also join with newline for readability
            throw new Exception($"FCM Errors:\n{string.Join("\n", errors)}");
        }
    }
}