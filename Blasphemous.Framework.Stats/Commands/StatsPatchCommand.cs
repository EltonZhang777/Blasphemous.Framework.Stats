using Blasphemous.CheatConsole;
using Blasphemous.Framework.Stats.Components;
using Blasphemous.Framework.Stats.Extensions;
using Blasphemous.Framework.Stats.StatsPatching;
using Blasphemous.Framework.Stats.StatsPatching.EntitiesStats.EnemyStats;
using Blasphemous.Framework.Stats.StatsPatching.EntitiesStats.PenitentStats;
using Blasphemous.Framework.Stats.StatsPatching.ItemStats;
using Blasphemous.ModdingAPI;
using Framework.Managers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Blasphemous.Framework.Stats.Commands;

internal class StatsPatchCommand : ModCommand
{
    protected override string CommandName => "statspatch";

    protected override bool AllowUppercase => true;
    protected override Dictionary<string, Action<string[]>> AddSubCommands()
    {
        Dictionary<string, Action<string[]>> result = new()
        {
            { "help", SubCommand_Help },
            { "list", SubCommand_List },
            { "activate", SubCommand_Activate },
            { "deactivate", SubCommand_Deactivate }
        };
#if DEBUG
        result.Add("exportjson", SubCommand_ExportToJson);
        result.Add("exportallinventoryitems", SubCommand_ExportAllInventoryItems);
#endif

        return result;
    }

    private void SubCommand_Help(string[] parameters)
    {
        if (!ValidateParameterList(parameters, 0))
            return;

        Write($"Available {CommandName} commands:");
        Write($"{CommandName} list : list all loaded stats patches");
        Write($"{CommandName} list [active/inactive] : list all active/inactive stats patches");
        Write($"{CommandName} activate [patchName] : activate the specified stats patch. (only supports patches that can be manually toggled active)");
        Write($"{CommandName} deactivate [patchName] : deactivate the specified stats patch. (only supports patches that can be manually toggled active)");
#if DEBUG
        Write($"{CommandName} exportjson [patchName] : (debug use) export the info of specified stats patch to JSON file");
        Write($"{CommandName} exportallinventoryitems : (debug use) export the info all inventory items to JSON file");
#endif
    }

    private void SubCommand_List(string[] parameters)
    {
        if (!ValidateParameterList(parameters, [0, 1]))
            return;

        bool hasAny = false;
        if (parameters.Length == 0)
        {
            Write($"All loaded backgrounds: ");
            foreach (BaseStatsPatch patch in StatsPatchRegister.StatsPatches)
            {
                hasAny = true;
                string activeState = patch.isActive ? "yes" : "no";
                Write($"  {patch.name}\n    [active?: {activeState}]\n    [type: {patch.GetType()}]");
            }
            if (!hasAny)
            {
                Write($"No stats patch is found!");
            }
        }
        else
        {
            if (parameters[0].Equals("active"))
            {
                Write($"All active stats patches: ");
                foreach (BaseStatsPatch patch in StatsPatchRegister.StatsPatches.Where(x => x.isActive == true))
                {
                    hasAny = true;
                    Write($"  {patch.name}");
                }
                if (!hasAny)
                {
                    Write($"No stats patch is found!");
                }
            }
            else if (parameters[0].Equals("inactive"))
            {
                Write($"All inactive stats patches: ");
                foreach (BaseStatsPatch patch in StatsPatchRegister.StatsPatches.Where(x => x.isActive == false))
                {
                    hasAny = true;
                    Write($"  {patch.name}");
                }
                if (!hasAny)
                {
                    Write($"No stats patch is found!");
                }
            }
            else
            {
                Write($"Unknown parameter #0 `{parameters[0]}`!");
            }
        }
    }

    private void SubCommand_Activate(string[] parameters)
    {
        if (!ValidateParameterList(parameters, 1))
            return;

        string targetName = parameters[0];
        if (TrySetPatchActive(targetName, true))
        {
            Write($"Successfully activated patch `{targetName}`!");
        }
    }

    private void SubCommand_Deactivate(string[] parameters)
    {
        if (!ValidateParameterList(parameters, 1))
            return;

        string targetName = parameters[0];
        if (TrySetPatchActive(targetName, false))
        {
            Write($"Successfully deactivated patch `{targetName}`!");
        }
    }

