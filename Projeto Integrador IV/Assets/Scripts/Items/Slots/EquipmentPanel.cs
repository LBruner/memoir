using System;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
	public GameObject EquipmentSlotPrefab;

	public List<EquipmentSlot> EquipmentSlots;
	[SerializeField] Transform equipmentSlotsParent;

	public event Action<BaseItemSlot> OnPointerEnterEvent;
	public event Action<BaseItemSlot> OnPointerExitEvent;
	public event Action<BaseItemSlot> OnRightClickEvent;

	private void Awake()
	{
		if (EquipmentSlots.Count == 0)
		{
			foreach(Item item in Player.Instance.Items)
			{
				AddItem(item as EquippableItem);
			}
		}
	}

	private void Start()
	{
		for (int i = 0; i < EquipmentSlots.Count; i++)
		{
			EquipmentSlots[i].OnPointerEnterEvent += slot => OnPointerEnterEvent(slot);
			EquipmentSlots[i].OnPointerExitEvent += slot => OnPointerExitEvent(slot);
			EquipmentSlots[i].OnRightClickEvent += slot => OnRightClickEvent(slot);
		}
	}


	private void Update()
	{
		
	}

	private void OnValidate()
	{
		//EquipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
	}

	public bool AddItem(EquippableItem item)
	{
		GameObject slotObject = Instantiate(EquipmentSlotPrefab);
		EquipmentSlot slot = slotObject.GetComponent<EquipmentSlot>();
		slot.Item = item;
		slot.Type = item.Type;
		slot.Amount = 1;

		EquipmentSlots.Add(slot);

		slotObject.transform.SetParent(equipmentSlotsParent);

		return true;
	}

	public bool AddItem(EquippableItem item, int i)
	{
		EquipmentSlot slot = new EquipmentSlot();
		slot.Item = item;
		slot.Type = item.Type;
		slot.Amount = 1;

		EquipmentSlots.Insert(i, slot);

		return true;
	}

	public bool RemoveItem(EquippableItem item)
	{
		for (int i = 0; i < EquipmentSlots.Count; i++)
		{
			if (EquipmentSlots[i].Item == item)
			{
				EquipmentSlots.RemoveAt(i);

				GameObject slot = equipmentSlotsParent.GetChild(i).gameObject;

				Destroy(slot);

				return true;
			}
		}

		return false;
	}
}
