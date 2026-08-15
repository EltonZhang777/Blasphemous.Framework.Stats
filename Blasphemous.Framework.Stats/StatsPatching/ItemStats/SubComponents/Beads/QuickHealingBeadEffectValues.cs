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

        flaskUseSpeedMultiplier = Main.GetValue<float>(obj, "AnimatorSpeed", Main.TraverseAccessType.Field);
    }

    public void SetValueTo(QuickHealingBeadEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        Main.SetValueIfNotNull(ref obj, "AnimatorSpeed", flaskUseSpeedMultiplier, Main.TraverseAccessType.Field);
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
