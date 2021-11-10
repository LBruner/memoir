using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card Effects/Power Up")]
public class PowerUpCardEffect : CardEffect
{
	public int powerAmmount;

	public override void ExecuteEffect(Card card, Player player)
	{
		StatModifier statModifier = new StatModifier(powerAmmount, StatModType.Flat, true, card);
		player.DamageModifier.AddModifier(statModifier);
	}

	public override void ExecuteEffect(Card card, Enemy enemy)
	{
		throw new System.NotImplementedException();
	}

	public override string GetEffectDescription()
	{
		return "Power " + powerAmmount;
	}

	public override void RemoveEffect(Card card, Player player)
	{
		player.DamageModifier.RemoveAllModifiersFromSource(card);
	}

	public override void RemoveEffect(Card card, Enemy enemy)
	{
		throw new System.NotImplementedException();
		//enemy.DamageModifier.RemoveAllModifiersFromSource(card);
	}
}
