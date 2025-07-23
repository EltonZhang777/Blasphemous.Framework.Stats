using Blasphemous.Framework.Stats.Components;
using System;
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

        throw new NotImplementedException();
    }

    public void SetValueTo(PenitentCrawlerOrbsEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        throw new NotImplementedException();
    }
}
