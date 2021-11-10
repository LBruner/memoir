using System.IO;
using UnityEngine;

public class CardSaveIO
{
	private static readonly string baseSavePath;

	static CardSaveIO()
	{
		baseSavePath = Application.persistentDataPath;
	}

	public static void SaveDeck(DeckSaveData deck, string path)
	{
		Debug.Log("[Saving] Deck: " + baseSavePath + "/" + path + ".dat");
		string json = JsonUtility.ToJson(deck);
		File.WriteAllText(baseSavePath + "/" + path + ".dat", json);
	}

	public static DeckSaveData LoadDeck(string path)
	{
		string filePath = baseSavePath + "/" + path + ".dat";
		Debug.Log("[Loading] Deck: " + filePath);

		if (System.IO.File.Exists(filePath))
		{
			string json = File.ReadAllText(filePath);

			return JsonUtility.FromJson<DeckSaveData>(json);
		}

		return null;
	}
}
