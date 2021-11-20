using System;

[Serializable]
public class ItemSaveData
{
	public string ItemID;
	public int Amount;

	public ItemSaveData(string id, int amount)
	{
		ItemID = id;
		Amount = amount;
	}
}

[Serializable]
public class InventorySaveData
{
	public ItemSaveData[] SavedItems;

	public InventorySaveData(int numItems)
	{
		SavedItems = new ItemSaveData[numItems];
	}
}
