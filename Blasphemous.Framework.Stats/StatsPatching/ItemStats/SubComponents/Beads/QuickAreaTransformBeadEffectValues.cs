using Blasphemous.Framework.Stats.Components;
using Framework.Inventory;
using Blasphemous.NewbieEltonLibs.Extensions.GameLibs;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.SwordHearts;

/// <summary>
/// Effect for RB108: Fire Enclosed in Enamel
/// </summary>
public class QuickAreaTransformBeadEffectValues : ObjectEffectValues, IAccessible_Class<QuickAreaTransformBeadEffect>
{
    public float? prayerCastSpeedMultiplier;

    public void GetValueFrom(QuickAreaTransformBeadEffect obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        prayerCastSpeedMultiplier = TraverseUtils.GetValue<float>(obj, "AuraTransformAnimationSpeed", TraverseUtils.TraverseAccessType.Field);
    }

    public void SetValueTo(QuickAreaTransformBeadEffect obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        TraverseUtils.SetValueIfNotNull(ref obj, "AuraTransformAnimationSpeed", prayerCastSpeedMultiplier, TraverseUtils.TraverseAccessType.Field);
    }

    public override void GetValueFrom(object obj)
    {
        if (obj is QuickAreaTransformBeadEffect t)
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
        if (obj is QuickAreaTransformBeadEffect t)
        {
            SetValueTo(t);
        }
        else
        {
            base.SetValueTo(obj);
        }
    }
}
