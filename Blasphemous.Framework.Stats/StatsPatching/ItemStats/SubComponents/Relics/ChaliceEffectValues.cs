using Blasphemous.Framework.Stats.Components;
using System.Collections.Generic;
using Tools.Items;
using Blasphemous.NewbieEltonLibs.Extensions.GameLibs;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.Relics;

/// <summary>
/// Effect for QI75, QI76, and QI77: Chalice of Inverted Verses
/// </summary>
public class ChaliceEffectValues : ObjectEffectValues, IAccessible_Class<ChaliceEffect>
{
    public List<string> targetEnemyNames;

    public void GetValueFrom(ChaliceEffect obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        targetEnemyNames = TraverseUtils.GetValue<List<string>>(obj, "EnemiesNames", TraverseUtils.TraverseAccessType.Field);
    }

    public void SetValueTo(ChaliceEffect obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        TraverseUtils.SetValueIfNotNull(ref obj, "EnemiesNames", targetEnemyNames, TraverseUtils.TraverseAccessType.Field);
    }

    public override void GetValueFrom(object obj)
    {
        if (obj is ChaliceEffect t)
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
        if (obj is ChaliceEffect t)
        {
            SetValueTo(t);
        }
        else
        {
            base.SetValueTo(obj);
        }
    }
}
