using Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents;
using Framework.Managers;
using Gameplay.GameControllers.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Blasphemous.Framework.Stats.Patches;

/// <summary>
/// Containing hit data of a specific patch towards a specific hit.
/// </summary>
public class HitPatchData
{
    public float? baseDamage;
    public float? attackDamageMultiplier;
    public float? basePrayerDamage;
    public float? prayerBonusEfficiency;
    public float? finalMultiplier = 1f;
    public HitValues hitValues = new();

    [JsonIgnore]
    public Func<bool> IsActive { get; set; }

    [JsonIgnore]
    public float? BaseAttackDamage => Core.Logic.Penitent.Stats.Strength.Final;

    [JsonIgnore]
    public float? FinalDamage => HitPatchController.CalculateFinalDamageWithBonuses(
        baseDamage,
        finalMultiplier,
        new KeyValuePair<float?, float?>(BaseAttackDamage, attackDamageMultiplier),
        new KeyValuePair<float?, float?>(basePrayerDamage, HitPatchController.CalculatePrayerDamageMultiplier(prayerBonusEfficiency)));

    public Hit CreateHit()
    {
        Hit result = hitValues.CreateHitFromValues();
        result.DamageAmount = FinalDamage.HasValue
            ? FinalDamage.Value
            : result.DamageAmount;
        return result;
    }

    public Hit CreateHit(Hit originalHit)
    {
        Hit result = originalHit;
        hitValues.SetValueTo(ref result);
        result.DamageAmount = FinalDamage.HasValue
            ? FinalDamage.Value
            : result.DamageAmount;
        return result;
    }

    public void CopyNonNullValuesTo(HitPatchData other)
    {
        if (baseDamage.HasValue)
            other.baseDamage = baseDamage.Value;
        if (attackDamageMultiplier.HasValue)
            other.attackDamageMultiplier = attackDamageMultiplier.Value;
        if (basePrayerDamage.HasValue)
            other.basePrayerDamage = basePrayerDamage.Value;
        if (prayerBonusEfficiency.HasValue)
            other.prayerBonusEfficiency = prayerBonusEfficiency.Value;
        if (finalMultiplier.HasValue)
            other.finalMultiplier = finalMultiplier.Value;
        if (hitValues != null)
            other.hitValues = hitValues;
    }
}
