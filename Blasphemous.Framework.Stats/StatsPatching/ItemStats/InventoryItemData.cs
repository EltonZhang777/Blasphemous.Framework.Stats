using Blasphemous.Framework.Stats.Components;
using Blasphemous.ModdingAPI;
using Framework.Inventory;
using Framework.Managers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats;

/// <summary>
/// Stats of an of Inventory Item
/// </summary>
public class InventoryItemData : IAccessible<BaseInventoryObject>, IStatsPatchable
{
    [JsonProperty(Required = Required.Always)]
    public string itemId;

    internal InventoryItemType itemType;

    protected BaseInventoryObject target;

    internal enum InventoryItemType
    {
        Relic,
        RosaryBead,
        QuestItem,
        Prayer,
        Collectible,
        SwordHeart
    }

    /// <summary>
    /// WIP temporary implementation
    /// </summary>
    public List<MonoBehaviour> scripts = new();

    [JsonConstructor]
    public InventoryItemData()
    {
        itemType = GetItemTypeFromId(itemId);
    }

    public InventoryItemData(string id)
    {
        itemId = id;
        itemType = GetItemTypeFromId(itemId);
    }

    /// <inheritdoc/>
    public void GetValueFrom(BaseInventoryObject obj)
    {
        // WIP
        scripts = obj.gameObject.GetComponents<MonoBehaviour>().ToList();
        scripts.RemoveAll(obj => obj as BaseInventoryObject != null);
        ModLog.Warn($"  Monobehaviors for `{itemId}` : ");
        scripts.ForEach(x => ModLog.Warn(x.GetType()));
        ModLog.Info("\n");
    }

    /// <inheritdoc/>
    public void GetValueFromFirstTarget()
    {
        GetValueFrom(target);
    }

    /// <inheritdoc/>
    public void SetValueTo(BaseInventoryObject obj)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public void SetValueToAllTargets()
    {
        SetValueTo(target);
    }

    /// <inheritdoc/>
    public bool TryGetTargets()
    {
        target = GetInventoryItemFromId(itemId);
        return target != null;
    }

    internal static BaseInventoryObject GetInventoryItemFromId(string id)
    {
        id = id.Trim();
        InventoryItemType itemType = GetItemTypeFromId(id);
        switch (itemType)
        {
            case InventoryItemType.Relic:
                return Core.InventoryManager.GetRelic(id);
            case InventoryItemType.RosaryBead:
                return Core.InventoryManager.GetRosaryBead(id);
            case InventoryItemType.QuestItem:
                return Core.InventoryManager.GetQuestItem(id);
            case InventoryItemType.Prayer:
                return Core.InventoryManager.GetPrayer(id);
            case InventoryItemType.Collectible:
                return Core.InventoryManager.GetCollectibleItem(id);
            case InventoryItemType.SwordHeart:
                return Core.InventoryManager.GetSword(id);
            default:
                ModLog.Error($"Unknown inventory item type for id: {id}");
                return null;
        }
    }

    internal static InventoryItemType GetItemTypeFromId(string id)
    {
        InventoryItemType result = InventoryItemType.Collectible;
        id = id.Trim();

        if (id.StartsWith("RE"))
        {
            result = InventoryItemType.Relic;
        }
        else if (id.StartsWith("RB"))
        {
            result = InventoryItemType.RosaryBead;
        }
        else if (id.StartsWith("QI"))
        {
            result = InventoryItemType.QuestItem;
        }
        else if (id.StartsWith("PR"))
        {
            result = InventoryItemType.Prayer;
        }
        else if (id.StartsWith("CO"))
        {
            result = InventoryItemType.Collectible;
        }
        else if (id.StartsWith("HE"))
        {
            result = InventoryItemType.SwordHeart;
        }
        else
        {
            ModLog.Error($"Unknown inventory item type for id: {id}");
        }

        return result;
    }
}
