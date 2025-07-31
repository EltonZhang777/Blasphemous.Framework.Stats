using Blasphemous.ModdingAPI;
using Framework.Inventory;
using Framework.Managers;
using System.Collections.Generic;
using static Framework.Managers.InventoryManager;

namespace Blasphemous.Framework.Stats.Extensions;

internal static class InventoryManagerExtensions
{
    internal static List<BaseInventoryObject> GetAllInventoryObjects(this InventoryManager inventoryManager)
    {
        List<BaseInventoryObject> result = [
            .. inventoryManager.GetAllCollectibleItems(),
            .. inventoryManager.GetAllPrayers(),
            .. inventoryManager.GetAllQuestItems(),
            .. inventoryManager.GetAllRelics(),
            .. inventoryManager.GetAllRosaryBeads(),
            .. inventoryManager.GetAllSwords(),
            ];
        return result;
    }

    internal static List<BaseInventoryObject> GetAllOwnedInventoryObjects(this InventoryManager inventoryManager)
    {
        List<BaseInventoryObject> result = [
            .. inventoryManager.GetCollectibleItemOwned(),
            .. inventoryManager.GetPrayersOwned(),
            .. inventoryManager.GetQuestItemOwned(),
            .. inventoryManager.GetRelicsOwned(),
            .. inventoryManager.GetRosaryBeadOwned(),
            .. inventoryManager.GetSwordsOwned(),
            ];
        return result;
    }

    internal static BaseInventoryObject GetInventoryItemFromId(this InventoryManager inventoryManager, string id)
    {
        id = id.Trim();
        ItemType itemType = inventoryManager.GetItemTypeFromId(id);
        switch (itemType)
        {
            case ItemType.Relic:
                return inventoryManager.GetRelic(id);
            case ItemType.Bead:
                return inventoryManager.GetRosaryBead(id);
            case ItemType.Quest:
                return inventoryManager.GetQuestItem(id);
            case ItemType.Prayer:
                return inventoryManager.GetPrayer(id);
            case ItemType.Collectible:
                return inventoryManager.GetCollectibleItem(id);
            case ItemType.Sword:
                return inventoryManager.GetSword(id);
            default:
                ModLog.Error($"Unknown inventory item type for id: {id}");
                return null;
        }
    }

    internal static ItemType GetItemTypeFromId(this InventoryManager inventoryManager, string id)
    {
        ItemType result = ItemType.Collectible;
        id = id.Trim();

        if (id.StartsWith("RE"))
        {
            result = ItemType.Relic;
        }
        else if (id.StartsWith("RB"))
        {
            result = ItemType.Bead;
        }
        else if (id.StartsWith("QI"))
        {
            result = ItemType.Quest;
        }
        else if (id.StartsWith("PR"))
        {
            result = ItemType.Prayer;
        }
        else if (id.StartsWith("CO"))
        {
            result = ItemType.Collectible;
        }
        else if (id.StartsWith("HE"))
        {
            result = ItemType.Sword;
        }
        else
        {
            ModLog.Error($"Unknown inventory item type for id: {id}");
        }

        return result;
    }
}
