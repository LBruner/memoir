using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Item")]
public class ItemCard : Card
{
	public ItemCard()
	{
		Type = CardType.Item;
		Targetable = false;
		Expendable = true;
	}

	public override void ExecuteEffect(Card card, Player player)
	{
		foreach (CardEffect effect in Effects)
		{
			effect.ExecuteEffect(this, player);
		}
	}


	public override void ExecuteEffect(Card card, Player player, Enemy enemy)
	{
		foreach (CardEffect effect in Effects)
		{
			effect.ExecuteEffect(this, enemy);
		}
	}

	public override Card GetCopy()
	{
		return Instantiate(this);
	}

	public override string GetCardType()
	{
		return "Item";
	}
}
