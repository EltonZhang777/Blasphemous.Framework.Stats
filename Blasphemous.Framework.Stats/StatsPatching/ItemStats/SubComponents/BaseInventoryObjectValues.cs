using Blasphemous.Framework.Stats.Components;
using Framework.Inventory;
using Blasphemous.NewbieEltonLibs.Extensions.GameLibs;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents;

public class BaseInventoryObjectValues : IAccessible_Class<BaseInventoryObject>
{
    public bool? carryOnStart;
    public bool? preserveInNewGamePlus;

    /// <inheritdoc/>
    public void GetValueFrom(BaseInventoryObject obj)
    {
        carryOnStart = TraverseUtils.GetValue<bool>(obj, "carryonstart", TraverseUtils.TraverseAccessType.Field);
        preserveInNewGamePlus = TraverseUtils.GetValue<bool>(obj, "preserveInNewGamePlus", TraverseUtils.TraverseAccessType.Field);
    }

    /// <inheritdoc/>
    public void SetValueTo(BaseInventoryObject obj)
    {
        TraverseUtils.SetValueIfNotNull(ref obj, "carryonstart", carryOnStart, TraverseUtils.TraverseAccessType.Field);
        TraverseUtils.SetValueIfNotNull(ref obj, "preserveInNewGamePlus", preserveInNewGamePlus, TraverseUtils.TraverseAccessType.Field);
    }
}
