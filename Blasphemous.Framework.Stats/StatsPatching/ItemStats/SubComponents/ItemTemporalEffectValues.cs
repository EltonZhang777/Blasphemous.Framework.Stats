using Blasphemous.Framework.Stats.Components;
using System.Collections.Generic;
using Tools.Items;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents;

public class ItemTemporalEffectValues : ObjectEffectValues, IAccessible_Class<ItemTemporalEffect>
{
    public List<ItemTemporalEffect.PenitentEffects> temporalEffects;

    public void GetValueFrom(ItemTemporalEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        temporalEffects = Main.GetValue<List<ItemTemporalEffect.PenitentEffects>>(obj, "effects", Main.TraverseAccessType.Field);
    }

    public void SetValueTo(ItemTemporalEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        Main.SetValueIfNotNull(ref obj, "effects", temporalEffects, Main.TraverseAccessType.Field);
    }

    public override void GetValueFrom(object obj)
    {
        if (obj is ItemTemporalEffect t)
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
        if (obj is ItemTemporalEffect t)
        {
            SetValueTo(t);
        }
        else
        {
            base.SetValueTo(obj);
        }
    }
}
