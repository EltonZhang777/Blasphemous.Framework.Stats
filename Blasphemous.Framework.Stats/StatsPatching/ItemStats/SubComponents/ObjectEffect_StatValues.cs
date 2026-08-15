using Blasphemous.Framework.Stats.Components;
using Framework.Inventory;
using Gameplay.GameControllers.Entities;
using Blasphemous.NewbieEltonLibs.Extensions.GameLibs;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents;

public class ObjectEffect_StatValues : ObjectEffectValues, IAccessible_Class<ObjectEffect_Stat>, IAccessible_Polymorphic
{
    public bool? useHitAsBaseValue;
    public ObjectEffect_Stat.EffectMode? effectMode;
    public EntityStats.StatsTypes? statType;
    public ObjectEffect_Stat.ValueType? valueType;
    public EntityStats.StatsTypes? statValueType;
    public float? value;
    public float? multiplier;

    /// <inheritdoc/>
    public void GetValueFrom(ObjectEffect_Stat obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        useHitAsBaseValue = TraverseUtils.GetValue<bool>(obj, "UseHitAsBaseValue", TraverseUtils.TraverseAccessType.Field);
        effectMode = TraverseUtils.GetValue<ObjectEffect_Stat.EffectMode>(obj, "effectMode", TraverseUtils.TraverseAccessType.Field);
        statType = TraverseUtils.GetValue<EntityStats.StatsTypes>(obj, "statType", TraverseUtils.TraverseAccessType.Field);
        valueType = TraverseUtils.GetValue<ObjectEffect_Stat.ValueType>(obj, "valueType", TraverseUtils.TraverseAccessType.Field);
        statValueType = TraverseUtils.GetValue<EntityStats.StatsTypes>(obj, "statValueType", TraverseUtils.TraverseAccessType.Field);
        value = TraverseUtils.GetValue<float>(obj, "value", TraverseUtils.TraverseAccessType.Field);
        multiplier = TraverseUtils.GetValue<float>(obj, "multiplier", TraverseUtils.TraverseAccessType.Field);
    }

    /// <inheritdoc/>
    public void SetValueTo(ObjectEffect_Stat obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        TraverseUtils.SetValueIfNotNull(ref obj, "UseHitAsBaseValue", useHitAsBaseValue, TraverseUtils.TraverseAccessType.Field);
        TraverseUtils.SetValueIfNotNull(ref obj, "effectMode", effectMode, TraverseUtils.TraverseAccessType.Field);
        TraverseUtils.SetValueIfNotNull(ref obj, "statType", statType, TraverseUtils.TraverseAccessType.Field);
        TraverseUtils.SetValueIfNotNull(ref obj, "valueType", valueType, TraverseUtils.TraverseAccessType.Field);
        TraverseUtils.SetValueIfNotNull(ref obj, "statValueType", statValueType, TraverseUtils.TraverseAccessType.Field);
        TraverseUtils.SetValueIfNotNull(ref obj, "value", value, TraverseUtils.TraverseAccessType.Field);
        TraverseUtils.SetValueIfNotNull(ref obj, "multiplier", multiplier, TraverseUtils.TraverseAccessType.Field);
    }

    public override void GetValueFrom(object obj)
    {
        if (obj is ObjectEffect_Stat t)
        {
            GetValueFrom(t);
        }
        else
        {
            base.GetValueFrom(obj);
        }
    }

    public override void SetValueTo(object obj)
    {
        if (obj is ObjectEffect_Stat t)
        {
            SetValueTo(t);
        }
        else
        {
            base.SetValueTo(obj);
        }
    }
}
