using Blasphemous.Framework.Stats.Components;
using Blasphemous.Framework.Stats.Patches;
using Blasphemous.Framework.Stats.Patches.ItemPatches;
using Tools.Items;
using UnityEngine;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.Prayers;

// WIP
/// <summary>
/// Effect for PR03: Debla of the Lights
/// </summary>
public class PenitentLightBeamEffectValues : ObjectEffectValues, IAccessible_Class<PenitentLightBeamEffect>
{
    public string penitentColorWhileCasting;
    public HitPatchData hitData;
    private string _vanillaDefaultColor = "#439AC3FF";

    public void GetValueFrom(PenitentLightBeamEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        penitentColorWhileCasting = _vanillaDefaultColor;
        hitData = new();
        hitData.basePrayerDamage = (float)Main.GetValue<int>(obj, "DamageAmount", Main.TraverseAccessType.Field);
    }

    public void SetValueTo(PenitentLightBeamEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        hitData.CopyNonNullValuesTo(PatchController.Hits.PR03);

        // copy color while casting to the Harmony patch
        Color color = ColorUtility.TryParseHtmlString(penitentColorWhileCasting ?? _vanillaDefaultColor, out Color c)
            ? c
            : new Color32(67, 154, 195, 255); // fallback to the vanilla blue tint color, Tint: (0.2627451, 0.6039216, 0.764706, 1.0)
        color.a = 1f;
        PR03_HitPatch.penitentColorWhileCasting = color;
    }

    public override void GetValueFrom(object obj)
    {
        if (obj is PenitentLightBeamEffect t)
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
        if (obj is PenitentLightBeamEffect t)
        {
            SetValueTo(t);
        }
        else
        {
            base.SetValueTo(obj);
        }
    }
}
