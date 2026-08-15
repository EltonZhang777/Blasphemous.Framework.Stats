using BepInEx;
using Blasphemous.ModdingAPI;
using Framework.FrameworkCore;
using UnityEngine;

namespace Blasphemous.Framework.Stats;

[BepInPlugin(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_VERSION)]
[BepInDependency("Blasphemous.ModdingAPI", "2.4.1")]
[BepInDependency("Blasphemous.Framework.UI", "0.1.2")]

internal class Main : BaseUnityPlugin
{
    public static StatsFramework StatsFramework { get; private set; }

    private void Start()
    {
        StatsFramework = new StatsFramework();
    }

    /// <summary>
    /// Return a normalized directional <see cref="Vector2"/> based on the <see cref="EntityOrientation"/> given.
    /// </summary>
    /// <param name="entityOrientation">The given orientation</param>
    /// <returns><see cref="Vector2.left"/> if <see cref="EntityOrientation.Left"/>, <see cref="Vector2.right"/> if <see cref="EntityOrientation.Right"/></returns>
    internal static Vector2 EntityOrientationToDirectionalVector(EntityOrientation entityOrientation)
    {
        return entityOrientation switch
        {
            EntityOrientation.Right => Vector2.right,
            EntityOrientation.Left => Vector2.left
        };
    }
}
