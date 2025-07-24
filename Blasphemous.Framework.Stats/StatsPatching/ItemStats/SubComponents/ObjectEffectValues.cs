using Blasphemous.Framework.Stats.Components;
using Blasphemous.ModdingAPI;
using Framework.Inventory;
using System.Collections.Generic;

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
        if (!Main.Validate(obj, x => x != null))
            return;

        effectType = Main.GetValue<ObjectEffect, ObjectEffect.EffectType>(obj, "effectType", Main.TraverseAccessType.Field);
        abilityName = Main.GetValue<ObjectEffect, string>(obj, "abilityName", Main.TraverseAccessType.Field);
        limitTime = Main.GetValue<ObjectEffect, bool>(obj, "LimitTime", Main.TraverseAccessType.Field);
        effectTime = Main.GetValue<ObjectEffect, float>(obj, "EffectTime", Main.TraverseAccessType.Field);
        usePrayerDurationAddition = Main.GetValue<ObjectEffect, bool>(obj, "UsePrayerDurationAddition", Main.TraverseAccessType.Field);
        triggerOnlyOnce = Main.GetValue<ObjectEffect, bool>(obj, "TriggerOnlyOnce", Main.TraverseAccessType.Field);
        onlyWhenUsingPrayer = Main.GetValue<ObjectEffect, bool>(obj, "OnlyWhenUsingPrayer", Main.TraverseAccessType.Field);
        percentToExecute = Main.GetValue<ObjectEffect, int>(obj, "percentToExecute", Main.TraverseAccessType.Field);
        pingTime = Main.GetValue<ObjectEffect, float>(obj, "PingTime", Main.TraverseAccessType.Field);
        timeToWait = Main.GetValue<ObjectEffect, float>(obj, "TimeToWait", Main.TraverseAccessType.Field);
        useWhenCastingPrayer = Main.GetValue<ObjectEffect, bool>(obj, "UseWhenCastingPrayer", Main.TraverseAccessType.Field);
        conditions = Main.GetValue<ObjectEffect, List<ObjectEffect.Condition>>(obj, "Conditions", Main.TraverseAccessType.Field);
        stoppingConditions = Main.GetValue<ObjectEffect, List<ObjectEffect.Condition>>(obj, "StoppingConditions", Main.TraverseAccessType.Field);
        activationFxSound = Main.GetValue<ObjectEffect, string>(obj, "ActivationFxSound", Main.TraverseAccessType.Field);
    }

    /// <inheritdoc/>
    public virtual void SetValueTo(ObjectEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        Main.SetValueIfNotNull(ref obj, "effectType", effectType, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "abilityName", abilityName, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "LimitTime", limitTime, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "EffectTime", effectTime, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "UsePrayerDurationAddition", usePrayerDurationAddition, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "TriggerOnlyOnce", triggerOnlyOnce, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "OnlyWhenUsingPrayer", onlyWhenUsingPrayer, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "percentToExecute", percentToExecute, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "PingTime", pingTime, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "TimeToWait", timeToWait, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "UseWhenCastingPrayer", useWhenCastingPrayer, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "Conditions", conditions, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "StoppingConditions", stoppingConditions, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "ActivationFxSound", activationFxSound, Main.TraverseAccessType.Field);
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
