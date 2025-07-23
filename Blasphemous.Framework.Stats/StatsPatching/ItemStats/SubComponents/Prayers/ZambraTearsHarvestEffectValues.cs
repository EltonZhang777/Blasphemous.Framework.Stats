using Blasphemous.Framework.Stats.Components;
using Framework.Inventory;
using System;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.Prayers;

// WIP
/// <summary>
/// Effect for PR16: Zambra to the Resplendent Crown
/// </summary>
public class ZambraTearsHarvestEffectValues : ObjectEffectValues, IAccessible_Class<ZambraTearsHarvestEffect>
{
    public void GetValueFrom(ZambraTearsHarvestEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        throw new NotImplementedException();
    }

    public void SetValueTo(ZambraTearsHarvestEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        throw new NotImplementedException();
    }
}
