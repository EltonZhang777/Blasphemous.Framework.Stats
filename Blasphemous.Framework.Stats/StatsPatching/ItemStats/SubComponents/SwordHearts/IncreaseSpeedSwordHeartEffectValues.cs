using Blasphemous.Framework.Stats.Components;
using Framework.Inventory;
using Gameplay.GameControllers.Penitent.Abilities;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.SwordHearts;

/// <summary>
/// Effect for HE101: Brilliant Heart of Dawn
/// </summary>
public class IncreaseSpeedSwordHeartEffectValues : ObjectEffectValues, IAccessible_Class<IncreaseSpeedSwordHeartEffect>
{
    public Dash.MoveSetting? movementSetting;

    public void GetValueFrom(IncreaseSpeedSwordHeartEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        movementSetting = Main.GetValue<Dash.MoveSetting>(obj, "MotionSettings", Main.TraverseAccessType.Field);
    }

    public void SetValueTo(IncreaseSpeedSwordHeartEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        Main.SetValueIfNotNull(ref obj, "MotionSettings", movementSetting, Main.TraverseAccessType.Field);
    }

    public override void GetValueFrom(object obj)
    {
        if (obj is IncreaseSpeedSwordHeartEffect t)
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
        if (obj is IncreaseSpeedSwordHeartEffect t)
        {
            SetValueTo(t);
        }
        else
        {
            base.SetValueTo(obj);
        }
    }
}
