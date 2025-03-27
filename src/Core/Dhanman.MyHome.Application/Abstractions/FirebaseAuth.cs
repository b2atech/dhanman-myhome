using Google.Apis.Auth.OAuth2;

namespace Dhanman.MyHome.Application.Abstractions;

public class FirebaseAuth
{
    private static readonly string[] Scopes = { "https://www.googleapis.com/auth/firebase.messaging" };
    private readonly string _serviceAccountKeyPath;

    public FirebaseAuth(string serviceAccountKeyPath)
    {
        _serviceAccountKeyPath = serviceAccountKeyPath;
    }

    public async Task<string> GetAccessTokenAsync()
    {
        GoogleCredential credential;
        using (var stream = new FileStream(_serviceAccountKeyPath, FileMode.Open, FileAccess.Read))
        {
            credential = GoogleCredential.FromStream(stream);
        }

        if (credential.IsCreateScopedRequired)
        {
            credential = credential.CreateScoped(Scopes);
        }

        var accessToken = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();
        return accessToken;
    }
}
