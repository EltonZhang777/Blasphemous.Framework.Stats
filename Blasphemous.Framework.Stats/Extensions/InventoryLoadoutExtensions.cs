using Blasphemous.Framework.Stats.Components;
using Blasphemous.ModdingAPI;
using Blasphemous.NewbieEltonLibs.Extensions.ModdingAPI;
using Framework.Managers;
using System.Collections.Generic;

namespace Blasphemous.Framework.Stats.Extensions;

/// <summary>
/// Loadout save/load helpers, kept local to this mod (not part of NewbieEltonLibs).
/// </summary>
internal static class InventoryLoadoutExtensions
{
    /// <summary>
    /// Save the current state of TPO to the Data object passed in
    /// </summary>
    public static EquipmentLoadout SaveCurrentEquipmentAsLoadout(this InventoryManager inventoryManager)
    {
        EquipmentLoadout result = new();

        result.beads = new();
        for (int i = 0; i < Core.Logic.Penitent.Stats.BeadSlots.Final; i++)
        {
            result.beads.Add(Core.InventoryManager.GetRosaryBeadInSlot(i)?.id);
        }

        result.swordHeart = Core.InventoryManager.GetSwordInSlot(0)?.id;
        result.prayer = Core.InventoryManager.GetPrayerInSlot(0)?.id;

        result.relics = new();
        for (int i = 0; i < 3; i++)
        {
            result.relics.Add(Core.InventoryManager.GetRelicInSlot(i)?.id);
        }

        return result;
    }

    /// <summary>
    /// Load the given loadout to current state of TPO
    /// </summary>
    public static void EquipItemsInLoadout(this InventoryManager inventoryManager, EquipmentLoadout loadout)
    {
        ModLogExtensions.InfoIfDebugBuild($"Equipping loadout: \n{loadout}");
        for (int i = 0; i < loadout.beads.Count; i++)
        {
            if (string.IsNullOrEmpty(loadout.beads[i]))
                continue;
            Core.InventoryManager.SetRosaryBeadInSlot(i, loadout.beads[i]);
        }
        if (!string.IsNullOrEmpty(loadout.swordHeart))
            Core.InventoryManager.SetSwordInSlot(0, loadout.swordHeart);
        if (!string.IsNullOrEmpty(loadout.prayer))
            Core.InventoryManager.SetPrayerInSlot(0, loadout.prayer);
        for (int i = 0; i < loadout.relics.Count; i++)
        {
            if (string.IsNullOrEmpty(loadout.relics[i]))
                continue;
            Core.InventoryManager.SetRelicInSlot(i, loadout.relics[i]);
        }
    }
}
