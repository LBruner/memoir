using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Attack")]
public class AttackCard : Card
{
	public int MinDamage;
	public int MaxDamage;

	public AttackCard()
	{
		Type = CardType.Attack;
		Targetable = true;
	}

	public override void ExecuteEffect(Enemy enemy)
	{
		Used++;

		foreach (CardEffect effect in Effects)
		{
			effect.ExecuteEffect(this, enemy);
		}

		enemy.CurrentHealth -= Random.Range(MinDamage, MaxDamage);
	}

	public override Card GetCopy()
	{
		return Instantiate(this);
	}

	public override string GetCardType()
	{
		return "Attack";
	}
}
