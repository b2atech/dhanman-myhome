using Dhanman.MyHome.Application.Enums;

namespace Dhanman.MyHome.Application.Abstractions;
public interface IFirebaseService
{
    /// <summary>
    /// Sends a standardized notification message to a device.
    /// </summary>
    /// <param name="fcmToken">The device's FCM token.</param>
    /// <param name="type">The type of notification (enum).</param>
    /// <param name="title">Notification title.</param>
    /// <param name="body">Notification body.</param>
    /// <param name="payload">Payload object, serialized to JSON.</param>
    /// <param name="version">Envelope version, default "1.0".</param>
    Task SendNotificationAsync(
        IEnumerable<string> fcmTokens,
        FirebaseMessageType type,
        string title,
        string body,
        //       object payload = null,
        Dictionary<string, string>? data = null,
        string version = "1.0"
    );
}