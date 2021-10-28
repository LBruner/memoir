using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Ability")]
public class AbilityCard : Card
{

	public AbilityCard()
	{
		Type = CardType.Ability;
		Targetable = false;
	}

	public override void ExecuteEffect(Player player)
	{
		Used++;

		foreach (CardEffect effect in Effects)
		{
			effect.ExecuteEffect(this, player);
		}
	}


	public override void ExecuteEffect(Enemy enemy)
	{
		Used++;

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
		return "Ability";
	}
}
