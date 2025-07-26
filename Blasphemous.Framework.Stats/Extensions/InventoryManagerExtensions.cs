using Framework.Inventory;
using Framework.Managers;
using System.Collections.Generic;

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
}
