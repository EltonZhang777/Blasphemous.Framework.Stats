using Blasphemous.Framework.Stats.Components;
using System;
using Tools.Items;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.Prayers;

// WIP
/// <summary>
/// Effect for PR12: Cante Jondo of the Three Sisters
/// </summary>
public class PenitentAreaAttackValues : ObjectEffectValues, IAccessible_Class<PenitentAreaAttack>
{
    public void GetValueFrom(PenitentAreaAttack obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        throw new NotImplementedException();
    }

    public void SetValueTo(PenitentAreaAttack obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        throw new NotImplementedException();
    }
}
