using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card Effects/Draw")]
public class DrawCardEffect : CardEffect
{
	public int DrawAmount;

	public override void ExecuteEffect(Card card, Player player)
	{
		player.DeckSystem.DrawCards(DrawAmount);
	}

	public override void ExecuteEffect(Card card, Enemy enemy)
	{
		throw new System.NotImplementedException();
	}

	public override string GetEffectDescription()
	{
		return "Draw " + DrawAmount + " cards";
	}

	public override void RemoveEffect(Card card, Player player)
	{
		throw new System.NotImplementedException();
	}

	public override void RemoveEffect(Card card, Enemy enemy)
	{
		throw new System.NotImplementedException();
	}
}
