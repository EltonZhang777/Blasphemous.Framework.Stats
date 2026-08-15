using Blasphemous.Framework.Stats.Components;
using Framework.Inventory;
using Blasphemous.NewbieEltonLibs.Extensions.GameLibs;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.Prayers;

// WIP
/// <summary>
/// Effect for PR16: Zambra to the Resplendent Crown
/// </summary>
public class ZambraTearsHarvestEffectValues : ObjectEffectValues, IAccessible_Class<ZambraTearsHarvestEffect>
{
    public void GetValueFrom(ZambraTearsHarvestEffect obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

    }

    public void SetValueTo(ZambraTearsHarvestEffect obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

    }

    public override void GetValueFrom(object obj)
    {
        if (obj is ZambraTearsHarvestEffect t)
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
        if (obj is ZambraTearsHarvestEffect t)
        {
            SetValueTo(t);
        }
        else
        {
            base.SetValueTo(obj);
        }
    }
}
