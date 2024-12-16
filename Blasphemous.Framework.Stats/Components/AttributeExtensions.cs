using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.FrameworkCore.Attributes.Logic;
using HarmonyLib;
using Newtonsoft.Json;
using Attribute = Framework.FrameworkCore.Attributes.Logic.Attribute;

namespace Blasphemous.Framework.Stats.Components;

/// <summary>
/// Useful extensions for inspecting <see cref="Attribute"/>
/// </summary>
public static class AttributeExtensions
{
    /// <summary>
    /// Get base value of the given attribute
    /// </summary>
    public static float GetBase(this Attribute attr)
    {
        return Traverse.Create(attr).Field("_initialValue").GetValue<float>();
    }

    /// <summary>
    /// Get value increased at each upgrade of the given attribute
    /// </summary>
    public static float GetUpgradeIncrement(this Attribute attr)
    {
        return Traverse.Create(attr).Field("_upgradeValue").GetValue<float>();
    }

    /// <summary>
    /// Get the total value given by upgrading the given attribute
    /// </summary>
    public static float GetPermanentBonus(this Attribute attr)
    {
        return attr.PermanetBonus;
    }

    /// <summary>
    /// Get the list of raw bonuses applied to the given attribute
    /// </summary>
    public static List<RawBonus> GetRawBonuses(this Attribute attr)
    {
        return Traverse.Create(attr).Field("_rawBonuses").GetValue<List<RawBonus>>();
    }

    /// <summary>
    /// Get the list of final bonuses applied to the given attribute
    /// </summary>
    public static List<FinalBonus> GetFinalBonuses(this Attribute attr)
    {
        return Traverse.Create(attr).Field("_finalBonuses").GetValue<List<FinalBonus>>();
    }

    /// <summary>
    /// Write the useful values of the given attribute to an <see cref="AttributeValues"/> instance
    /// </summary>
    public static AttributeValues ToAttributeValues(this Attribute attr)
    {
        return new AttributeValues()
        {
            baseValue = attr.GetBase(),
            upgradeIncrement = attr.GetUpgradeIncrement(),
            upgradeCount = attr.GetUpgrades(),
            bonusValueWithoutUpgrades = attr.Bonus - attr.GetUpgradeIncrement() * attr.GetUpgrades(),
            finalValue = attr.Final,
        };
    }
}

/// <summary>
/// Documenting useful values of a Blasphemous <see cref="Attribute"/>
/// </summary>
public class AttributeValues
{
    [JsonProperty] public float baseValue;
    [JsonProperty] public float upgradeIncrement;
    [JsonProperty] public int upgradeCount;
    [JsonProperty] public float bonusValueWithoutUpgrades;
    [JsonProperty] public float finalValue;
}