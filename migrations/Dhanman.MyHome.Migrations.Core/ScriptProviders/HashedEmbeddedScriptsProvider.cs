﻿using DbUp.Engine;
using DbUp.Engine.Transactions;
using Dhanman.MyHome.Migrations.Core.Hashing;
using Dhanman.MyHome.Migrations.Core.Journal;
using System.Reflection;

namespace Dhanman.MyHome.Migrations.Core.ScriptProviders;

public class HashedEmbeddedScriptsProvider : IScriptProvider
{
    private readonly Assembly _assembly;
    private readonly HashedSqlTableJournal _journal;

    /// <summary>
    /// Initializes a new instance of the <see cref="HashedEmbeddedScriptsProvider"/> class.
    /// </summary>
    /// <param name="assembly">The assembly.</param>
    /// <param name="journal">The journal instance.</param>
    public HashedEmbeddedScriptsProvider(Assembly assembly, HashedSqlTableJournal journal)
    {
        _assembly = assembly;
        _journal = journal;
    }

    /// <inheritdoc />
    public IEnumerable<SqlScript> GetScripts(IConnectionManager connectionManager)
    {
        Dictionary<string, string> executedScriptsDictionary = _journal.GetExecutedScriptsDictionary();

        IEnumerable<SqlScript> allSqlScripts = GetAllSqlScriptsEmbeddedInAssembly();

        return allSqlScripts
            .Where(sqlScript => !executedScriptsDictionary.TryGetValue(sqlScript.Name, out string sqlScriptHash) ||
                                SHA256.ComputeHash(sqlScript.Contents) != sqlScriptHash);
    }

    /// <summary>
    /// Gets all of the SQL scripts embedded in the assembly.
    /// </summary>
    /// <returns>The enumerable collection of all SQL scripts embedded in the assembly.</returns>
    private IEnumerable<SqlScript> GetAllSqlScriptsEmbeddedInAssembly()
    {
        IEnumerable<SqlScript> allSqlScripts = _assembly
            .GetManifestResourceNames()
            .Where(resourceName => resourceName.EndsWith(".sql", StringComparison.OrdinalIgnoreCase))
            .Select(resourceName => SqlScript.FromStream(resourceName, _assembly.GetManifestResourceStream(resourceName)))
            .OrderBy(sqlScript => sqlScript.Name);

        return allSqlScripts;
    }
}