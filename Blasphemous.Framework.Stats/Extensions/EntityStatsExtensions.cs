using Framework.Managers;
using Gameplay.GameControllers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Blasphemous.Framework.Stats.Extensions;

/// <summary>
/// Useful extensions for inspecting <see cref="EntityStats"/>
/// </summary>
public static class EntityStatsExtensions
{
    /// <summary>
    /// Write the useful values of the given attribute to an <see cref="EntityStatsValues_Enemy"/> instance
    /// </summary>
    public static EntityStatsValues_Enemy ToEntityStatsValues_Enemy(this EntityStats stats)
    {
        return new EntityStatsValues_Enemy()
        {
            life = stats.Life.ToAttributeValues(),
            strength = stats.Strength.ToAttributeValues(),
            defense = stats.Defense.ToAttributeValues(),
            physicalDamageReduction = stats.NormalDmgReduction.ToAttributeValues(),
            contactDamageReduction = stats.ContactDmgReduction.ToAttributeValues(),
            magicDamageReduction = stats.MagicDmgReduction.ToAttributeValues(),
            lightningDamageReduction = stats.LightningDmgReduction.ToAttributeValues(),
            fireDamageReduction = stats.FireDmgReduction.ToAttributeValues(),
            toxicDamageReduction = stats.ToxicDmgReduction.ToAttributeValues(),
        };
    }

    public static EntityStatsValues_Penitent ToEntityStatsValues_Penitent(this EntityStats stats)
    {
        return new EntityStatsValues_Penitent()
        {
            life = stats.Life.ToAttributeValues(),
            strength = stats.Strength.ToAttributeValues(),
            defense = stats.Defense.ToAttributeValues(),
            physicalDamageReduction = stats.NormalDmgReduction.ToAttributeValues(),
            contactDamageReduction = stats.ContactDmgReduction.ToAttributeValues(),
            magicDamageReduction = stats.MagicDmgReduction.ToAttributeValues(),
            lightningDamageReduction = stats.LightningDmgReduction.ToAttributeValues(),
            fireDamageReduction = stats.FireDmgReduction.ToAttributeValues(),
            toxicDamageReduction = stats.ToxicDmgReduction.ToAttributeValues(),

            attackSpeed = stats.AttackSpeed.ToAttributeValues(),
            fervour = stats.Fervour.ToAttributeValues(),
            fervourStrength = stats.FervourStrength.ToAttributeValues(),
            rangedStrength = stats.RangedStrength.ToAttributeValues(),
            flask = stats.Flask.ToAttributeValues(),
            flaskHealth = stats.FlaskHealth.ToAttributeValues(),
            beadSlots = stats.BeadSlots.ToAttributeValues(),
            dashCooldown = stats.DashCooldown.ToAttributeValues(),
            dashDuration = stats.DashRide.ToAttributeValues(),
            purgeStrength = stats.PurgeStrength.ToAttributeValues(),
            meaCulpa = stats.MeaCulpa.ToAttributeValues(),
            parryWindow = stats.ParryWindow.ToAttributeValues(),
            righteousRiposteWindow = stats.ActiveRiposteWindow.ToAttributeValues(),
            physicalDamageMultiplier = stats.DamageMultiplier.ToAttributeValues(),
            prayerDurationAddition = stats.PrayerDurationAddition.ToAttributeValues(),
            prayerStrengthMultiplier = stats.PrayerStrengthMultiplier.ToAttributeValues(),
            prayerCostAddition = stats.PrayerCostAddition.ToAttributeValues(),
            airImpulses = stats.AirImpulses.ToAttributeValues(),
        };
    }
}

/// <summary>
/// Documenting useful values of a Blasphemous <see cref="EntityStats"/> for <see cref="Gameplay.GameControllers.Penitent.Penitent"/>
/// </summary>
public class EntityStatsValues_Penitent
{
    [JsonProperty] public AttributeValues life;
    [JsonProperty] public AttributeValues strength;
    [JsonProperty] public AttributeValues defense;
    [JsonProperty] public AttributeValues physicalDamageReduction;
    [JsonProperty] public AttributeValues contactDamageReduction;
    [JsonProperty] public AttributeValues magicDamageReduction;
    [JsonProperty] public AttributeValues lightningDamageReduction;
    [JsonProperty] public AttributeValues fireDamageReduction;
    [JsonProperty] public AttributeValues toxicDamageReduction;

    [JsonProperty] public AttributeValues attackSpeed;
    [JsonProperty] public AttributeValues fervour;
    [JsonProperty] public AttributeValues fervourStrength;
    [JsonProperty] public AttributeValues rangedStrength;
    [JsonProperty] public AttributeValues flask;
    [JsonProperty] public AttributeValues flaskHealth;
    [JsonProperty] public AttributeValues beadSlots;
    [JsonProperty] public AttributeValues dashCooldown;
    [JsonProperty] public AttributeValues dashDuration;
    [JsonProperty] public AttributeValues purgeStrength;
    [JsonProperty] public AttributeValues meaCulpa;
    [JsonProperty] public AttributeValues parryWindow;
    [JsonProperty] public AttributeValues righteousRiposteWindow;
    [JsonProperty] public AttributeValues physicalDamageMultiplier;
    [JsonProperty] public AttributeValues prayerDurationAddition;
    [JsonProperty] public AttributeValues prayerStrengthMultiplier;
    [JsonProperty] public AttributeValues prayerCostAddition;
    [JsonProperty] public AttributeValues airImpulses;
}

/// <summary>
/// Documenting useful values of a Blasphemous <see cref="EntityStats"/> for <see cref="Enemy"/>
/// </summary>
public class EntityStatsValues_Enemy
{
    [JsonProperty] public AttributeValues life;
    [JsonProperty] public AttributeValues strength;
    [JsonProperty] public AttributeValues defense;
    [JsonProperty] public AttributeValues physicalDamageReduction;
    [JsonProperty] public AttributeValues contactDamageReduction;
    [JsonProperty] public AttributeValues magicDamageReduction;
    [JsonProperty] public AttributeValues lightningDamageReduction;
    [JsonProperty] public AttributeValues fireDamageReduction;
    [JsonProperty] public AttributeValues toxicDamageReduction;
}