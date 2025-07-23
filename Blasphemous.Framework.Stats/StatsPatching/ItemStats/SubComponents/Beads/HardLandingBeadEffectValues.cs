using Blasphemous.Framework.Stats.Components;
using Framework.Inventory;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.SwordHearts;

/// <summary>
/// Effect for RB15: Piece of a Tombstone
/// </summary>
public class HardLandingBeadEffectValues : ObjectEffect_StatValues, IAccessible_Class<HardLandingBeadEffect>
{
    public float? animatorNormalizedSpeed;

    public void GetValueFrom(HardLandingBeadEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        animatorNormalizedSpeed = Main.GetValue<HardLandingBeadEffect, float>(obj, "AnimatorNormalizedSpeed", Main.TraverseAccessType.Field);
    }

    public void SetValueTo(HardLandingBeadEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        Main.SetValueIfNotNull(ref obj, "AnimatorNormalizedSpeed", animatorNormalizedSpeed, Main.TraverseAccessType.Field);
    }
}
