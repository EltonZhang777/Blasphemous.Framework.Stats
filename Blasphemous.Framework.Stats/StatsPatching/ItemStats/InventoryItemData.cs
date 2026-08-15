using Blasphemous.Framework.Stats.Components;
using Blasphemous.NewbieEltonLibs.Extensions.GameLibs;
using Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents;
using Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.Beads;
using Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.Prayers;
using Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.Relics;
using Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.SwordHearts;
using Blasphemous.ModdingAPI;
using Framework.Inventory;
using Framework.Managers;
using HarmonyLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Tools.Items;
using UnityEngine;
using static Blasphemous.NewbieEltonLibs.Extensions.GameLibs.InventoryManagerExtensions;
using static Framework.Managers.InventoryManager;
using Blasphemous.NewbieEltonLibs.Extensions.ModdingAPI;

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
    /// Fervour cost for prayers
    /// </summary>
    public int? fervourCost;

    /// <summary>
    /// List of <see cref="ObjectEffectValues"/> that will be added to the item.
    /// </summary>
    public List<ObjectEffectValues> effectAdditions = [];

    /// <summary>
    /// List of <see cref="ObjectEffectValues"/> that will be deleted from the item.
    /// </summary>
    public List<ObjectEffectValues> effectDeletions = [];

    /// <summary>
    /// List of <see cref="ObjectEffectValues"/> that are serialized from vanilla MonoBehavior scripts.
    /// </summary>
    public List<ObjectEffectValues> vanillaEffects = [];

    protected BaseInventoryObject target;

    internal bool isApplied = false;

    /// <summary>
    /// All the vanilla MonoBehaviors attached to the inventory object's GameObject
    /// </summary>
    internal List<MonoBehaviour> vanillaMonoBehaviors = [];

    /// <summary>
    /// Vanilla <see cref="ObjectEffect"/>s of the item, serialized as <see cref="ObjectEffectValues"/> for inspection. 
    /// Dictionary value indicates whether this effect is deleted by the deletion process.
    /// </summary>
    internal Dictionary<ObjectEffectValues, bool> vanillaEffectsToIsDeleted = [];

    /// <summary>
    /// All the mod-added MonoBehaviors attached to the inventory object's GameObject
    /// </summary>
    internal List<MonoBehaviour> modMonoBehaviors = [];

    /// <summary>
    /// Dictionary to map <see cref="ObjectEffectValues"/> to their corresponding MonoBehaviors.
    /// </summary>
    internal Dictionary<ObjectEffectValues, MonoBehaviour> serializedObjectsToVanillaMonoBehaviors = [];

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
        { typeof(GuiltPenitenceBeadEffect), typeof(GuiltPenitenceBeadEffectValues) },
        { typeof(CloisteredGemBeadEffect), typeof(CloisteredGemBeadEffectValues) },
        { typeof(QuickHealingBeadEffect), typeof(QuickHealingBeadEffectValues) },
        { typeof(QuickAreaTransformBeadEffect), typeof(QuickAreaTransformBeadEffectValues) },
        { typeof(HardLandingBeadEffect), typeof(HardLandingBeadEffectValues) },
        { typeof(BidirectionalParryBeadEffect), typeof(BidirectionalParryBeadEffectValues) },
        { typeof(IncreaseSpeedBeadEffect), typeof(IncreaseSpeedBeadEffectValues) },
        { typeof(FamiliarSpawnEffect), typeof(FamiliarSpawnEffectValues) },

        // prayers
        { typeof(PenitentLightBeamEffect), typeof(PenitentLightBeamEffectValues) },
        { typeof(PenitentMultishotEffect), typeof(PenitentMultishotEffectValues) },
        { typeof(PrayerShieldEffect), typeof(PrayerShieldEffectValues) },
        { typeof(PenitentDivineLightEffect), typeof(PenitentDivineLightEffectValues) },
        { typeof(HeavyAttackPrayerEffect), typeof(HeavyAttackPrayerEffectValues) },
        { typeof(PrayerGhostGuardian), typeof(PrayerGhostGuardianValues) },
        { typeof(PenitentGuardianEffect), typeof(PenitentGuardianEffectValues) },
        { typeof(PenitentAreaAttack), typeof(PenitentAreaAttackValues) },
        { typeof(PenitentCrawlerOrbsEffect), typeof(PenitentCrawlerOrbsEffectValues) },
        { typeof(ToxicCloudEffect), typeof(ToxicCloudEffectValues) },
        { typeof(ZambraTearsHarvestEffect), typeof(ZambraTearsHarvestEffectValues) },
        { typeof(PrayerMiriam), typeof(PrayerMiriamValues) },
        { typeof(PenitentTeleportToPriedieu), typeof(PenitentTeleportToPriedieuValues) },
        { typeof(PR203ElmFireLoopEffect), typeof(PR203ElmFireLoopEffectValues) },

        // sword hearts
        { typeof(IncreaseSpeedSwordHeartEffect), typeof(IncreaseSpeedSwordHeartEffectValues) },
        { typeof(PowerSlashesSwordHeartEffect), typeof(PowerSlashesSwordHeartEffectValues) },

        // relics and quest items
        { typeof(ChaliceEffect), typeof(ChaliceEffectValues) },
    };

    /// <summary>
    /// Stores vanilla fervour cost, if this inventory object is a prayer.
    /// </summary>
    private int _vanillaFervourCost;

    internal static Dictionary<Type, Type> JsonTypeToScriptType
    {
        get
        {
            Dictionary<Type, Type> reversedDict = [];
            foreach (KeyValuePair<Type, Type> kvp in scriptTypeToJsonType)
            {
                reversedDict[kvp.Value] = kvp.Key;
            }
            return reversedDict;
        }
    }

    internal Dictionary<MonoBehaviour, ObjectEffectValues> MonoBehaviorsToSerializedObjects
    {
        get
        {
            Dictionary<MonoBehaviour, ObjectEffectValues> reversedDict = [];
            foreach (KeyValuePair<ObjectEffectValues, MonoBehaviour> kvp in serializedObjectsToVanillaMonoBehaviors)
            {
                reversedDict[kvp.Value] = kvp.Key;
            }
            return reversedDict;
        }
    }

    internal List<ObjectEffect> ObjectEffects => vanillaMonoBehaviors.Where(x => x is ObjectEffect).Select(x => x as ObjectEffect).ToList();

    internal ItemType ItemType => Core.InventoryManager.GetItemTypeFromId(itemId);

    [JsonConstructor]
    public InventoryItemData()
    {
    }

    public InventoryItemData(string id)
    {
        itemId = id;
    }

    /// <inheritdoc/>
    public void GetValueFrom(BaseInventoryObject obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        vanillaEffects.Clear();
        vanillaEffectsToIsDeleted.Clear();
        serializedObjectsToVanillaMonoBehaviors.Clear();

        // Get MonoBehaviors attached to the inventory object
        vanillaMonoBehaviors = obj.gameObject.GetComponents<MonoBehaviour>()?.ToList();
        if (vanillaMonoBehaviors == null || vanillaMonoBehaviors?.Count == 0)
        {
            ModLog.Error($"No MonoBehaviour scripts found on object with ID: {itemId}!");
            return;
        }

        // serialize each MonoBehavior
        foreach (MonoBehaviour mb in vanillaMonoBehaviors)
        {
            object scriptData = CreateJsonInstanceFromScriptType(mb.GetType());
            ModLogExtensions.WarnIfDebugBuild($"Starting to get value for `{mb}` of type {mb.GetType()}!");
            switch (scriptData)
            {
                case ObjectEffectValues data:
                    ModLogExtensions.WarnIfDebugBuild($"Getting ObjectEffectValues of derived type `{data.GetType()}`!");
                    data.GetValueFrom((object)mb);
                    vanillaEffects.Add(data);
                    vanillaEffectsToIsDeleted.Add(data, false);
                    serializedObjectsToVanillaMonoBehaviors.Add(data, mb);
                    break;
                case BaseInventoryObjectValues data:
                    ModLogExtensions.WarnIfDebugBuild($"Getting BaseInventoryObjectValues!");
                    data.GetValueFrom((BaseInventoryObject)mb);
                    inheritenceSettings = data;

                    // if this is a prayer, get fervour cost
                    if ((mb as Prayer) != null)
                    {
                        Prayer prayer = mb as Prayer;
                        fervourCost = prayer.fervourNeeded;
                        _vanillaFervourCost = prayer.fervourNeeded;
                    }

                    break;
                default:
                    ModLogExtensions.ErrorIfDebugBuild($"Skipping value reading of unpatchable type `{mb.GetType()}`!");
                    break;
            }
        }
    }

    /// <inheritdoc/>
    public void GetValueFromFirstTarget()
    {
        if (!TryGetTargets())
        {
            ModLog.Warn($"No targets found when patching `{itemId}`!");
            return;
        }
        GetValueFrom(target);
    }

    /// <inheritdoc/>
    public void SetValueTo(BaseInventoryObject obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        isApplied = true;

        // Write inheritenceSettings before it is overwritten by the following data backup step
        inheritenceSettings?.SetValueTo(obj);

        // Apply fervour cost change before it is overwritten by the following data backup step
        if (fervourCost.HasValue && obj is Prayer prayer)
        {
            prayer.fervourNeeded = fervourCost.Value;
        }

        // Get MonoBehaviors attached to the inventory object and serialize them for comparison
        if (vanillaMonoBehaviors == null || vanillaMonoBehaviors?.Count == 0)
        {
            GetValueFrom(obj);
        }

        // Apply deletions before additions
        foreach (ObjectEffectValues vanillaEffect in vanillaEffectsToIsDeleted.Keys.ToList())
        {
            foreach (ObjectEffectValues deletion in effectDeletions)
            {
#if DEBUG
                JsonSerializerSettings jsonSerializerSettings = new()
                {
                    Converters = [
                        new StringEnumConverter(),
                ],
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    PreserveReferencesHandling = PreserveReferencesHandling.None,
                };
                ModLog.Info($"Checking if \n`{JsonConvert.SerializeObject(vanillaEffect, Formatting.Indented, jsonSerializerSettings)}`\n matches deletion condition \n`{JsonConvert.SerializeObject(deletion, Formatting.Indented, jsonSerializerSettings)}`\n for inventory object `{obj.id}`!");
#endif

                if (!ObjectEffectValuesMatchEvaluator(vanillaEffect, deletion))
                    continue;
                ModLogExtensions.InfoIfDebugBuild($"the evaluator matches current vanillaEffect!");

                // This effect matches the delete condition, destroy its MonoBehavior
                if (!serializedObjectsToVanillaMonoBehaviors.TryGetValue(vanillaEffect, out MonoBehaviour mb))
                    continue;

                Traverse.Create(mb).Method("OnUnEquipInventoryObject").GetValue(null);  // disable the effect if it is activated
                MonoBehaviour.Destroy(mb);
                vanillaEffectsToIsDeleted[vanillaEffect] = true;  // update deleted status to dictionary
                ModLogExtensions.WarnIfDebugBuild($"Disabling MonoBehavior of type `{mb.GetType()}` to inventory object `{obj.id}`!");
                break;
            }
        }

        // Apply additions
        foreach (ObjectEffectValues addition in effectAdditions)
        {
            if (!TryFindTypeOrBaseTypeFromList(addition.GetType(), JsonTypeToScriptType.Keys.ToList(), out Type finalJsonType))
            {
                ModLog.Error($"Failed to create MonoBehavior from JSON type `{addition.GetType()}`!");
                continue;
            }
            MonoBehaviour mb = obj.gameObject.AddComponent(JsonTypeToScriptType[finalJsonType]) as MonoBehaviour;
            if (mb == null)
            {
                ModLog.Error($"Failed to create MonoBehavior from JSON type `{addition.GetType()}`!");
                continue;
            }
            ModLogExtensions.WarnIfDebugBuild($"Adding MonoBehavior of type `{mb.GetType()}` to inventory object `{obj.id}`!");
            addition.SetValueTo(mb);
            modMonoBehaviors.Add(mb);
        }
    }

    /// <inheritdoc/>
    public void SetValueToAllTargets()
    {
        if (!TryGetTargets())
        {
            ModLog.Warn($"No targets found when patching `{itemId}`!");
            return;
        }
        SetValueTo(target);
    }

    /// <summary>
    /// Revert all changes done to the inventory object
    /// </summary>
    public void RevertValueTo(BaseInventoryObject obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        isApplied = false;

        // Revert inheritenceSettings
        new BaseInventoryObjectValues() { carryOnStart = false, preserveInNewGamePlus = true }.SetValueTo(obj);

        // Revert fervour cost
        if (obj is Prayer prayer)
        {
            prayer.fervourNeeded = _vanillaFervourCost;
        }

        // Revert additions
        foreach (MonoBehaviour mb in modMonoBehaviors)
        {
            MonoBehaviour.Destroy(mb);
        }

        // Revert deletions
        foreach (ObjectEffectValues vanillaEffect in vanillaEffectsToIsDeleted.Keys.Where(x => vanillaEffectsToIsDeleted[x] == true))
        {
            if (vanillaEffect == null)
            {
                ModLog.Error($"Serialized vanilla MonoBehavior is null for inventory object `{obj.id}`!");
                continue;
            }

            // Recreate the MonoBehavior from the serialized data
            if (!TryFindTypeOrBaseTypeFromList(vanillaEffect.GetType(), JsonTypeToScriptType.Keys.ToList(), out Type finalJsonType))
            {
                ModLog.Error($"Failed to create MonoBehavior from JSON type `{vanillaEffect.GetType()}` for inventory object `{obj.id}`!");
                continue;
            }

            MonoBehaviour mb = obj.gameObject.AddComponent(JsonTypeToScriptType[finalJsonType]) as MonoBehaviour;
            if (mb == null)
            {
                ModLog.Error($"Failed to create MonoBehavior from JSON type `{JsonTypeToScriptType[finalJsonType]}` for inventory object `{obj.id}`!");
                continue;
            }

            vanillaEffect.SetValueTo(mb);
            ModLogExtensions.WarnIfDebugBuild($"Successfully reverted vanilla ObjectEffect `{mb}` of type `{mb.GetType()}`!");
        }
    }

    /// <summary>
    /// Revert all changes done by the patch
    /// </summary>
    public void RevertValueToAllTargets()
    {
        RevertValueTo(target);
    }

    /// <inheritdoc/>
    public bool TryGetTargets()
    {
        target = Core.InventoryManager.GetInventoryItemFromId(itemId);
        return target != null;
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
            ModLogExtensions.WarnIfDebugBuild($"JSON type: {jsonType}");
            return (TResult)jsonType.GetConstructor(Type.EmptyTypes)?.Invoke(null);
        }
        else
        {
            ModLog.Error($"No JSON type found for script type: {scriptType}");
            return default;
        }
    }

    internal static object CreateScriptInstanceFromJsonType(Type jsonType)
    {
        return CreateScriptInstanceFromJsonType<object>(jsonType);
    }

    internal static TResult CreateScriptInstanceFromJsonType<TResult>(Type jsonType)
    {
        if (TryFindTypeOrBaseTypeFromList(jsonType, JsonTypeToScriptType.Keys.ToList(), out Type targetType))
        {
            Type scriptType = JsonTypeToScriptType[targetType];
            ModLogExtensions.WarnIfDebugBuild($"script type: {scriptType}");
            return (TResult)scriptType.GetConstructor(Type.EmptyTypes)?.Invoke(null);
        }
        else
        {
            ModLog.Error($"No JSON type found for json type: {jsonType}");
            return default;
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

    /// <summary>
    /// First check type, if the <paramref name="target"/> and <paramref name="conditions"/> are not of the same type, return false. 
    /// Then check if the non-null fields of <paramref name="conditions"/> object matches the corresponding fields of <paramref name="target"/>. 
    /// If all the checked fields match, returns true.
    /// </summary>
    internal static bool ObjectEffectValuesMatchEvaluator(ObjectEffectValues target, ObjectEffectValues conditions)
    {
        // Check type match
        if (target.GetType().Name != conditions.GetType().Name)
            return false;

        // Check non-null properties of the conditions object
        List<FieldInfo> conditionsToBeChecked = conditions.GetType()
            .GetFields(BindingFlags.Public | BindingFlags.Instance)
            .Where(x => x.GetValue(conditions) != null)
            .ToList();
#if DEBUG
        foreach (FieldInfo item in conditionsToBeChecked)
        {
            ModLog.Info($"condition field to be checked: {item.Name}");
        }
#endif
        if (conditionsToBeChecked.Count == 0)
        {
            ModLog.Info($"No fields to be checked in the evaluator! Returning false");
            return false;
        }
        foreach (FieldInfo conditionField in conditionsToBeChecked)
        {
            object targetFieldValue = target.GetType().GetField(conditionField.Name).GetValue(target);
            object conditionFieldValue = conditionField.GetValue(conditions);
            ModLogExtensions.InfoIfDebugBuild($"Checking field `{conditionField.Name}`! \n  `{targetFieldValue}` V.S. `{conditionFieldValue}` = {targetFieldValue.Equals(conditionFieldValue)}");
            if (!targetFieldValue.Equals(conditionFieldValue))
                return false;
        }
        return true;
    }
}
