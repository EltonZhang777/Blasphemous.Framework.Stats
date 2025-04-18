using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blasphemous.CheatConsole;
using Blasphemous.Framework.Stats.PenitentInfo;
using Blasphemous.ModdingAPI.Files;
using Framework.Managers;
using Newtonsoft.Json;
using System.IO;
using Blasphemous.Framework.Stats.Components;

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
        result.Add("readinfo", SubCommand_ReadInfo);
        result.Add("writeinfo", SubCommand_WriteInfo);
#endif

        return result;
    }

    private void SubCommand_Help(string[] parameters)
    {
        if (!ValidateParameterList(parameters, 0))
            return;

#if DEBUG
        Write($"{CommandName} readinfo : Read the stats of Penitent and export it to JSON file");
        Write($"{CommandName} writeinfo : Write the stats of Penitent from JSON file");
#endif
    }

    private void SubCommand_ReadInfo(string[] parameters)
    {
        if (!ValidateParameterList(parameters, 0))
            return;

        PenitentData result = new();
        result.GetValueFrom(Core.Logic.Penitent);
        string exportPath = Main.StatsFramework.FileHandler.ContentFolder + @"penitent_stats.json";
        File.WriteAllText(
            exportPath,
            JsonConvert.SerializeObject(
                result,
                Formatting.Indented));

        Write($"Successfully written penitent values to `{exportPath}` !");
    }

    private void SubCommand_WriteInfo(string[] parameters)
    {
        if (!ValidateParameterList(parameters, 0))
            return;

        PenitentData result = new();
        string importPath = Main.StatsFramework.FileHandler.ContentFolder + @"penitent_stats.json";
        result = JsonConvert.DeserializeObject<PenitentData>(File.ReadAllText(importPath));
        result.SetValueTo(Core.Logic.Penitent);

        Write($"Successfully imported penitent values from `{importPath}` !");
    }

    private bool ValidateParameterList(string[] parameters, List<int> validParameterLengths)
    {
        if (!validParameterLengths.Contains(parameters.Length))
        {
            StringBuilder sb = new();
            sb.Append($"This command takes ");
            for (int i = 0; i < validParameterLengths.Count; i++)
            {
                sb.Append($"{i} ");
                if (i != validParameterLengths.Count - 1)
                    sb.Append("or ");
            }
            sb.Append($"parameters.  You passed {parameters.Length}");
            Write(sb.ToString());

            return false;
        }

        return true;
    }
}
