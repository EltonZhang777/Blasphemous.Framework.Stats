using Blasphemous.ModdingAPI;
using Framework.FrameworkCore.Attributes.Logic;
using Newtonsoft.Json;
using System;

namespace Blasphemous.Framework.Stats.Components;

/// <summary>
/// Documenting useful values of a Blasphemous <see cref="BlasAttribute"/>
/// </summary>
public class AttributeValues : IAccessible<BlasAttribute>
{
    [JsonProperty] public float baseValue;
    [JsonProperty] public float initialValue;
    [JsonProperty] public float upgradeIncrement;
    [JsonProperty] public int upgradeCount;
    [JsonProperty] public float bonusValue;
    [JsonProperty] public float finalValue;

    /// <inheritdoc/>
    public void GetValueFrom(BlasAttribute attr)
    {
        Main.Validate(attr, x => x != null);

        baseValue = Main.GetValue<BlasAttribute, float>(attr, "Base", Main.TraverseAccessType.Property);
        initialValue = Main.GetValue<BlasAttribute, float>(attr, "_initialValue", Main.TraverseAccessType.Field);
        upgradeIncrement = Main.GetValue<BlasAttribute, float>(attr, "_upgradeValue", Main.TraverseAccessType.Field);
        upgradeCount = attr.GetUpgrades();
        bonusValue = Main.GetValue<BlasAttribute, float>(attr, "Bonus", Main.TraverseAccessType.Property);
        finalValue = Main.GetValue<BlasAttribute, float>(attr, "Final", Main.TraverseAccessType.Property);
    }

    /// <summary>
    /// Write everything but bonusValue and finalValue of this instance to the specified <see cref="Attribute"/>. 
    /// Safe method of writing since it does not directly modify finalValue
    /// </summary>
    public void SetValueTo(BlasAttribute attr)
    {
        Main.Validate(attr, x => x != null);

        Main.SetValue(ref attr, "Base", baseValue, Main.TraverseAccessType.Property);
        Main.SetValue(ref attr, "_initialValue", initialValue, Main.TraverseAccessType.Field);
        Main.SetValue(ref attr, "_upgradeValue", upgradeIncrement, Main.TraverseAccessType.Field);

        // re-upgrade according to current upgrade count
        attr.ResetUpgrades();
        for (int i = 0; i < upgradeCount; i++)
        {
            attr.Upgrade();
        }
    }
}