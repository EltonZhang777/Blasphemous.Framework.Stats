using Blasphemous.Framework.Stats.Components;
using Framework.Inventory;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents;

public class BaseInventoryObjectValues : IAccessible_Class<BaseInventoryObject>
{
    public bool? carryOnStart;
    public bool? preserveInNewGamePlus;

    /// <inheritdoc/>
    public void GetValueFrom(BaseInventoryObject obj)
    {
        carryOnStart = Main.GetValue<BaseInventoryObject, bool>(obj, "carryonstart", Main.TraverseAccessType.Field);
        preserveInNewGamePlus = Main.GetValue<BaseInventoryObject, bool>(obj, "preserveInNewGamePlus", Main.TraverseAccessType.Field);
    }

    /// <inheritdoc/>
    public void SetValueTo(BaseInventoryObject obj)
    {
        Main.SetValueIfNotNull(ref obj, "carryonstart", carryOnStart, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "preserveInNewGamePlus", preserveInNewGamePlus, Main.TraverseAccessType.Field);
    }
}
