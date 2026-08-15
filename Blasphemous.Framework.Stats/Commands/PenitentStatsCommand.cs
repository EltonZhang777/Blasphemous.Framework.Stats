using Blasphemous.CheatConsole;
using Blasphemous.NewbieEltonLibs.CheatConsole;
using Blasphemous.NewbieEltonLibs.Extensions.ModdingAPI;
using Blasphemous.Framework.Stats.StatsPatching.EntitiesStats.PenitentStats;
using Framework.Managers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace Blasphemous.Framework.Stats.Commands;

internal class PenitentStatsCommand : AutoModCommand
{
    protected override string CommandName => "penitentstats";

#if DEBUG
    [ModSubCommand("exportjson", "read the stats of Penitent and export it to JSON file (debug use)")]
    private void SubCommand_ExportJson(string[] parameters)
    {
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

    [ModSubCommand("importjson", "write the stats of Penitent from JSON file (debug use)")]
    private void SubCommand_ImportJson(string[] parameters)
    {
        PenitentData data = new();
        string fileName = "penitent_data.json";
        Main.StatsFramework.FileHandler.LoadContentAsJson<PenitentData>(fileName, out data);
        data.SetValueTo(Core.Logic.Penitent);

        Write($"Successfully imported penitent values from `{fileName}` !");
    }
#endif
}
