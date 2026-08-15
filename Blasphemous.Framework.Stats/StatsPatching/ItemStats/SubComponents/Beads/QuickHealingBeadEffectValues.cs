using Blasphemous.Framework.Stats.Components;
using Framework.Inventory;
using Blasphemous.NewbieEltonLibs.Extensions.GameLibs;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.SwordHearts;

/// <summary>
/// Effect for RB106: Bead of Gold Thread
/// </summary>
public class QuickHealingBeadEffectValues : ObjectEffectValues, IAccessible_Class<QuickHealingBeadEffect>
{
    public float? flaskUseSpeedMultiplier;

    public void GetValueFrom(QuickHealingBeadEffect obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        flaskUseSpeedMultiplier = TraverseUtils.GetValue<float>(obj, "AnimatorSpeed", TraverseUtils.TraverseAccessType.Field);
    }

    public void SetValueTo(QuickHealingBeadEffect obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        TraverseUtils.SetValueIfNotNull(ref obj, "AnimatorSpeed", flaskUseSpeedMultiplier, TraverseUtils.TraverseAccessType.Field);
    }

    public override void GetValueFrom(object obj)
    {
        if (obj is QuickHealingBeadEffect t)
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
        if (obj is QuickHealingBeadEffect t)
        {
            SetValueTo(t);
        }
        else
        {
            base.SetValueTo(obj);
        }
    }
}
