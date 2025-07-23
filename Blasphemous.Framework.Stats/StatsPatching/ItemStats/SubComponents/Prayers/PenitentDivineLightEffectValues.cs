using Blasphemous.Framework.Stats.Components;
using System;
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

        throw new NotImplementedException();
    }

    public void SetValueTo(PenitentDivineLightEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        throw new NotImplementedException();
    }
}
