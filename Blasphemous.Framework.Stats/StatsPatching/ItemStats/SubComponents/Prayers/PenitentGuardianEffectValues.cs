using Blasphemous.Framework.Stats.Components;
using Tools.Items;
using Blasphemous.NewbieEltonLibs.Extensions.GameLibs;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.Prayers;

// WIP
/// <summary>
/// Effect for PR11: Tiento to your Thorned Hairs
/// </summary>
public class PenitentGuardianEffectValues : ObjectEffectValues, IAccessible_Class<PenitentGuardianEffect>
{
    /// <summary>
    /// Y offset of the thorned lady VFX to Penitent
    /// </summary>
    public float? yOffset;

    public void GetValueFrom(PenitentGuardianEffect obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        yOffset = TraverseUtils.GetValue<float>(obj, "YOffset", TraverseUtils.TraverseAccessType.Field);
    }

    public void SetValueTo(PenitentGuardianEffect obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        TraverseUtils.SetValueIfNotNull(ref obj, "YOffset", yOffset, TraverseUtils.TraverseAccessType.Field);
    }

    public override void GetValueFrom(object obj)
    {
        if (obj is PenitentGuardianEffect t)
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
        if (obj is PenitentGuardianEffect t)
        {
            SetValueTo(t);
        }
        else
        {
            base.SetValueTo(obj);
        }
    }
}
