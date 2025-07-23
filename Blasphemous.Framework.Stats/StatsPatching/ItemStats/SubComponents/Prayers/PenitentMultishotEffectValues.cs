using Blasphemous.Framework.Stats.Components;
using System;
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

        throw new NotImplementedException();
    }

    public void SetValueTo(PenitentMultishotEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        throw new NotImplementedException();
    }
}
