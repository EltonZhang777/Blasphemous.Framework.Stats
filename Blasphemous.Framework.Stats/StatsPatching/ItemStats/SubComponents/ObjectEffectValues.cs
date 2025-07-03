using Blasphemous.Framework.Stats.Components;
using Framework.Inventory;
using System;
using System.Collections.Generic;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents;

public class ObjectEffectValues : IAccessible<BlasAttribute>
{
    public ObjectEffect.EffectType? effectType;
    public string abilityName;
    public bool? limitTime;
    public float? effectTime;
    public bool? UsePrayerDurationAddition;
    public bool? TriggerOnlyOnce;
    public bool? OnlyWhenUsingPrayer;
    public int? percentToExecute;
    public float? pingTime;
    public float? timeToWait;
    public bool? useWhenCastingPrayer;
    public List<ObjectEffect.Condition> conditions;
    public List<ObjectEffect.Condition> stoppingConditions;
    public string activationFxSound;

    public void GetValueFrom(BlasAttribute obj)
    {
        throw new NotImplementedException();
    }

    public void SetValueTo(BlasAttribute obj)
    {
        throw new NotImplementedException();
    }
}
