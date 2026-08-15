using Blasphemous.Framework.Stats.Components;
using Tools.Items;
using Blasphemous.NewbieEltonLibs.Extensions.GameLibs;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.SwordHearts;

/// <summary>
/// Effect for RB102: Reliquary of the Suffering Heart
/// </summary>
public class BloodPenitenceBeadEffectValues : ObjectEffectValues, IAccessible_Class<BloodPenitenceBeadEffect>
{
    public float? regenFactorIncrease;

    public void GetValueFrom(BloodPenitenceBeadEffect obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        regenFactorIncrease = TraverseUtils.GetValue<float>(obj, "regenFactorIncrease", TraverseUtils.TraverseAccessType.Field);
    }

    public void SetValueTo(BloodPenitenceBeadEffect obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        TraverseUtils.SetValueIfNotNull(ref obj, "regenFactorIncrease", regenFactorIncrease, TraverseUtils.TraverseAccessType.Field);
    }

    public override void GetValueFrom(object obj)
    {
        if (obj is BloodPenitenceBeadEffect t)
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
        if (obj is BloodPenitenceBeadEffect t)
        {
            SetValueTo(t);
        }
        else
        {
            base.SetValueTo(obj);
        }
    }
}
