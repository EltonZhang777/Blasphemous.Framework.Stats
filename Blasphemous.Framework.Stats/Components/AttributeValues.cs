using Framework.FrameworkCore.Attributes.Logic;
using Newtonsoft.Json;
using System;

namespace Blasphemous.Framework.Stats.Components;

/// <summary>
/// Documenting useful values of a Blasphemous <see cref="BlasAttribute"/>
/// </summary>
public class AttributeValues : IAccessible<BlasAttribute>
{
    [JsonProperty] public float baseValue = Main.DEFAULT_FLOAT;
    [JsonProperty] public float initialValue = Main.DEFAULT_FLOAT;
    [JsonProperty] public float upgradeIncrement = Main.DEFAULT_FLOAT;
    [JsonProperty] public int upgradeCount = Main.DEFAULT_INT;
    [JsonProperty] public float bonusValue = Main.DEFAULT_FLOAT;
    [JsonProperty] public float finalValue = Main.DEFAULT_FLOAT;

    /// <inheritdoc/>
    public void GetValueFrom(BlasAttribute attr)
    {
        if (!Main.Validate(attr, x => x != null))
            return;

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
        if (!Main.Validate(attr, x => x != null))
            return;

        // reset `_bonusValue` since it won't be properly reset in vanilla code
        Main.SetValue(ref attr, "Bonus", 0f, Main.TraverseAccessType.Property);

        // set values
        Main.SetValueIfValidated(ref attr, "Base", baseValue, Main.IsNotDefault, Main.TraverseAccessType.Property);
        Main.SetValueIfValidated(ref attr, "_initialValue", initialValue, Main.IsNotDefault, Main.TraverseAccessType.Field);
        Main.SetValueIfValidated(ref attr, "_upgradeValue", upgradeIncrement, Main.IsNotDefault, Main.TraverseAccessType.Field);

        // re-upgrade according to current upgrade count
        if (Main.IsNotDefault(upgradeCount))
        {
            SetUpgrades(attr);
        }
    }

    private void SetUpgrades(BlasAttribute attr)
    {
        attr.ResetUpgrades();
        for (int i = 0; i < upgradeCount; i++)
        {
            attr.Upgrade();
        }
    }
}