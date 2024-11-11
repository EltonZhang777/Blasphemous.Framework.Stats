using BepInEx;

namespace Blasphemous.Framework.Stats;

[BepInPlugin(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_VERSION)]
[BepInDependency("Blasphemous.ModdingAPI", "2.4.1")]
[BepInDependency("Blasphemous.Framework.UI", "0.1.2")]

public class Main : BaseUnityPlugin
{
    public static StatsFramework StatsFramework { get; private set; }

    private void Start()
    {
        StatsFramework = new StatsFramework();
    }
}
