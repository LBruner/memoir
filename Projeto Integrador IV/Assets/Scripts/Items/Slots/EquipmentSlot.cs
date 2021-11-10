

public class EquipmentSlot : ItemSlot
{
	public EquipmentType Type;

	protected override void OnValidate()
	{
		base.OnValidate();
		gameObject.name = Type.ToString() + " Slot";
	}

	public override bool CanReceiveItem(Item item)
	{
		if (item == null)
			return true;

		EquippableItem equippableItem = item as EquippableItem;
		return equippableItem != null && equippableItem.Type == Type;
	}
}
