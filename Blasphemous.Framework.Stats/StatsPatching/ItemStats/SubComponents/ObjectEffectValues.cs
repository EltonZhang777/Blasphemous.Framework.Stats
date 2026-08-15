using Blasphemous.Framework.Stats.Components;
using Blasphemous.ModdingAPI;
using Framework.Inventory;
using System.Collections.Generic;
using Blasphemous.NewbieEltonLibs.Extensions.GameLibs;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents;

public class ObjectEffectValues : IAccessible_Class<ObjectEffect>, IAccessible_Polymorphic
{
    public ObjectEffect.EffectType? effectType;
    public string abilityName;
    public bool? limitTime;
    public float? effectTime;
    public bool? usePrayerDurationAddition;
    public bool? triggerOnlyOnce;
    public bool? onlyWhenUsingPrayer;
    public int? percentToExecute;
    public float? pingTime;
    public float? timeToWait;
    public bool? useWhenCastingPrayer;
    public List<ObjectEffect.Condition> conditions;
    public List<ObjectEffect.Condition> stoppingConditions;
    public string activationFxSound;

    /// <inheritdoc/>
    public virtual void GetValueFrom(ObjectEffect obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        effectType = TraverseUtils.GetValue<ObjectEffect.EffectType>(obj, "effectType", TraverseUtils.TraverseAccessType.Field);
        abilityName = TraverseUtils.GetValue<string>(obj, "abilityName", TraverseUtils.TraverseAccessType.Field);
        limitTime = TraverseUtils.GetValue<bool>(obj, "LimitTime", TraverseUtils.TraverseAccessType.Field);
        effectTime = TraverseUtils.GetValue<float>(obj, "EffectTime", TraverseUtils.TraverseAccessType.Field);
        usePrayerDurationAddition = TraverseUtils.GetValue<bool>(obj, "UsePrayerDurationAddition", TraverseUtils.TraverseAccessType.Field);
        triggerOnlyOnce = TraverseUtils.GetValue<bool>(obj, "TriggerOnlyOnce", TraverseUtils.TraverseAccessType.Field);
        onlyWhenUsingPrayer = TraverseUtils.GetValue<bool>(obj, "OnlyWhenUsingPrayer", TraverseUtils.TraverseAccessType.Field);
        percentToExecute = TraverseUtils.GetValue<int>(obj, "percentToExecute", TraverseUtils.TraverseAccessType.Field);
        pingTime = TraverseUtils.GetValue<float>(obj, "PingTime", TraverseUtils.TraverseAccessType.Field);
        timeToWait = TraverseUtils.GetValue<float>(obj, "TimeToWait", TraverseUtils.TraverseAccessType.Field);
        useWhenCastingPrayer = TraverseUtils.GetValue<bool>(obj, "UseWhenCastingPrayer", TraverseUtils.TraverseAccessType.Field);
        conditions = TraverseUtils.GetValue<List<ObjectEffect.Condition>>(obj, "Conditions", TraverseUtils.TraverseAccessType.Field);
        stoppingConditions = TraverseUtils.GetValue<List<ObjectEffect.Condition>>(obj, "StoppingConditions", TraverseUtils.TraverseAccessType.Field);
        activationFxSound = TraverseUtils.GetValue<string>(obj, "ActivationFxSound", TraverseUtils.TraverseAccessType.Field);
    }

    /// <inheritdoc/>
    public virtual void SetValueTo(ObjectEffect obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        TraverseUtils.SetValueIfNotNull(ref obj, "effectType", effectType, TraverseUtils.TraverseAccessType.Field);
        TraverseUtils.SetValueIfNotNull(ref obj, "abilityName", abilityName, TraverseUtils.TraverseAccessType.Field);
        TraverseUtils.SetValueIfNotNull(ref obj, "LimitTime", limitTime, TraverseUtils.TraverseAccessType.Field);
        TraverseUtils.SetValueIfNotNull(ref obj, "EffectTime", effectTime, TraverseUtils.TraverseAccessType.Field);
        TraverseUtils.SetValueIfNotNull(ref obj, "UsePrayerDurationAddition", usePrayerDurationAddition, TraverseUtils.TraverseAccessType.Field);
        TraverseUtils.SetValueIfNotNull(ref obj, "TriggerOnlyOnce", triggerOnlyOnce, TraverseUtils.TraverseAccessType.Field);
        TraverseUtils.SetValueIfNotNull(ref obj, "OnlyWhenUsingPrayer", onlyWhenUsingPrayer, TraverseUtils.TraverseAccessType.Field);
        TraverseUtils.SetValueIfNotNull(ref obj, "percentToExecute", percentToExecute, TraverseUtils.TraverseAccessType.Field);
        TraverseUtils.SetValueIfNotNull(ref obj, "PingTime", pingTime, TraverseUtils.TraverseAccessType.Field);
        TraverseUtils.SetValueIfNotNull(ref obj, "TimeToWait", timeToWait, TraverseUtils.TraverseAccessType.Field);
        TraverseUtils.SetValueIfNotNull(ref obj, "UseWhenCastingPrayer", useWhenCastingPrayer, TraverseUtils.TraverseAccessType.Field);
        TraverseUtils.SetValueIfNotNull(ref obj, "Conditions", conditions, TraverseUtils.TraverseAccessType.Field);
        TraverseUtils.SetValueIfNotNull(ref obj, "StoppingConditions", stoppingConditions, TraverseUtils.TraverseAccessType.Field);
        TraverseUtils.SetValueIfNotNull(ref obj, "ActivationFxSound", activationFxSound, TraverseUtils.TraverseAccessType.Field);
    }

    public virtual void GetValueFrom(object obj)
    {
        if (obj is ObjectEffect t)
        {
            GetValueFrom(t);
        }
        else
        {
            ModLog.Warn($"Error getting value from `{obj}` of type `{obj.GetType()}`! Expected type: {typeof(ObjectEffect)} or its derived types.");
        }
    }

    public virtual void SetValueTo(object obj)
    {
        if (obj is ObjectEffect t)
        {
            SetValueTo(t);
        }
        else
        {
            ModLog.Warn($"Error getting value from `{obj}` of type `{obj.GetType()}`! Expected type: {typeof(ObjectEffect)} or its derived types.");
        }
    }
}
