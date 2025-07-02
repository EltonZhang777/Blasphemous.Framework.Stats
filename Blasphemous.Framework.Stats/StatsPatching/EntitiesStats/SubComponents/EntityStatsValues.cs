using Blasphemous.Framework.Stats.Components;
using Gameplay.GameControllers.Entities;
using Newtonsoft.Json;

namespace Blasphemous.Framework.Stats.StatsPatching.EntitiesStats.SubComponents;

/// <summary>
/// Documenting useful values of a Blasphemous <see cref="EntityStats"/> for <see cref="Gameplay.GameControllers.Penitent.Penitent"/>
/// </summary>
public class EntityStatsValues_Penitent : IAccessible<EntityStats>
{
    [JsonProperty] public AttributeValues life = new();
    [JsonProperty] public AttributeValues strength = new();
    [JsonProperty] public AttributeValues defense = new();
    [JsonProperty] public AttributeValues physicalDamageReduction = new();
    [JsonProperty] public AttributeValues contactDamageReduction = new();
    [JsonProperty] public AttributeValues magicDamageReduction = new();
    [JsonProperty] public AttributeValues lightningDamageReduction = new();
    [JsonProperty] public AttributeValues fireDamageReduction = new();
    [JsonProperty] public AttributeValues toxicDamageReduction = new();

    [JsonProperty] public AttributeValues attackSpeed = new();
    [JsonProperty] public AttributeValues fervour = new();
    [JsonProperty] public AttributeValues fervourStrength = new();
    [JsonProperty] public AttributeValues rangedStrength = new();
    [JsonProperty] public AttributeValues flask = new();
    [JsonProperty] public AttributeValues flaskHealth = new();
    [JsonProperty] public AttributeValues beadSlots = new();
    [JsonProperty] public AttributeValues dashCooldown = new();
    [JsonProperty] public AttributeValues dashDuration = new();
    [JsonProperty] public AttributeValues purgeStrength = new();
    [JsonProperty] public AttributeValues meaCulpa = new();
    [JsonProperty] public AttributeValues parryWindow = new();
    [JsonProperty] public AttributeValues righteousRiposteWindow = new();
    [JsonProperty] public AttributeValues physicalDamageMultiplier = new();
    [JsonProperty] public AttributeValues prayerDurationAddition = new();
    [JsonProperty] public AttributeValues prayerStrengthMultiplier = new();
    [JsonProperty] public AttributeValues prayerCostAddition = new();
    [JsonProperty] public AttributeValues airImpulses = new();

    /// <inheritdoc/>
    public void GetValueFrom(EntityStats stats)
    {
        if (!Main.Validate(stats, x => x != null))
            return;

        life.GetValueFrom(stats.Life);
        strength.GetValueFrom(stats.Strength);
        defense.GetValueFrom(stats.Defense);
        physicalDamageReduction.GetValueFrom(stats.NormalDmgReduction);
        contactDamageReduction.GetValueFrom(stats.ContactDmgReduction);
        magicDamageReduction.GetValueFrom(stats.MagicDmgReduction);
        lightningDamageReduction.GetValueFrom(stats.LightningDmgReduction);
        fireDamageReduction.GetValueFrom(stats.FireDmgReduction);
        toxicDamageReduction.GetValueFrom(stats.ToxicDmgReduction);

        attackSpeed.GetValueFrom(stats.AttackSpeed);
        fervour.GetValueFrom(stats.Fervour);
        fervourStrength.GetValueFrom(stats.FervourStrength);
        rangedStrength.GetValueFrom(stats.RangedStrength);
        flask.GetValueFrom(stats.Flask);
        flaskHealth.GetValueFrom(stats.FlaskHealth);
        beadSlots.GetValueFrom(stats.BeadSlots);
        dashCooldown.GetValueFrom(stats.DashCooldown);
        dashDuration.GetValueFrom(stats.DashRide);
        purgeStrength.GetValueFrom(stats.PurgeStrength);
        meaCulpa.GetValueFrom(stats.MeaCulpa);
        parryWindow.GetValueFrom(stats.ParryWindow);
        righteousRiposteWindow.GetValueFrom(stats.ActiveRiposteWindow);
        physicalDamageMultiplier.GetValueFrom(stats.DamageMultiplier);
        prayerDurationAddition.GetValueFrom(stats.PrayerDurationAddition);
        prayerStrengthMultiplier.GetValueFrom(stats.PrayerStrengthMultiplier);
        prayerCostAddition.GetValueFrom(stats.PrayerCostAddition);
        airImpulses.GetValueFrom(stats.AirImpulses);
    }

