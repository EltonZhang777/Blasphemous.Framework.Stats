using Blasphemous.Framework.Stats.Components;
using Tools.Items;
using UnityEngine;
using Blasphemous.NewbieEltonLibs.Extensions.GameLibs;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.SwordHearts;

/// <summary>
/// Effect for RB42: Bust Carved in Ivory
/// </summary>
public class FamiliarSpawnEffectValues : ObjectEffectValues, IAccessible_Class<FamiliarSpawnEffect>
{
    public SerializableVector3? minionOffsetToPenitent;

    public void GetValueFrom(FamiliarSpawnEffect obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        minionOffsetToPenitent = TraverseUtils.GetValue<Vector2>(obj, "Offset", TraverseUtils.TraverseAccessType.Field);
    }

    public void SetValueTo(FamiliarSpawnEffect obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        TraverseUtils.SetValueIfNotNull(ref obj, "Offset", minionOffsetToPenitent, TraverseUtils.TraverseAccessType.Field);
    }

    public override void GetValueFrom(object obj)
    {
        if (obj is FamiliarSpawnEffect t)
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
        if (obj is FamiliarSpawnEffect t)
        {
            SetValueTo(t);
        }
        else
        {
            base.SetValueTo(obj);
        }
    }
}
