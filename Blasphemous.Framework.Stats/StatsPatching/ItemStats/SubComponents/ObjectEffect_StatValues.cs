using Blasphemous.Framework.Stats.Components;
using Framework.Inventory;
using Gameplay.GameControllers.Entities;

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
        if (!Main.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        useHitAsBaseValue = Main.GetValue<ObjectEffect_Stat, bool>(obj, "UseHitAsBaseValue", Main.TraverseAccessType.Field);
        effectMode = Main.GetValue<ObjectEffect_Stat, ObjectEffect_Stat.EffectMode>(obj, "effectMode", Main.TraverseAccessType.Field);
        statType = Main.GetValue<ObjectEffect_Stat, EntityStats.StatsTypes>(obj, "statType", Main.TraverseAccessType.Field);
        valueType = Main.GetValue<ObjectEffect_Stat, ObjectEffect_Stat.ValueType>(obj, "valueType", Main.TraverseAccessType.Field);
        statValueType = Main.GetValue<ObjectEffect_Stat, EntityStats.StatsTypes>(obj, "statValueType", Main.TraverseAccessType.Field);
        value = Main.GetValue<ObjectEffect_Stat, float>(obj, "value", Main.TraverseAccessType.Field);
        multiplier = Main.GetValue<ObjectEffect_Stat, float>(obj, "multiplier", Main.TraverseAccessType.Field);
    }

    /// <inheritdoc/>
    public void SetValueTo(ObjectEffect_Stat obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        Main.SetValueIfNotNull(ref obj, "UseHitAsBaseValue", useHitAsBaseValue, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "effectMode", effectMode, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "statType", statType, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "valueType", valueType, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "statValueType", statValueType, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "value", value, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "multiplier", multiplier, Main.TraverseAccessType.Field);
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
