global using BlasAttribute = Framework.FrameworkCore.Attributes.Logic.Attribute;
global using UObject = UnityEngine.Object;
using Blasphemous.CheatConsole;
using Blasphemous.Framework.Stats.Commands;
using Blasphemous.Framework.Stats.Extensions;
using Blasphemous.Framework.Stats.Patches;
using Blasphemous.Framework.Stats.Patches.ItemPatches;
using Blasphemous.Framework.Stats.StatsPatching;
using Blasphemous.Framework.Stats.StatsPatching.EntitiesStats.EnemyStats;
using Blasphemous.Framework.Stats.StatsPatching.EntitiesStats.PenitentStats;
using Blasphemous.Framework.Stats.StatsPatching.ItemStats;
using Blasphemous.ModdingAPI;
using Blasphemous.ModdingAPI.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;

namespace Blasphemous.Framework.Stats;

/// <summary>
/// Mod that allows other mods to read and write vanilla stats.
/// </summary>
public class StatsFramework : BlasMod
{
    internal static Config Config { get; set; }
    internal string StatsFolder => FileHandler.ModdingFolder + @"stats/";

    internal delegate void StandardEvent();
    internal event StandardEvent OnLateUpdateEvent;

    private static bool _firstLevelLoadedFlag = false;

    internal StatsFramework()
        : base(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_AUTHOR, ModInfo.MOD_VERSION)
    { }

    /// <inheritdoc/>
    protected override void OnInitialize()
    {
        //LocalizationHandler.RegisterDefaultLanguage("en");
        Config = ConfigHandler.Load<Config>();
        ConfigHandler.Save(Config);
        if (!Directory.Exists(StatsFolder))
        {
            Directory.CreateDirectory(StatsFolder);
        }
    }

    /// <inheritdoc/>
    protected override void OnRegisterServices(ModServiceProvider provider)
    {
        provider.RegisterCommand(new PenitentStatsCommand());
        provider.RegisterCommand(new StatsPatchCommand());

        JsonSerializerSettings jsonSerializerSettings = new()
        {
            Converters = [
                new StringEnumConverter(),
                ],
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            PreserveReferencesHandling = PreserveReferencesHandling.None,
            TypeNameHandling = TypeNameHandling.Objects,
        };
#if DEBUG
        provider.RegisterStatsPatch(FileHandler.LoadDataAsJson<PenitentStatsPatch>("test_patch_penitent.json", jsonSerializerSettings));
        provider.RegisterStatsPatch(FileHandler.LoadDataAsJson<EnemyStatsPatch>("test_patch_enemy.json", jsonSerializerSettings));
        provider.RegisterStatsPatch(FileHandler.LoadDataAsJson<InventoryItemStatsPatch>("test_patch_items.json", jsonSerializerSettings));
        provider.RegisterStatsPatch(FileHandler.LoadDataAsJson<InventoryItemStatsPatch>("test_patch_prayer.json", jsonSerializerSettings));
#endif
    }

    /// <inheritdoc/>
    protected override void OnLateUpdate()
    {
        OnLateUpdateEvent?.Invoke();
    }

    /// <inheritdoc/>
    protected override void OnLevelLoaded(string oldLevel, string newLevel)
    {
        if (SceneHelper.GameSceneLoaded)
        {
            if (!_firstLevelLoadedFlag)
            {
                _firstLevelLoadedFlag = true;

                // Do initialization work when a game scene is loaded for the first time
                PR03_HitPatch.GetTintMaterial();
            }

            PatchController.PatchAllStats();
        }
    }

    /// <inheritdoc/>
    protected override void OnExitGame()
    {
        ConfigHandler.Save(Config);
    }
}
