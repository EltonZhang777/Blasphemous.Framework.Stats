using Blasphemous.Framework.Stats.Components;
using Tools.Items;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.Prayers;

// WIP
/// <summary>
/// Effect for PR07: Lorquiana
/// </summary>
public class PenitentMultishotEffectValues : ObjectEffectValues, IAccessible_Class<PenitentMultishotEffect>
{
    public void GetValueFrom(PenitentMultishotEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);
    }

    public void SetValueTo(PenitentMultishotEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);
    }

    public override void GetValueFrom(object obj)
    {
        if (obj is PenitentMultishotEffect t)
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
        if (obj is PenitentMultishotEffect t)
        {
            SetValueTo(t);
        }
        else
        {
            base.SetValueTo(obj);
        }
    }
}
