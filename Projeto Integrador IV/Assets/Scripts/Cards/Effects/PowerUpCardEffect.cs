using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card Effects/Power Up")]
public class PowerUpCardEffect : CardEffect
{
	public int powerAmmount;

	public override void ExecuteEffect(Card card, Player player)
	{
		StatModifier statModifier = new StatModifier(powerAmmount, StatModType.Flat, true, this);
		player.temporaryModifiers.Add(statModifier);
		player.DamageModifier.AddModifier(statModifier);
	}

	public override void ExecuteEffect(Card card, Enemy enemy)
	{
		throw new System.NotImplementedException();
	}

	public override string GetEffectDescription()
	{
		if (Temp && Duration > 0)
		{
			return "Power " + powerAmmount + " for " + Duration + " turns";
		}

		return "Power " + powerAmmount;
	}

	public override void RemoveEffect(Player player)
	{
		player.DamageModifier.RemoveAllModifiersFromSource(this);
	}

	public override void RemoveEffect(Enemy enemy)
	{
		throw new System.NotImplementedException();
		//enemy.DamageModifier.RemoveAllModifiersFromSource(card);
	}
}
