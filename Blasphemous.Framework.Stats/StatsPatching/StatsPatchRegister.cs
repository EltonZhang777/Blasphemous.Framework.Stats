using Blasphemous.Framework.Stats.Components;
using Blasphemous.Framework.Stats.StatsPatching.EntitiesStats.EnemyStats;
using Blasphemous.Framework.Stats.StatsPatching.EntitiesStats.PenitentStats;
using Blasphemous.Framework.Stats.StatsPatching.ItemStats;
using Blasphemous.ModdingAPI;
using System.Collections.Generic;
using System.Linq;

namespace Blasphemous.Framework.Stats.StatsPatching;

/// <summary>
/// Register handler for new stats patches
/// </summary>
public static class StatsPatchRegister
{
    private static readonly List<BaseStatsPatch> _statsPatches = [];
    internal static IEnumerable<BaseStatsPatch> StatsPatches => _statsPatches;
    internal static IEnumerable<PenitentStatsPatch> PenitentPatches => _statsPatches.OfType<PenitentStatsPatch>();
    internal static IEnumerable<EnemyStatsPatch> EnemyPatches => _statsPatches.OfType<EnemyStatsPatch>();
    internal static IEnumerable<InventoryItemStatsPatch> ItemPatches => _statsPatches.OfType<InventoryItemStatsPatch>();
    internal static int Total => _statsPatches.Count;

    internal static BaseStatsPatch AtIndex(int index) => _statsPatches[index];

    internal static BaseStatsPatch AtName(string name)
    {
        BaseStatsPatch result = Exists(name)
            ? _statsPatches.First(x => x.name == name)
            : null;
        if (result == null)
        {
            ModLog.Error($"Failed to access nonexistent stats patch of name `{name}`");
        }
        return result;
    }

    internal static bool Exists(string name) => _statsPatches.Any(x => x.name == name);
    internal static bool Exists(string name, bool active) => _statsPatches.Any(x => x.name == name && x.isActive == active);
    internal static bool Exists<T>(string name) where T : BaseStatsPatch => _statsPatches.OfType<T>().Any(x => x.name == name);
    internal static bool Exists<T>(string name, bool active) where T : BaseStatsPatch => _statsPatches.OfType<T>().Any(x => x.name == name && x.isActive == active);
    internal static IEnumerable<T> OfType<T>(this IEnumerable<BaseStatsPatch> collection) where T : BaseStatsPatch
    {
        return collection.Where(x => x is T)?.Select(x => x as T);
    }
    internal static IEnumerable<T> SelectActive<T>(this IEnumerable<T> collection, bool active) where T : BaseStatsPatch
    {
        return collection.Where(x => x.isActive == active);
    }

    /// <summary>
    /// Registers a new stats patch 
    /// </summary>
    public static void RegisterStatsPatch(
        this ModServiceProvider provider,
        BaseStatsPatch statsPatch)
    {
        if (provider == null)
            return;

        // prevents repeated registering
        if (_statsPatches.Any(x => x.name == statsPatch.name))
            return;

        statsPatch.parentModId = provider.RegisteringMod.Id;
        _statsPatches.Add(statsPatch);
        ModLog.Info($"Registered custom stats patch: {statsPatch.name}");
    }
}