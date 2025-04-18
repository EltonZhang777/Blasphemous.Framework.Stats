global using BlasAttribute = Framework.FrameworkCore.Attributes.Logic.Attribute;
using Blasphemous.CheatConsole;
using Blasphemous.Framework.Stats.Commands;
using Blasphemous.Framework.Stats.PenitentInfo;
using Blasphemous.ModdingAPI;
using Blasphemous.ModdingAPI.Helpers;
using Framework.Managers;
using Newtonsoft.Json;
using System.IO;

namespace Blasphemous.Framework.Stats;

/// <summary>
/// Mod that allows other mods to read and write vanilla stats.
/// </summary>
public class StatsFramework : BlasMod
{
    internal static Config Config { get; set; }

    internal delegate void StandardEvent();
    internal event StandardEvent OnLateUpdateEvent;

    internal StatsFramework()
        : base(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_AUTHOR, ModInfo.MOD_VERSION)
    { }

    /// <inheritdoc/>
    protected override void OnInitialize()
    {
        //LocalizationHandler.RegisterDefaultLanguage("en");
        Config = ConfigHandler.Load<Config>();
        ConfigHandler.Save(Config);
        if (!Directory.Exists(FileHandler.ModdingFolder + @"stats/"))
        {
            Directory.CreateDirectory(FileHandler.ModdingFolder + @"stats/");
        }
    }

    /// <inheritdoc/>
    protected override void OnRegisterServices(ModServiceProvider provider)
    {
        provider.RegisterCommand(new PenitentStatsCommand());
    }

    /// <inheritdoc/>
    protected override void OnLateUpdate()
    {
        OnLateUpdateEvent?.Invoke();
    }

    /// <inheritdoc/>
    protected override void OnLevelLoaded(string oldLevel, string newLevel)
    {
    }

    /// <inheritdoc/>
    protected override void OnExitGame()
    {
        ConfigHandler.Save(Config);
    }
}
