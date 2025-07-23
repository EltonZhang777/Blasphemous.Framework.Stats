using Blasphemous.Framework.Stats.Components;
using Tools.Items;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.SwordHearts;

/// <summary>
/// Effect for RB102: Reliquary of the Suffering Heart
/// </summary>
public class BloodPenitenceBeadEffectValues : ObjectEffectValues, IAccessible_Class<BloodPenitenceBeadEffect>
{
    public float? regenFactorIncrease;

    public void GetValueFrom(BloodPenitenceBeadEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        regenFactorIncrease = Main.GetValue<BloodPenitenceBeadEffect, float>(obj, "regenFactorIncrease", Main.TraverseAccessType.Field);
    }

    public void SetValueTo(BloodPenitenceBeadEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        Main.SetValueIfNotNull(ref obj, "regenFactorIncrease", regenFactorIncrease, Main.TraverseAccessType.Field);
    }
}
