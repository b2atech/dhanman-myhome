namespace Dhanman.MyHome.Persistence;

public sealed class ConnectionString
{
    /// <summary>
    /// The connection strings key.
    /// </summary>
    public const string SettingsKey = "CommunityDb";
    public const string PermissionDBKey = "PermissionsDb";
    public const string TemplateDBKey = "TemplateDb";

    /// <summary>
    /// Initializes a new instance of the <see cref="ConnectionString"/> class.
    /// </summary>
    /// <param name="value">The connection string value.</param>
    public ConnectionString(string value) => Value = value;

    /// <summary>
    /// Gets the connection string value.
    /// </summary>
    public string Value { get; }

    public static implicit operator string(ConnectionString connectionString) => connectionString.Value;
}
