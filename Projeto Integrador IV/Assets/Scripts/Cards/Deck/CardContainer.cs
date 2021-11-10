using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardContainer : MonoBehaviour, ICardContainer
{
	public bool AddCard(Card card)
	{
		return true;
	}

	public bool CanAddCard(Card card, int amount = 1)
	{
		//int freeSpaces = 0;

		return true;
	}

	public Card RemoveCard(string cardId)
	{
		throw new System.NotImplementedException();
	}
}
