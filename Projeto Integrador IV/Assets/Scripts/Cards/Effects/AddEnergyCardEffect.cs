using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card Effects/Draw")]
public class AddEnergyCardEffect : CardEffect
{
	public int EnergyAdded;

	public AddEnergyCardEffect()
	{
		Temp = false;
		Duration = 0;
	}

	public override void ExecuteEffect(Card card, Player player)
	{
		player.CurrentEnergy += EnergyAdded;
	}

	public override void ExecuteEffect(Card card, Enemy enemy)
	{
		throw new System.NotImplementedException();
	}

	public override string GetEffectDescription()
	{
		return "Energy " + EnergyAdded;
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
