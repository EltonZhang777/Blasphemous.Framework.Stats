using Blasphemous.Framework.Stats.Components;
using Framework.Inventory;
using Gameplay.GameControllers.Penitent.Abilities;
using Blasphemous.NewbieEltonLibs.Extensions.GameLibs;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.SwordHearts;

/// <summary>
/// Effect for RB203: The Young Mason's Wheel
/// </summary>
public class IncreaseSpeedBeadEffectValues : ObjectEffectValues, IAccessible_Class<IncreaseSpeedBeadEffect>
{
    public Dash.MoveSetting? movementSetting;

    public void GetValueFrom(IncreaseSpeedBeadEffect obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        movementSetting = TraverseUtils.GetValue<Dash.MoveSetting>(obj, "BeadMoveSettings", TraverseUtils.TraverseAccessType.Field);
    }

    public void SetValueTo(IncreaseSpeedBeadEffect obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        TraverseUtils.SetValueIfNotNull(ref obj, "BeadMoveSettings", movementSetting, TraverseUtils.TraverseAccessType.Field);
    }

    public override void GetValueFrom(object obj)
    {
        if (obj is IncreaseSpeedBeadEffect t)
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
        if (obj is IncreaseSpeedBeadEffect t)
        {
            SetValueTo(t);
        }
        else
        {
            base.SetValueTo(obj);
        }
    }
}
