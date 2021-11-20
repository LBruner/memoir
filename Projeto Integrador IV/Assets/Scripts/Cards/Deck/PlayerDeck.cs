using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{

	public static PlayerDeck Instance;

	public Deck startingDeck;

	public List<Card> Cards;

	private void Awake()
	{
		if (Cards.Count == 0)
		{
			SetStartingDeck();
		}

		Instance = this;

		DontDestroyOnLoad(gameObject);
	}

	public void SetStartingDeck()
	{
		foreach (Card card in startingDeck.Cards)
		{
			Cards.Add(card.GetCopy());
		}
	}
}
