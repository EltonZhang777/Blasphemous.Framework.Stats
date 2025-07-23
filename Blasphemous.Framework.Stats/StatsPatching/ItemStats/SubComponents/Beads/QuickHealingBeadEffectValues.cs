using Blasphemous.Framework.Stats.Components;
using Framework.Inventory;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.SwordHearts;

/// <summary>
/// Effect for RB106: Bead of Gold Thread
/// </summary>
public class QuickHealingBeadEffectValues : ObjectEffectValues, IAccessible_Class<QuickHealingBeadEffect>
{
    public float? flaskUseSpeedMultiplier;

    public void GetValueFrom(QuickHealingBeadEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        flaskUseSpeedMultiplier = Main.GetValue<QuickHealingBeadEffect, float>(obj, "AnimatorSpeed", Main.TraverseAccessType.Field);
    }

    public void SetValueTo(QuickHealingBeadEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        Main.SetValueIfNotNull(ref obj, "AnimatorSpeed", flaskUseSpeedMultiplier, Main.TraverseAccessType.Field);
    }
}
