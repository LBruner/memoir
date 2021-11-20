using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card Effects/Defense")]
public class DefenseCardEffect : CardEffect
{
	public int DefenseAmount;

	public override void ExecuteEffect(Card card, Player player)
	{
		player.Defense += DefenseAmount;
	}

	public override void ExecuteEffect(Card card, Enemy enemy)
	{
		throw new System.NotImplementedException();
	}

	public override string GetEffectDescription()
	{
		return "Defends for " + DefenseAmount;
	}

	public override void RemoveEffect(Player player)
	{
		throw new System.NotImplementedException();
	}

	public override void RemoveEffect(Enemy enemy)
	{
		throw new System.NotImplementedException();
	}
}
