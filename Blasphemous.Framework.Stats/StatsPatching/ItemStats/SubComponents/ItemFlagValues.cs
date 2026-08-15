using Blasphemous.Framework.Stats.Components;
using Tools.Items;
using Blasphemous.NewbieEltonLibs.Extensions.GameLibs;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents;

public class ItemFlagValues : ObjectEffectValues, IAccessible_Class<ItemFlag>
{
    public string flagName;

    public void GetValueFrom(ItemFlag obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        flagName = TraverseUtils.GetValue<string>(obj, "flagName", TraverseUtils.TraverseAccessType.Field);
    }

    public void SetValueTo(ItemFlag obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        TraverseUtils.SetValueIfNotNull(ref obj, "flagName", flagName, TraverseUtils.TraverseAccessType.Field);
    }

    public override void GetValueFrom(object obj)
    {
        if (obj is ItemFlag t)
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
        if (obj is ItemFlag t)
        {
            SetValueTo(t);
        }
        else
        {
            base.SetValueTo(obj);
        }
    }
}
