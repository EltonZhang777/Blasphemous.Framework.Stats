using Blasphemous.Framework.Stats.Components;
using Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents;
using Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.Prayers;
using Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.Relics;
using Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.SwordHearts;
using Blasphemous.ModdingAPI;
using Framework.Inventory;
using Framework.Managers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Tools.Items;
using UnityEngine;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats;

/// <summary>
/// Stats of an of Inventory Item
/// </summary>
public class InventoryItemData : IAccessible_Class<BaseInventoryObject>, IStatsPatchable
{
    [JsonProperty(Required = Required.Always)]
    public string itemId;

    /// <summary>
    /// Can only read/write <see cref="BaseInventoryObject.carryonstart"/> and <see cref="BaseInventoryObject.preserveInNewGamePlus"/>
    /// </summary>
    public BaseInventoryObjectValues inheritenceSettings;

    /// <summary>
    /// Modifications to existing <see cref="ObjectEffect"/>s of the item, data read from the game will be stored here. 
    /// </summary>
    public List<ObjectEffectValues> effectModifications = new();

    /// <summary>
    /// List of <see cref="ObjectEffectValues"/> that will be deleted from the item.
    /// </summary>
    public List<ObjectEffectValues> effectDeletions = new();

    internal InventoryItemType itemType;

    protected BaseInventoryObject target;

    /// <summary>
    /// WIP temporary implementation
    /// </summary>
    internal List<MonoBehaviour> monoBehaviorScripts = new();

    internal List<ObjectEffect> ObjectEffects => monoBehaviorScripts.Where(x => x is ObjectEffect).Select(x => x as ObjectEffect).ToList();

    internal static readonly Dictionary<Type, Type> scriptTypeToJsonType = new()
    {
        // basic types
        { typeof(BaseInventoryObject), typeof(BaseInventoryObjectValues) },
        { typeof(ObjectEffect), typeof(ObjectEffectValues) },
        { typeof(ObjectEffect_Stat), typeof(ObjectEffect_StatValues) },
        { typeof(ObjectEffect_ChangeItem), typeof(ObjectEffect_ChangeItemValues) },
        { typeof(ItemGhostTrail), typeof(ItemGhostTrailValues) },
        { typeof(ItemFlag), typeof(ItemFlagValues) },
        { typeof(ItemTemporalEffect), typeof(ItemTemporalEffectValues)},
        
        // beads
        { typeof(BloodPenitenceBeadEffect), typeof(BloodPenitenceBeadEffectValues) },
        { typeof(GuiltPenitenceBeadEffect), typeof(ObjectEffectValues) },
        { typeof(CloisteredGemBeadEffect), typeof(CloisteredGemBeadEffectValues) },
        { typeof(QuickHealingBeadEffect), typeof(QuickHealingBeadEffectValues) },
        { typeof(QuickAreaTransformBeadEffect), typeof(QuickAreaTransformBeadEffectValues) },
        { typeof(HardLandingBeadEffect), typeof(HardLandingBeadEffectValues) },
        { typeof(BidirectionalParryBeadEffect), typeof(ObjectEffectValues) },
        { typeof(IncreaseSpeedBeadEffect), typeof(IncreaseSpeedBeadEffectValues) },
        { typeof(FamiliarSpawnEffect), typeof(FamiliarSpawnEffectValues) },

        // prayers
        { typeof(PenitentLightBeamEffect), typeof(PenitentLightBeamEffectValues) },
        { typeof(PenitentMultishotEffect), typeof(PenitentMultishotEffectValues) },
        { typeof(PrayerShieldEffect), typeof(PrayerShieldEffectValues) },
        { typeof(PenitentDivineLightEffect), typeof(PenitentDivineLightEffectValues) },
        { typeof(HeavyAttackPrayerEffect), typeof(ObjectEffect_StatValues) },
        { typeof(PrayerGhostGuardian), typeof(PrayerGhostGuardianValues) },
        { typeof(PenitentGuardianEffect), typeof(PenitentGuardianEffectValues) },
        { typeof(PenitentAreaAttack), typeof(PenitentAreaAttackValues) },
        { typeof(PenitentCrawlerOrbsEffect), typeof(PenitentCrawlerOrbsEffectValues) },
        { typeof(ToxicCloudEffect), typeof(ToxicCloudEffectValues) },
        { typeof(ZambraTearsHarvestEffect), typeof(ZambraTearsHarvestEffectValues) },
        { typeof(PrayerMiriam), typeof(PrayerMiriamValues) },
        { typeof(PenitentTeleportToPriedieu), typeof(ObjectEffectValues) },
        { typeof(PR203ElmFireLoopEffect), typeof(PR203ElmFireLoopEffectValues) },

        // sword hearts
        { typeof(IncreaseSpeedSwordHeartEffect), typeof(IncreaseSpeedSwordHeartEffectValues) },
        { typeof(PowerSlashesSwordHeartEffect), typeof(ObjectEffectValues) },

        // relics and quest items
        { typeof(ChaliceEffect), typeof(ChaliceEffectValues) },
    };

