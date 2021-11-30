using System.IO;
using UnityEngine;

public static class ItemSaveIO
{
	private static readonly string baseSavePath;

	static ItemSaveIO()
	{
		baseSavePath = Application.persistentDataPath;
	}

	public static void SaveItems(InventorySaveData items, string path)
	{
		Debug.Log("[Saving] Inventory: " + baseSavePath + "/" + path + ".dat");
		string json = JsonUtility.ToJson(items);
		File.WriteAllText(baseSavePath + "/" + path + ".dat", json);
	}

	public static InventorySaveData LoadItems(string path)
	{
		string filePath = baseSavePath + "/" + path + ".dat";
		Debug.Log("[Loading] Inventory: " + filePath);

		if (System.IO.File.Exists(filePath))
		{
			string json = File.ReadAllText(filePath);

			return JsonUtility.FromJson<InventorySaveData>(json);
		}

		return null;
	}
}
