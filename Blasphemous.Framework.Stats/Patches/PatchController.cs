using Blasphemous.Framework.Stats.Components;
using Blasphemous.Framework.Stats.Extensions;
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
    internal static HitPatchController Hits { get; set; } = new();

    /// <summary>
    /// Patch all active stats patches.
    /// </summary>
    internal static void PatchAllStats()
    {
        PatchPenitentStats();
        PatchEnemyStats();
        PatchItemStats();
    }

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
    /// Patch all active Penitent stats patches to penitent object.
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
        //// store the loadout and unequip everything before patching 
        //EquipmentLoadout loadout = Core.InventoryManager.SaveCurrentEquipmentAsLoadout();
        //Core.InventoryManager.RemoveEquipableObjects();

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

        //// re-equip all objects
        //Core.InventoryManager.EquipItemsInLoadout(loadout);
    }
}
