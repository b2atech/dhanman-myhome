using System.Runtime.CompilerServices;

namespace Dhanman.MyHome.Persistence;

public static class MyModuleInitializer
{
    [ModuleInitializer]
    public static void Initialize()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
}
