using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{

	public Deck startingDeck;

	public List<Card> Cards;

	private void Start()
	{
		if (Cards.Count == 0)
		{
			SetStartingDeck();
		}
	}

	public void SetStartingDeck()
	{
		foreach (Card card in startingDeck.Cards)
		{
			Cards.Add(card.GetCopy());
		}
	}
}
