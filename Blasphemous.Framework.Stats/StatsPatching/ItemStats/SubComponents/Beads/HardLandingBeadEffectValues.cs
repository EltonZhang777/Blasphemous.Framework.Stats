using Blasphemous.Framework.Stats.Components;
using Framework.Inventory;
using Blasphemous.NewbieEltonLibs.Extensions.GameLibs;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.SwordHearts;

/// <summary>
/// Effect for RB15: Piece of a Tombstone
/// </summary>
public class HardLandingBeadEffectValues : ObjectEffect_StatValues, IAccessible_Class<HardLandingBeadEffect>
{
    public float? fallRecoveryAnimationSpeed;

    public void GetValueFrom(HardLandingBeadEffect obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        fallRecoveryAnimationSpeed = TraverseUtils.GetValue<float>(obj, "AnimatorNormalizedSpeed", TraverseUtils.TraverseAccessType.Field);
    }

    public void SetValueTo(HardLandingBeadEffect obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        TraverseUtils.SetValueIfNotNull(ref obj, "AnimatorNormalizedSpeed", fallRecoveryAnimationSpeed, TraverseUtils.TraverseAccessType.Field);
    }

    public override void GetValueFrom(object obj)
    {
        if (obj is HardLandingBeadEffect t)
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
        if (obj is HardLandingBeadEffect t)
        {
            SetValueTo(t);
        }
        else
        {
            base.SetValueTo(obj);
        }
    }
}
