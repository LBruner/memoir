using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICardContainer
{
    bool CanAddCard(Card card, int amount = 1);
    bool AddCard(Card card);

    Card RemoveCard(string cardId);
}
