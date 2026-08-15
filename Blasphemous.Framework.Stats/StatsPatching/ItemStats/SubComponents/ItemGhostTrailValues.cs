using Blasphemous.Framework.Stats.Components;
using Tools.Items;
using UnityEngine;
using Blasphemous.NewbieEltonLibs.Extensions.GameLibs;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents;

public class ItemGhostTrailValues : ObjectEffectValues, IAccessible_Class<ItemGhostTrail>
{
    public string trailColor;

    private Color? TrailColor => !string.IsNullOrEmpty(trailColor)
        ? ColorUtility.TryParseHtmlString(trailColor, out global::UnityEngine.Color color)
            ? color
            : null
        : null;

    public void GetValueFrom(ItemGhostTrail obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        trailColor = ColorUtility.ToHtmlStringRGBA(TraverseUtils.GetValue<Color>(obj, "color", TraverseUtils.TraverseAccessType.Field));
    }

    public void SetValueTo(ItemGhostTrail obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        TraverseUtils.SetValueIfNotNull(ref obj, "color", TrailColor, TraverseUtils.TraverseAccessType.Field);
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
