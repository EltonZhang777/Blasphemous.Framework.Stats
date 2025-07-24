using Blasphemous.Framework.Stats.Components;
using Framework.Inventory;
using Framework.Managers;
using HarmonyLib;

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
    protected internal float? ProjectileFinalDamage => baseDamage + CurrentPenitentAttackDamage * attackDamageMultiplier;

    public void GetValueFrom(CloisteredGemBeadEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        finalDamageMultiplier = Main.GetValue<CloisteredGemBeadEffect, int>(obj, "DamageAmount", Main.TraverseAccessType.Field);
        maxUses = Main.GetValue<CloisteredGemBeadEffect, int>(obj, "MaxUses", Main.TraverseAccessType.Field);

        cloisteredGemProjectileAttack = Traverse.Create(obj).Field("projectileAttack");
        if (cloisteredGemProjectileAttack != null)
        {
            // WIP
            projectileSpeed = Main.GetValue<float>(cloisteredGemProjectileAttack, "projectileSpeed", Main.TraverseAccessType.Field);
            baseDamage = Main.GetValue<float>(cloisteredGemProjectileAttack, "ProjectileDamageAmount", Main.TraverseAccessType.Field);
        }
    }

    public void SetValueTo(CloisteredGemBeadEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        // WIP
        Main.SetValueIfNotNull(ref obj, "DamageAmount", finalDamageMultiplier, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "MaxUses", maxUses, Main.TraverseAccessType.Field);
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
