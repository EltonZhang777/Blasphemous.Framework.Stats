using Blasphemous.CheatConsole;
using Blasphemous.NewbieEltonLibs.Extensions.ModdingAPI;
using Blasphemous.Framework.Stats.StatsPatching.EntitiesStats.PenitentStats;
using Framework.Managers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace Blasphemous.Framework.Stats.Commands;

internal class PenitentStatsCommand : ModCommand
{
    protected override string CommandName => "penitentstats";

    protected override bool AllowUppercase => true;

    protected override Dictionary<string, Action<string[]>> AddSubCommands()
    {
        Dictionary<string, Action<string[]>> result = new()
        {
            { "help", SubCommand_Help },
        };

#if DEBUG
        result.Add("exportjson", SubCommand_ExportJson);
        result.Add("importjson", SubCommand_ImportJson);
#endif

        return result;
    }

    private void SubCommand_Help(string[] parameters)
    {
        if (!ValidateParameterList(parameters, 0))
            return;

#if DEBUG
        Write($"{CommandName} exportjson: Read the stats of Penitent and export it to JSON file");
        Write($"{CommandName} importjson : Write the stats of Penitent from JSON file");
#endif
    }

    private void SubCommand_ExportJson(string[] parameters)
    {
        if (!ValidateParameterList(parameters, 0))
            return;

        PenitentData data = new();
        string fileName = "penitent_data.json";
        data.GetValueFrom(Core.Logic.Penitent);
        JsonSerializerSettings settings = new()
        {
            Converters = [
                new StringEnumConverter(),
                ]
        };
        Main.StatsFramework.FileHandler.WriteJsonToContent(fileName, data, settings);

        Write($"Successfully exported penitent values to `{fileName}` !");
    }

    private void SubCommand_ImportJson(string[] parameters)
    {
        if (!ValidateParameterList(parameters, 0))
            return;

        PenitentData data = new();
        string fileName = "penitent_data.json";
        Main.StatsFramework.FileHandler.LoadContentAsJson<PenitentData>(fileName, out data);
        data.SetValueTo(Core.Logic.Penitent);

        Write($"Successfully imported penitent values from `{fileName}` !");
    }
}
