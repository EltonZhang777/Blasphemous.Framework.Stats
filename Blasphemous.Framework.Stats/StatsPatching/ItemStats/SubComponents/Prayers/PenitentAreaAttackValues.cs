using Blasphemous.Framework.Stats.Components;
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

    }

    public void SetValueTo(PenitentAreaAttack obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

    }

    public override void GetValueFrom(object obj)
    {
        if (obj is PenitentAreaAttack t)
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
        if (obj is PenitentAreaAttack t)
        {
            SetValueTo(t);
        }
        else
        {
            base.SetValueTo(obj);
        }
    }
}
