using Blasphemous.Framework.Stats.Components;
using Framework.FrameworkCore.Attributes.Logic;
using System;
using Blasphemous.NewbieEltonLibs.Extensions.GameLibs;

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
        if (!TraverseUtils.Validate(attr, x => x != null))
            return;

        baseValue = TraverseUtils.GetValue<float>(attr, "Base", TraverseUtils.TraverseAccessType.Property);
        initialValue = TraverseUtils.GetValue<float>(attr, "_initialValue", TraverseUtils.TraverseAccessType.Field);
        upgradeIncrement = TraverseUtils.GetValue<float>(attr, "_upgradeValue", TraverseUtils.TraverseAccessType.Field);
        upgradeCount = attr.GetUpgrades();
        bonusValue = TraverseUtils.GetValue<float>(attr, "Bonus", TraverseUtils.TraverseAccessType.Property);
        finalValue = TraverseUtils.GetValue<float>(attr, "Final", TraverseUtils.TraverseAccessType.Property);
    }

    /// <summary>
    /// Write everything but bonusValue and finalValue of this instance to the specified <see cref="Attribute"/>. 
    /// Safe method of writing since it does not directly modify finalValue
    /// </summary>
    public void SetValueTo(BlasAttribute attr)
    {
        if (!TraverseUtils.Validate(attr, x => x != null))
            return;

        // reset `_bonusValue` since it won't be properly reset in vanilla code
        TraverseUtils.SetValue(ref attr, "Bonus", 0f, TraverseUtils.TraverseAccessType.Property);

        // set values
        TraverseUtils.SetValueIfNotNull(ref attr, "Base", baseValue, TraverseUtils.TraverseAccessType.Property);
        TraverseUtils.SetValueIfNotNull(ref attr, "_initialValue", initialValue, TraverseUtils.TraverseAccessType.Field);
        TraverseUtils.SetValueIfNotNull(ref attr, "_upgradeValue", upgradeIncrement, TraverseUtils.TraverseAccessType.Field);

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