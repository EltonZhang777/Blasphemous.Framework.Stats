using Blasphemous.Framework.Stats.Components;
using Tools.Items;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.Prayers;

// WIP
/// <summary>
/// Effect for PR03: Debla of the Lights
/// </summary>
public class PenitentLightBeamEffectValues : ObjectEffectValues, IAccessible_Class<PenitentLightBeamEffect>
{
    public void GetValueFrom(PenitentLightBeamEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);
    }

    public void SetValueTo(PenitentLightBeamEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);
    }

    public override void GetValueFrom(object obj)
    {
        if (obj is PenitentLightBeamEffect t)
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
        if (obj is PenitentLightBeamEffect t)
        {
            SetValueTo(t);
        }
        else
        {
            base.SetValueTo(obj);
        }
    }
}
