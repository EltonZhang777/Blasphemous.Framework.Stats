using Blasphemous.Framework.Stats.Components;
using Framework.Inventory;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.SwordHearts;

/// <summary>
/// Effect for RB108: Fire Enclosed in Enamel
/// </summary>
public class QuickAreaTransformBeadEffectValues : ObjectEffectValues, IAccessible_Class<QuickAreaTransformBeadEffect>
{
    public float? prayerCastSpeedMultiplier;

    public void GetValueFrom(QuickAreaTransformBeadEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        prayerCastSpeedMultiplier = Main.GetValue<QuickAreaTransformBeadEffect, float>(obj, "AuraTransformAnimationSpeed", Main.TraverseAccessType.Field);
    }

    public void SetValueTo(QuickAreaTransformBeadEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        Main.SetValueIfNotNull(ref obj, "AuraTransformAnimationSpeed", prayerCastSpeedMultiplier, Main.TraverseAccessType.Field);
    }
}
