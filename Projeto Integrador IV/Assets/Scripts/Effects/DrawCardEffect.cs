using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card Effects/Draw")]
public class DrawCardEffect : CardEffect
{
	public int DrawAmount;

	public override void ExecuteEffect(Card card, Player player)
	{
		player.Deck.DrawCards(DrawAmount);
	}

	public override void ExecuteEffect(Card card, Enemy enemy)
	{
		throw new System.NotImplementedException();
	}

	public override string GetEffectDescription()
	{
		return "Draw " + DrawAmount + " cards";
	}
}
