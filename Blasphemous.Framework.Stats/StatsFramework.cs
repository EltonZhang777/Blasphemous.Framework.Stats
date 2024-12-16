using Blasphemous.Framework.Stats.PenitentInfo;
using Blasphemous.ModdingAPI;
using Blasphemous.ModdingAPI.Helpers;
using Newtonsoft.Json;
using System.IO;

namespace Blasphemous.Framework.Stats;

/// <summary>
/// Allows other mods to read and write vanilla stats.
/// </summary>
public class StatsFramework : BlasMod
{
    internal static Config Config { get; set; }

    internal delegate void StandardEvent();
    internal event StandardEvent OnLateUpdateEvent;

    internal StatsFramework()
        : base(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_AUTHOR, ModInfo.MOD_VERSION)
    { }

    protected override void OnInitialize()
    {
        Config = ConfigHandler.Load<Config>();
        ConfigHandler.Save(Config);
        if (!Directory.Exists(FileHandler.ModdingFolder + @"stats/"))
        {
            Directory.CreateDirectory(FileHandler.ModdingFolder + @"stats/");
        }
    }

    protected override void OnExitGame()
    {
        ConfigHandler.Save(Config);
    }

    protected override void OnLateUpdate()
    {
        OnLateUpdateEvent?.Invoke();
    }

    protected override void OnLevelLoaded(string oldLevel, string newLevel)
    {
#if DEBUG
        if (SceneHelper.GameSceneLoaded)
        {
            File.WriteAllText(
                FileHandler.ModdingFolder + @"stats/penitent.json",
                JsonConvert.SerializeObject(
                    PenitentInfoInspector.ReadPenitentInfo(),
                    Formatting.Indented));
        }
#endif
    }
}
