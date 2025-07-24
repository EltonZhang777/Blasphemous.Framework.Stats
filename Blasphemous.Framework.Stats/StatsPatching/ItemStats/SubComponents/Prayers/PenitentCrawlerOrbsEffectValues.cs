using Blasphemous.Framework.Stats.Components;
using Tools.Items;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.Prayers;

// WIP
/// <summary>
/// Effect for PR14: Verdiales of the Forsaken Hamlet
/// </summary>
public class PenitentCrawlerOrbsEffectValues : ObjectEffectValues, IAccessible_Class<PenitentCrawlerOrbsEffect>
{
    public void GetValueFrom(PenitentCrawlerOrbsEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

    }

    public void SetValueTo(PenitentCrawlerOrbsEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

    }

    public override void GetValueFrom(object obj)
    {
        if (obj is PenitentCrawlerOrbsEffect t)
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
        if (obj is PenitentCrawlerOrbsEffect t)
        {
            SetValueTo(t);
        }
        else
        {
            base.SetValueTo(obj);
        }
    }
}
