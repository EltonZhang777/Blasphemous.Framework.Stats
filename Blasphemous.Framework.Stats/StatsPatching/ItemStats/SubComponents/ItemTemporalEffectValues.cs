using Blasphemous.Framework.Stats.Components;
using System.Collections.Generic;
using Tools.Items;
using Blasphemous.NewbieEltonLibs.Extensions.GameLibs;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents;

public class ItemTemporalEffectValues : ObjectEffectValues, IAccessible_Class<ItemTemporalEffect>
{
    public List<ItemTemporalEffect.PenitentEffects> temporalEffects;

    public void GetValueFrom(ItemTemporalEffect obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        temporalEffects = TraverseUtils.GetValue<List<ItemTemporalEffect.PenitentEffects>>(obj, "effects", TraverseUtils.TraverseAccessType.Field);
    }

    public void SetValueTo(ItemTemporalEffect obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        TraverseUtils.SetValueIfNotNull(ref obj, "effects", temporalEffects, TraverseUtils.TraverseAccessType.Field);
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
