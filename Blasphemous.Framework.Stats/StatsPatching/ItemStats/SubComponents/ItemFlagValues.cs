using Blasphemous.Framework.Stats.Components;
using Tools.Items;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents;

public class ItemFlagValues : ObjectEffectValues, IAccessible_Class<ItemFlag>
{
    public string flagName;

    public void GetValueFrom(ItemFlag obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        flagName = Main.GetValue<ItemFlag, string>(obj, "flagName", Main.TraverseAccessType.Field);
    }

    public void SetValueTo(ItemFlag obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        Main.SetValueIfNotNull(ref obj, "flagName", flagName, Main.TraverseAccessType.Field);
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