    /// <inheritdoc/>
    public void SetValueTo(EntityStats stats)
    {
        if (!Main.Validate(stats, x => x != null))
            return;

        life.SetValueTo(stats.Life);
        strength.SetValueTo(stats.Strength);
        defense.SetValueTo(stats.Defense);
        physicalDamageReduction.SetValueTo(stats.NormalDmgReduction);
        contactDamageReduction.SetValueTo(stats.ContactDmgReduction);
        magicDamageReduction.SetValueTo(stats.MagicDmgReduction);
        lightningDamageReduction.SetValueTo(stats.LightningDmgReduction);
        fireDamageReduction.SetValueTo(stats.FireDmgReduction);
        toxicDamageReduction.SetValueTo(stats.ToxicDmgReduction);

        attackSpeed.SetValueTo(stats.AttackSpeed);
        fervour.SetValueTo(stats.Fervour);
        fervourStrength.SetValueTo(stats.FervourStrength);
        rangedStrength.SetValueTo(stats.RangedStrength);
        flask.SetValueTo(stats.Flask);
        flaskHealth.SetValueTo(stats.FlaskHealth);
        beadSlots.SetValueTo(stats.BeadSlots);
        dashCooldown.SetValueTo(stats.DashCooldown);
        dashDuration.SetValueTo(stats.DashRide);
        purgeStrength.SetValueTo(stats.PurgeStrength);
        meaCulpa.SetValueTo(stats.MeaCulpa);
        parryWindow.SetValueTo(stats.ParryWindow);
        righteousRiposteWindow.SetValueTo(stats.ActiveRiposteWindow);
        physicalDamageMultiplier.SetValueTo(stats.DamageMultiplier);
        prayerDurationAddition.SetValueTo(stats.PrayerDurationAddition);
        prayerStrengthMultiplier.SetValueTo(stats.PrayerStrengthMultiplier);
        prayerCostAddition.SetValueTo(stats.PrayerCostAddition);
        airImpulses.SetValueTo(stats.AirImpulses);
    }
}

/// <summary>
/// Documenting useful values of a Blasphemous <see cref="EntityStats"/> for <see cref="Enemy"/>
/// </summary>
public class EntityStatsValues_Enemy : IAccessible<EntityStats>
{
    [JsonProperty] public AttributeValues life = new();
    [JsonProperty] public AttributeValues strength = new();
    [JsonProperty] public AttributeValues defense = new();
    [JsonProperty] public AttributeValues physicalDamageReduction = new();
    [JsonProperty] public AttributeValues contactDamageReduction = new();
    [JsonProperty] public AttributeValues magicDamageReduction = new();
    [JsonProperty] public AttributeValues lightningDamageReduction = new();
    [JsonProperty] public AttributeValues fireDamageReduction = new();
    [JsonProperty] public AttributeValues toxicDamageReduction = new();

    /// <inheritdoc/>
    public void GetValueFrom(EntityStats stats)
    {
        if (!Main.Validate(stats, x => x != null))
            return;

        life.GetValueFrom(stats.Life);
        strength.GetValueFrom(stats.Strength);
        defense.GetValueFrom(stats.Defense);
        physicalDamageReduction.GetValueFrom(stats.NormalDmgReduction);
        contactDamageReduction.GetValueFrom(stats.ContactDmgReduction);
        magicDamageReduction.GetValueFrom(stats.MagicDmgReduction);
        lightningDamageReduction.GetValueFrom(stats.LightningDmgReduction);
        fireDamageReduction.GetValueFrom(stats.FireDmgReduction);
        toxicDamageReduction.GetValueFrom(stats.ToxicDmgReduction);
    }

    /// <inheritdoc/>
    public void SetValueTo(EntityStats stats)
    {
        if (!Main.Validate(stats, x => x != null))
            return;

        life.SetValueTo(stats.Life);
        strength.SetValueTo(stats.Strength);
        defense.SetValueTo(stats.Defense);
        physicalDamageReduction.SetValueTo(stats.NormalDmgReduction);
        contactDamageReduction.SetValueTo(stats.ContactDmgReduction);
        magicDamageReduction.SetValueTo(stats.MagicDmgReduction);
        lightningDamageReduction.SetValueTo(stats.LightningDmgReduction);
        fireDamageReduction.SetValueTo(stats.FireDmgReduction);
        toxicDamageReduction.SetValueTo(stats.ToxicDmgReduction);
    }
}