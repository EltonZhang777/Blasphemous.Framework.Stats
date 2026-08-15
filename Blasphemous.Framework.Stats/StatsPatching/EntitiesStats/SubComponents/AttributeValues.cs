using Blasphemous.Framework.Stats.Components;
using Framework.FrameworkCore.Attributes.Logic;
using System;

namespace Blasphemous.Framework.Stats.StatsPatching.EntitiesStats.SubComponents;

/// <summary>
/// Documenting useful values of a Blasphemous <see cref="BlasAttribute"/>
/// </summary>
public class AttributeValues : IAccessible_Class<BlasAttribute>
{
    public float? baseValue;
    public float? initialValue;
    public float? upgradeIncrement;
    public int? upgradeCount;
    public float? bonusValue;
    public float? finalValue;

    /// <inheritdoc/>
    public void GetValueFrom(BlasAttribute attr)
    {
        if (!Main.Validate(attr, x => x != null))
            return;

        baseValue = Main.GetValue<float>(attr, "Base", Main.TraverseAccessType.Property);
        initialValue = Main.GetValue<float>(attr, "_initialValue", Main.TraverseAccessType.Field);
        upgradeIncrement = Main.GetValue<float>(attr, "_upgradeValue", Main.TraverseAccessType.Field);
        upgradeCount = attr.GetUpgrades();
        bonusValue = Main.GetValue<float>(attr, "Bonus", Main.TraverseAccessType.Property);
        finalValue = Main.GetValue<float>(attr, "Final", Main.TraverseAccessType.Property);
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
        Main.SetValueIfNotNull(ref attr, "Base", baseValue, Main.TraverseAccessType.Property);
        Main.SetValueIfNotNull(ref attr, "_initialValue", initialValue, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref attr, "_upgradeValue", upgradeIncrement, Main.TraverseAccessType.Field);

        // re-upgrade according to current upgrade count
        if (upgradeCount.HasValue)
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