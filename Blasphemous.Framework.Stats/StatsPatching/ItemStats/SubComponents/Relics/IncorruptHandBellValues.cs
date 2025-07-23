using Blasphemous.Framework.Stats.Components;
using System;
using Tools.Items;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.Relics;

/// <summary>
/// Effect for RE02: Incorrupt Hand of the Fraternal Master
/// </summary>
public class IncorruptHandBellValues : ObjectEffectValues, IAccessible_Class<IncorruptHandBell>
{
    public void GetValueFrom(IncorruptHandBell obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        throw new NotImplementedException();
    }

    public void SetValueTo(IncorruptHandBell obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        throw new NotImplementedException();
    }
}
