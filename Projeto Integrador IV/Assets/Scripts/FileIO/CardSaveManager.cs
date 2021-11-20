using System.Collections.Generic;
using UnityEngine;

public class CardSaveManager : MonoBehaviour
{
	[SerializeField] CardDatabase cardDatabase;

	private const string DeckFileName = "PlayerDeck";

	public void LoadDeck(PlayerDeck playerDeck)
	{
		DeckSaveData savedDeck = CardSaveIO.LoadDeck(DeckFileName);

		if (savedDeck == null)
			return;

		playerDeck.Cards.Clear();

		for (int i = 0; i < savedDeck.SavedCards.Length; i++)
		{
			CardSaveData savedCard = savedDeck.SavedCards[i];

			if (savedCard != null)
			{
				Debug.Log("[Loading] Card: " + JsonUtility.ToJson(savedCard));

				Card card = cardDatabase.GetCardCopy(savedCard.CardId);
				card.CanEvolve = savedCard.CanEvolve;
				card.Experience = savedCard.Experience;
				card.Used = savedCard.Uses;

				playerDeck.Cards.Insert(savedCard.DeckIndex, card);
			}
		}
	}

	public void SavePlayerDeck(PlayerDeck playerDeck)
	{
		SaveDeck(playerDeck.Cards, DeckFileName);
	}

	private void SaveDeck(List<Card> cards, string fileName)
	{
		var saveData = new DeckSaveData(cards.Count);

		for(int i = 0; i < saveData.SavedCards.Length; i++)
		{
			Debug.Log("[Saving - Card(" + i + ")] - Card: " + cards[i].Name + " " + cards[i].Used);
			Card card = cards[i];
			saveData.SavedCards[i] = new CardSaveData(card.ID, i, card.CanEvolve, card.Experience, card.Used);
		}

		CardSaveIO.SaveDeck(saveData, fileName);
	}
}

