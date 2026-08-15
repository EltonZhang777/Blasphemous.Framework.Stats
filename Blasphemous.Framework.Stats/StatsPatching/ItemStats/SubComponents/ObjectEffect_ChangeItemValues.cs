using Blasphemous.Framework.Stats.Components;
using Tools.Items;

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
        if (!Main.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        changeToNewItem = Main.GetValue<bool>(obj, "addObject", Main.TraverseAccessType.Field);
        autoEquipNewItem = Main.GetValue<bool>(obj, "equip", Main.TraverseAccessType.Field);
        newItem = Main.GetValue<InventoryObjectInspector>(obj, "NewItem", Main.TraverseAccessType.Field);
    }

    public void SetValueTo(ObjectEffect_ChangeItem obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        Main.SetValueIfNotNull(ref obj, "addObject", changeToNewItem, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "equip", autoEquipNewItem, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "NewItem", newItem, Main.TraverseAccessType.Field);
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
