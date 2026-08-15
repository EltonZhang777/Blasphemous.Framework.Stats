using Blasphemous.Framework.Stats.Components;
using Tools.Items;
using Blasphemous.NewbieEltonLibs.Extensions.GameLibs;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents;

public class ObjectEffect_ChangeItemValues : ObjectEffectValues, IAccessible_Class<ObjectEffect_ChangeItem>
{
    /// <summary>
    /// Whether a new inventory object is added after triggering this effect.
    /// </summary>
    public bool? changeToNewItem;

    /// <summary>
    /// Whether to automatically equip the newly-added item
    /// </summary>
    public bool? autoEquipNewItem;

    public InventoryObjectInspector newItem;

    public void GetValueFrom(ObjectEffect_ChangeItem obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        changeToNewItem = TraverseUtils.GetValue<bool>(obj, "addObject", TraverseUtils.TraverseAccessType.Field);
        autoEquipNewItem = TraverseUtils.GetValue<bool>(obj, "equip", TraverseUtils.TraverseAccessType.Field);
        newItem = TraverseUtils.GetValue<InventoryObjectInspector>(obj, "NewItem", TraverseUtils.TraverseAccessType.Field);
    }

    public void SetValueTo(ObjectEffect_ChangeItem obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        TraverseUtils.SetValueIfNotNull(ref obj, "addObject", changeToNewItem, TraverseUtils.TraverseAccessType.Field);
        TraverseUtils.SetValueIfNotNull(ref obj, "equip", autoEquipNewItem, TraverseUtils.TraverseAccessType.Field);
        TraverseUtils.SetValueIfNotNull(ref obj, "NewItem", newItem, TraverseUtils.TraverseAccessType.Field);
    }

    public override void GetValueFrom(object obj)
    {
        if (obj is ObjectEffect_ChangeItem t)
        {
            GetValueFrom(t);
        }
        else
        {
            base.GetValueFrom(obj);
        }
    }

    public override void SetValueTo(object obj)
    {
        if (obj is ObjectEffect_ChangeItem t)
        {
            SetValueTo(t);
        }
        else
        {
            base.SetValueTo(obj);
        }
    }
}