    internal enum InventoryItemType
    {
        Relic,
        RosaryBead,
        QuestItem,
        Prayer,
        Collectible,
        SwordHeart
    }

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
        if (!Main.Validate(obj, x => x != null))
            return;

        monoBehaviorScripts = obj.gameObject.GetComponents<MonoBehaviour>()?.ToList();
        if (monoBehaviorScripts == null)
        {
            ModLog.Error($"No MonoBehaviour scripts found on object with ID: {itemId}!");
            return;
        }

        foreach (MonoBehaviour mb in monoBehaviorScripts)
        {
            object scriptData = CreateJsonInstanceFromScriptType(mb.GetType());
#if DEBUG
            ModLog.Warn($"Starting to get value for `{mb}` of type {mb.GetType()}!");
#endif
            switch (scriptData)
            {
                case ObjectEffectValues data:
#if DEBUG
                    ModLog.Warn($"Getting ObjectEffectValues of derived type `{data.GetType()}`!");
#endif
                    data.GetValueFrom((ObjectEffect)mb);
                    effectModifications.Add(data);
                    break;
                case BaseInventoryObjectValues data:
#if DEBUG
                    ModLog.Warn($"Getting BaseInventoryObjectValues!");
#endif
                    data.GetValueFrom((BaseInventoryObject)mb);
                    inheritenceSettings = data;
                    break;
                default:
#if DEBUG
                    ModLog.Error($"Skipping value reading of type `{mb.GetType()}`!");
#endif
                    break;
            }
        }
    }

    /// <inheritdoc/>
    public void GetValueFromFirstTarget()
    {
        GetValueFrom(target);
    }

    /// <inheritdoc/>
    public void SetValueTo(BaseInventoryObject obj)
    {
        // WIP
        if (!Main.Validate(obj, x => x != null))
            return;

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

    internal static object CreateJsonInstanceFromScriptType(Type scriptType)
    {
        return CreateJsonInstanceFromScriptType<object>(scriptType);
    }

    internal static TResult CreateJsonInstanceFromScriptType<TResult>(Type scriptType)
    {
        if (TryFindTypeOrBaseTypeFromList(scriptType, scriptTypeToJsonType.Keys.ToList(), out Type targetType))
        {
            Type jsonType = scriptTypeToJsonType[targetType];
#if DEBUG
            ModLog.Warn($"JSON type: {jsonType}");
#endif
            return (TResult)jsonType.GetConstructor(Type.EmptyTypes)?.Invoke(null);
        }
        else
        {
            ModLog.Error($"No JSON type found for script type: {scriptType}");
            return default(TResult);
        }
    }

    internal static bool TryFindTypeOrBaseTypeFromList(Type targetType, List<Type> typeList, out Type outputType)
    {
        // Check for an exact match
        if (typeList.Contains(targetType))
        {
            outputType = targetType;
            return true;
        }

        // If no exact match is found, heck for the most derived base type
        Type baseType = targetType.BaseType;
        while (baseType != null)
        {
            if (typeList.Contains(baseType))
            {
                outputType = baseType;
                return true;
            }
            baseType = baseType.BaseType;
        }

        outputType = null;
        return false;
    }
}