    private void SubCommand_ExportToJson(string[] parameters)
    {
        if (!ValidateParameterList(parameters, 1))
            return;

        string backgroundName = parameters[0];
        if (!StatsPatchExists(backgroundName))
            return;

        string exportPath = Path.Combine(Main.StatsFramework.FileHandler.ContentFolder, $"exported--{backgroundName}.json");
        switch (StatsPatchRegister.AtName(backgroundName))
        {
            case PenitentStatsPatch patch:
                File.WriteAllText(
                    exportPath,
                    JsonConvert.SerializeObject(patch, Formatting.Indented));
                break;
            case EnemyStatsPatch patch:
                File.WriteAllText(
                    exportPath,
                    JsonConvert.SerializeObject(patch, Formatting.Indented));
                break;
        }
        Write($"Successfully exported `{backgroundName}` info to `{exportPath}`!");
    }

    private void SubCommand_ExportAllInventoryItems(string[] parameters)
    {
        // WIP!
        if (!ValidateParameterList(parameters, 0))
            return;

        JsonSerializerSettings jsonSerializerSettings = new()
        {
            Converters = [
                new UnityEngineIgnoreConverter(),
                new StringEnumConverter(),
                ],
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            PreserveReferencesHandling = PreserveReferencesHandling.None,
            TypeNameHandling = TypeNameHandling.Objects,
        };

        InventoryItemStatsPatch patch = new();
        foreach (var item in Core.InventoryManager.GetAllCollectibleItems())
        {
            InventoryItemData itemData = new InventoryItemData(item.id);
            itemData.GetValueFrom(item);
            patch.statsPatches.Add(itemData);
        }
        foreach (var item in Core.InventoryManager.GetAllPrayers())
        {
            InventoryItemData itemData = new InventoryItemData(item.id);
            itemData.GetValueFrom(item);
            patch.statsPatches.Add(itemData);
        }
        foreach (var item in Core.InventoryManager.GetAllQuestItems())
        {
            InventoryItemData itemData = new InventoryItemData(item.id);
            itemData.GetValueFrom(item);
            patch.statsPatches.Add(itemData);
        }
        foreach (var item in Core.InventoryManager.GetAllRelics())
        {
            InventoryItemData itemData = new InventoryItemData(item.id);
            itemData.GetValueFrom(item);
            patch.statsPatches.Add(itemData);
        }
        foreach (var item in Core.InventoryManager.GetAllRosaryBeads())
        {
            InventoryItemData itemData = new InventoryItemData(item.id);
            itemData.GetValueFrom(item);
            patch.statsPatches.Add(itemData);
        }
        foreach (var item in Core.InventoryManager.GetAllSwords())
        {
            InventoryItemData itemData = new InventoryItemData(item.id);
            itemData.GetValueFrom(item);
            patch.statsPatches.Add(itemData);
        }

        foreach (var itemPatch in patch.statsPatches)
        {
            ModLog.Warn($"Serializing `{itemPatch.itemId}`!");
            Main.StatsFramework.FileHandler.WriteJsonToContent(
                $"{itemPatch.itemId}.json",
                itemPatch,
                jsonSerializerSettings);
        }

        Write($"Successfully exported all inventory items' data to `{Main.StatsFramework.FileHandler.ContentFolder}`!");
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

    private bool StatsPatchExists(string name)
    {
        if (!StatsPatchRegister.Exists(name))
        {
            Write($"Stats patch `{name}` not found!");
            return false;
        }
        return true;
    }

    private bool TrySetPatchActive(string patchName, bool active)
    {
        if (!StatsPatchExists(patchName))
            return false;

        BaseStatsPatch targetPatch = StatsPatchRegister.StatsPatches.First(x => x.name.Equals(patchName));
        if (targetPatch.activeType != BaseStatsPatch.ActiveType.Manually)
        {
            Write($"Manual patch activation is only applicable to stats patches that are manually activated!");
            return false;
        }

        targetPatch.isActive = active;
        return true;
    }
}

public class UnityEngineIgnoreConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        // List of types to ignore
        List<System.Type> ignoredTypes = [
            typeof(UnityEngine.GameObject),
            typeof(UnityEngine.Transform),
            typeof(UnityEngine.Texture),
            typeof(UnityEngine.Sprite),
            typeof(UnityEngine.UI.Image),
            typeof(UnityEngine.Material)
            ];

        return ignoredTypes.Contains(objectType);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        // Simply write a null value for ignored types
        writer.WriteNull();
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        // Handle deserialization if necessary, or just return null
        return null;
    }
}