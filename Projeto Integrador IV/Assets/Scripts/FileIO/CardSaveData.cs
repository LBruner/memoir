using System;
using UnityEngine;

[Serializable]
public class CardSaveData
{
	public string CardId;
	public int DeckIndex;

	public bool CanEvolve;
	public int Uses;
	public int Experience;

	public CardSaveData(string id, int index, bool canEvolve, int uses, int experience)
	{
		CardId = id;
		DeckIndex = index;
		CanEvolve = canEvolve;
		Uses = uses;
		Experience = experience;
	}
}

[Serializable]
public class DeckSaveData
{
	public CardSaveData[] SavedCards;

	public DeckSaveData(int numCards)
	{
		SavedCards = new CardSaveData[numCards];
	}
}
