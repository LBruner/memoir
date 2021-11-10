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

	public override void ExecuteEffect(Player player, Enemy enemy)
	{
		Used++;

		foreach (CardEffect effect in Effects)
		{
			effect.ExecuteEffect(this, enemy);
		}

		Debug.Log("[Attack Card - " + this.Name + "] Damage Modifiers = " + player.DamageModifier.Value.ToString());

		Random.InitState(System.DateTime.Now.Millisecond);
		float damage = Random.Range(MinDamage + player.DamageModifier.Value, MaxDamage + player.DamageModifier.Value);

		Debug.Log("[Attack Card - " + this.Name + "] Damage = " + ((int)damage).ToString());

		int defenseAux = enemy.Defense;

		if (damage > enemy.Defense)
		{
			enemy.Defense = 0;
		}
		else
		{
			enemy.Defense -= (int)damage;
		}

		damage -= defenseAux;

		if (damage > 0)
		{
			enemy.CurrentHealth -= (int)damage;
		}
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
