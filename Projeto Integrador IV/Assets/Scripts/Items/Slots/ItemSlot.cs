using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : BaseItemSlot
{

	public override bool CanAddStack(Item item, int amount = 1)
	{
		return base.CanAddStack(item, amount) && Amount + amount <= item.MaximumStacks;
	}

	public override bool CanReceiveItem(Item item)
	{
		return true;
	}
}
