using Blasphemous.Framework.Stats.Components;
using Tools.Items;
using UnityEngine;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents;

public class ItemGhostTrailValues : ObjectEffectValues, IAccessible_Class<ItemGhostTrail>
{
    public string trailColor;

    private Color? TrailColor => !string.IsNullOrEmpty(trailColor)
        ? ColorUtility.TryParseHtmlString(trailColor, out var color)
            ? color
            : null
        : null;

    public void GetValueFrom(ItemGhostTrail obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        trailColor = ColorUtility.ToHtmlStringRGBA(Main.GetValue<ItemGhostTrail, Color>(obj, "color", Main.TraverseAccessType.Field));
    }

    public void SetValueTo(ItemGhostTrail obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        Main.SetValueIfNotNull(ref obj, "color", TrailColor, Main.TraverseAccessType.Field);
    }

    public override void GetValueFrom(object obj)
    {
        if (obj is ItemGhostTrail t)
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
        if (obj is ItemGhostTrail t)
        {
            SetValueTo(t);
        }
        else
        {
            base.SetValueTo(obj);
        }
    }
}
