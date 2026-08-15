using Blasphemous.CheatConsole;
using Blasphemous.NewbieEltonLibs.CheatConsole;
using Blasphemous.Framework.Stats.Components;
using Blasphemous.NewbieEltonLibs.Extensions.GameLibs;
using Blasphemous.NewbieEltonLibs.Extensions.ModdingAPI;
using Blasphemous.Framework.Stats.StatsPatching;
using Blasphemous.Framework.Stats.StatsPatching.ItemStats;
using Blasphemous.ModdingAPI;
using Framework.Inventory;
using Framework.Managers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Blasphemous.Framework.Stats.Commands;

internal class StatsPatchCommand : AutoModCommand
{
    protected override string CommandName => "statspatch";

    [ModSubCommand("list", "list all loaded stats patches", "[active/inactive]", 0, 1)]
    private void SubCommand_List(string[] parameters)
    {
        bool hasAny = false;
        if (parameters.Length == 0)
        {
            Write($"All loaded stats patches: ");
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

    [ModSubCommand("activate", "activate the specified stats patch (manually toggled only)", "[patchName]", 1)]
    private void SubCommand_Activate(string[] parameters)
    {
        string targetName = parameters[0];
        if (TrySetPatchActive(targetName, true))
        {
            Write($"Successfully activated patch `{targetName}`!");
        }
    }

    [ModSubCommand("deactivate", "deactivate the specified stats patch (manually toggled only)", "[patchName]", 1)]
    private void SubCommand_Deactivate(string[] parameters)
    {
        string targetName = parameters[0];
        if (TrySetPatchActive(targetName, false))
        {
            Write($"Successfully deactivated patch `{targetName}`!");
        }
    }

#if DEBUG
    [ModSubCommand("exportjson", "export the info of specified stats patch to JSON file (debug use)", "[patchName]", 1)]
    private void SubCommand_ExportToJson(string[] parameters)
    {
        string patchName = parameters[0];
        if (!StatsPatchExists(patchName))
            return;

        string exportPath = Path.Combine(Main.StatsFramework.FileHandler.ContentFolder, $"exported--{patchName}.json");
        JsonSerializerSettings jsonSerializerSettings = new()
        {
            Converters = [
                new StringEnumConverter(),
                ],
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            PreserveReferencesHandling = PreserveReferencesHandling.None,
            TypeNameHandling = TypeNameHandling.Objects,
        };

        File.WriteAllText(
            exportPath,
            JsonConvert.SerializeObject(
                StatsPatchRegister.AtName(patchName),
                Formatting.Indented,
                jsonSerializerSettings));
        Write($"Successfully exported `{patchName}` info to `{exportPath}`!");
    }
#endif

#if DEBUG
    [ModSubCommand("exportallinventoryitems", "export the info of all inventory items to JSON file (debug use)", null, 0)]
    private void SubCommand_ExportAllInventoryItems(string[] parameters)
    {
        // WIP!
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
        foreach (BaseInventoryObject item in Core.InventoryManager.GetAllInventoryObjects())
        {
            InventoryItemData itemData = new(item.id);
            itemData.GetValueFrom(item);
            patch.statsPatches.Add(itemData);
        }

        foreach (InventoryItemData itemPatch in patch.statsPatches)
        {
            ModLog.Warn($"Serializing `{itemPatch.itemId}`!");
            Main.StatsFramework.FileHandler.WriteJsonToContent(
                $"{itemPatch.itemId}.json",
                itemPatch,
                jsonSerializerSettings);
        }

        Write($"Successfully exported all inventory items' data to `{Main.StatsFramework.FileHandler.ContentFolder}`!");
    }
#endif

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
