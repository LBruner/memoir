using System.Collections.Generic;
using UnityEngine;

public class ItemSaveManager : MonoBehaviour
{
    [SerializeField] ItemDatabase itemDatabase;

    private const string InventoryFileName = "Inventory";

    public void LoadInventory()
    {
        InventorySaveData savedItems = ItemSaveIO.LoadItems(InventoryFileName);
        if (savedItems == null) return;
        Player.Instance.Items.Clear();

        for (int i = 0; i < savedItems.SavedItems.Length; i++)
        {
            ItemSaveData savedItem = savedItems.SavedItems[i];

            Player.Instance.Equip(itemDatabase.GetItemCopy(savedItem.ItemID) as EquippableItem);
        }
    }

    public void SaveInventory()
    {
        SaveItems(Player.Instance.Items, InventoryFileName);
    }

    private void SaveItems(IList<Item> items, string fileName)
    {
        var saveData = new InventorySaveData(items.Count);

        for (int i = 0; i < saveData.SavedItems.Length; i++)
        {
            Item item = items[i];

            //saveData.SavedItems[i] = new ItemSaveData(item.ID, 1);
        }

        ItemSaveIO.SaveItems(saveData, fileName);
    }
}
