namespace Dhanman.MyHome.Application.Abstractions;

public interface IFirebaseService
{
    Task SendNotificationAsync(string fcmToken, string title, string body, object data);
}
