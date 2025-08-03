using Blasphemous.Framework.Stats.StatsPatching;
using Blasphemous.Framework.Stats.StatsPatching.EntitiesStats.EnemyStats;
using Blasphemous.Framework.Stats.StatsPatching.EntitiesStats.PenitentStats;
using Blasphemous.Framework.Stats.StatsPatching.ItemStats;
using Framework.Managers;
using Gameplay.GameControllers.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Blasphemous.Framework.Stats.Patches;

/// <summary>
/// Contains useful accessors and static methods that facilitate Harmony patching.
/// </summary>
internal static class PatchController
{
    /// <summary>
    /// Gets all living enemies and patch their stats with active stats patches.
    /// </summary>
    internal static void PatchEnemyStats()
    {
        List<string> distinctEntityIds = Entity.LivingEntities.Select(x => x.Id).Distinct().ToList();
        foreach (EnemyStatsPatch patch in StatsPatchRegister.EnemyPatches)
        {
            patch.UpdateActive();
            if (patch.isActive)
            {
                patch.statsPatches.Where(x => distinctEntityIds.Contains(x.entityId)).ToList().ForEach(x => x.SetValueToAllTargets());
            }
        }
    }

    /// <summary>
    /// Patch all active stats patches to penitent.
    /// </summary>
    internal static void PatchPenitentStats()
    {
        foreach (PenitentStatsPatch patch in StatsPatchRegister.PenitentPatches)
        {
            patch.UpdateActive();
            if (patch.isActive)
            {
                patch.statsPatches.ForEach(x => x.SetValueToAllTargets());
            }
        }
    }

    /// <summary>
    /// Patch all active item stats patches to corresponding items.
    /// </summary>
    internal static void PatchItemStats()
    {
        foreach (InventoryItemStatsPatch patch in StatsPatchRegister.ItemPatches)
        {
            patch.UpdateActive();
            if (patch.isActive)
            {
                foreach (InventoryItemData p in patch.statsPatches)
                {
                    if (p.isApplied)
                        continue;

                    p.SetValueToAllTargets();
                }
            }
            else
            {
                foreach (InventoryItemData p in patch.statsPatches)
                {
                    if (!p.isApplied)
                        continue;

                    p.RevertValueToAllTargets();
                }
            }

        }
    }

    /// <summary>
    /// Calculate the final damage of a hit.
    /// </summary>
    /// <param name="baseDamage">base damage before bonuses and multipliers</param>
    /// <param name="finalMultiplier">final damage multiplier after individual multipliers are calculated</param>
    /// <param name="damageBonuses">individual stat bonus and their multiplier (e.g. 20 attack power with 50% of physical damage bonus will be input as {20, 0.5})</param>
    /// <returns>Final calculated damage</returns>
    internal static float CalculateFinalDamageWithBonuses(float baseDamage, float finalMultiplier, params KeyValuePair<float, float>[] damageBonuses)
    {
        float result = baseDamage;
        foreach (KeyValuePair<float, float> kvp in damageBonuses)
        {
            result += kvp.Key * kvp.Value;
        }
        result *= finalMultiplier;
        return result;
    }

    internal static float CalculatePrayerDamageMultiplier(float prayerBonusEfficiency)
    {
        return 1f + (Core.Logic.Penitent.Stats.PrayerStrengthMultiplier.Final - 1f) * prayerBonusEfficiency;
    }
}
