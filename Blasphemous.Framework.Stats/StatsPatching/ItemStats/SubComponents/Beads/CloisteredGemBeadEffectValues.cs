using Blasphemous.Framework.Stats.Components;
using Framework.Inventory;
using Framework.Managers;
using HarmonyLib;
using Blasphemous.NewbieEltonLibs.Extensions.GameLibs;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.SwordHearts;

// WIP
/// <summary>
/// Effect for RB105 and RB107: Cloistered Ruby & Cloistered Sapphire
/// </summary>
public class CloisteredGemBeadEffectValues : ObjectEffectValues, IAccessible_Class<CloisteredGemBeadEffect>
{
    /// <summary>
    /// Number of attacks that can spawn blades after the effect is triggered.
    /// </summary>
    public int? maxUses;

    public float? projectileSpeed;

    /// <summary>
    /// Base damage of the Cloistered Gem Bead blades.
    /// </summary>
    public float? baseDamage;

    /// <summary>
    /// Multiples of penitent attack damage that will be added to the Cloistered Gem Bead blades damage.
    /// </summary>
    public float? attackDamageMultiplier;

    /// <summary>
    /// Final damage multiplier of the Cloistered Gem Bead blades, multiplying the total damage after all previous effects are applied.
    /// </summary>
    public float? finalDamageMultiplier;

    public HitValues hitValues;

    protected internal Traverse cloisteredGemProjectileAttack;

    protected internal float CurrentPenitentAttackDamage => Core.Logic.Penitent.Stats.Strength.Final;
    protected internal float? ProjectileFinalDamage => baseDamage + (CurrentPenitentAttackDamage * attackDamageMultiplier);

    public void GetValueFrom(CloisteredGemBeadEffect obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        finalDamageMultiplier = TraverseUtils.GetValue<int>(obj, "DamageAmount", TraverseUtils.TraverseAccessType.Field);
        maxUses = TraverseUtils.GetValue<int>(obj, "MaxUses", TraverseUtils.TraverseAccessType.Field);

        cloisteredGemProjectileAttack = Traverse.Create(obj).Field("projectileAttack");
        if (cloisteredGemProjectileAttack != null)
        {
            // WIP
            projectileSpeed = TraverseUtils.GetValue<float>(cloisteredGemProjectileAttack, "projectileSpeed", TraverseUtils.TraverseAccessType.Field);
            baseDamage = TraverseUtils.GetValue<float>(cloisteredGemProjectileAttack, "ProjectileDamageAmount", TraverseUtils.TraverseAccessType.Field);
        }
    }

    public void SetValueTo(CloisteredGemBeadEffect obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        // WIP
        TraverseUtils.SetValueIfNotNull(ref obj, "DamageAmount", finalDamageMultiplier, TraverseUtils.TraverseAccessType.Field);
        TraverseUtils.SetValueIfNotNull(ref obj, "MaxUses", maxUses, TraverseUtils.TraverseAccessType.Field);
    }

    public override void GetValueFrom(object obj)
    {
        if (obj is CloisteredGemBeadEffect t)
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
        if (obj is CloisteredGemBeadEffect t)
        {
            SetValueTo(t);
        }
        else
        {
            base.SetValueTo(obj);
        }
    }
}
