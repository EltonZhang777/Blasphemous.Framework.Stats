using Blasphemous.Framework.Stats.Components;
using System;
using Tools.Items;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.Prayers;

// WIP
/// <summary>
/// Effect for PR203: Tirana of the Celestial Bastion
/// </summary>
public class PR203ElmFireLoopEffectValues : ObjectEffectValues, IAccessible_Class<PR203ElmFireLoopEffect>
{
    public void GetValueFrom(PR203ElmFireLoopEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        throw new NotImplementedException();
    }

    public void SetValueTo(PR203ElmFireLoopEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        throw new NotImplementedException();
    }
}
