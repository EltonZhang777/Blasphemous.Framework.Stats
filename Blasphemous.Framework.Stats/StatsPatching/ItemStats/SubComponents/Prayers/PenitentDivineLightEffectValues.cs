using Blasphemous.Framework.Stats.Components;
using Tools.Items;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.Prayers;

// WIP
/// <summary>
/// Effect for PR09: Taranto to My Sister
/// </summary>
public class PenitentDivineLightEffectValues : ObjectEffectValues, IAccessible_Class<PenitentDivineLightEffect>
{
    public void GetValueFrom(PenitentDivineLightEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);
    }

    public void SetValueTo(PenitentDivineLightEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

    }

    public override void GetValueFrom(object obj)
    {
        if (obj is PenitentDivineLightEffect t)
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
        if (obj is PenitentDivineLightEffect t)
        {
            SetValueTo(t);
        }
        else
        {
            base.SetValueTo(obj);
        }
    }
}
