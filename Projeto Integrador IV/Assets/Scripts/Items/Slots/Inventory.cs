using UnityEngine;

public class Inventory : ItemContainer
{
	[SerializeField] protected Transform itemsParent;

	protected override void OnValidate()
	{
		if (itemsParent != null)
			itemsParent.GetComponentsInChildren(includeInactive: true, result: ItemSlots);

		if (!Application.isPlaying) {
			
		}
	}

	protected override void Awake()
	{
		base.Awake();
	}
}
