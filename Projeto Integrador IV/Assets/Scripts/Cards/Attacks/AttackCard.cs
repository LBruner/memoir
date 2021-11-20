using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Attack")]
public class AttackCard : Card
{
	public int MinDamage;
	public int MaxDamage;

	public bool PlayTwice;
	public bool CriticalDamagable;

	public AttackCard()
	{
		Type = CardType.Attack;
		Targetable = true;
	}

	public override void ExecuteEffect(Card card, Player player, Enemy enemy)
	{
		card.Used++;

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

	public virtual string GetCardDescription()
	{
		sb.Length = 0;
		foreach (CardEffect effect in Effects)
		{
			sb.AppendLine(effect.GetEffectDescription());
		}

		if (PlayTwice)
			sb.AppendLine("Play twice");

		if (CriticalDamagable)
			sb.AppendLine("Double damage if hit for " + MaxDamage);

		if (Expendable)
			sb.AppendLine("Expend");

		return sb.ToString();
	}
}
