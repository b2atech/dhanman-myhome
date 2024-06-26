﻿using DbUp;
using DbUp.Engine;
using DbUp.Engine.Output;
using DbUp.Postgresql;
using Dhanman.MyHome.Migrations.Core.Extensions;
using Dhanman.MyHome.Migrations.Core.Journal;
using Dhanman.MyHome.Migrations.Scripts;
using System.Reflection;

namespace Dhanman.MyHome.Migrations.Core;

public static class MigrationsManager
{
    /// <summary>
    /// Executes all of the migration scripts that have not been ran.
    /// </summary>
    /// <param name="connectionString">The connection string.</param>
    /// <returns>Tuple containing a success status flag and an exception, if one is thrown.</returns>
    public static (bool Success, Exception? Error) ExecuteMigrations(string connectionString)
    {
        UpgradeEngine upgradeEngine = BuildUpgradeEngine(connectionString);

        EnsureDatabase.For.PostgresqlDatabase(connectionString);

        DatabaseUpgradeResult result = upgradeEngine.PerformUpgrade();

        return (result.Successful, result.Error);
    }

    /// <summary>
    /// Builds the database upgrade engine instance.
    /// </summary>
    /// <param name="connectionString">The connection string.</param>
    /// <returns>The configured database upgrade engine instance.</returns>
    private static UpgradeEngine BuildUpgradeEngine(string connectionString)
    {
        Assembly scriptsAssembly = typeof(ScriptsAssembly).Assembly;

        var connectionManager = new PostgresqlConnectionManager(connectionString);

        var log = new ConsoleUpgradeLog();

        var hashedSqlTableJournal = new HashedSqlTableJournal(() => connectionManager, () => log);

        UpgradeEngine upgradeEngine = DeployChanges
            .To
            .HashedSqlDatabase(connectionManager)
            .WithHashedScriptsEmbeddedInAssembly(scriptsAssembly, hashedSqlTableJournal)
            .LogToConsole()
            .Build();

        return upgradeEngine;
    }
}
