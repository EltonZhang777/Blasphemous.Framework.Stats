using Blasphemous.Framework.Stats.Components;
using Tools.Items;
using UnityEngine;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.SwordHearts;

/// <summary>
/// Effect for RB42: Bust Carved in Ivory
/// </summary>
public class FamiliarSpawnEffectValues : ObjectEffectValues, IAccessible_Class<FamiliarSpawnEffect>
{
    public SerializableVector3? minionOffsetToPenitent;

    public void GetValueFrom(FamiliarSpawnEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        minionOffsetToPenitent = Main.GetValue<FamiliarSpawnEffect, Vector2>(obj, "Offset", Main.TraverseAccessType.Field);
    }

    public void SetValueTo(FamiliarSpawnEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        Main.SetValueIfNotNull(ref obj, "Offset", minionOffsetToPenitent, Main.TraverseAccessType.Field);
    }
}
