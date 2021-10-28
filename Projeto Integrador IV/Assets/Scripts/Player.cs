using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Header("Status")]
	public int Level;
	public int MaxEnergy
	{
		get
		{
			return (int)EnergyModifier.Value;
		}
	}
	public int CurrentEnergy;
	public int GoldCoins;

	public int MaxHealth
	{
		get
		{
			return (int)HealthModifier.Value;
		}
	}
	public int CurrentHealth;

	public int Defense;

	[Header("Modifiers")]
	public CharacterStat HealthModifier;
	public CharacterStat EnergyModifier;
	public CharacterStat DamageModifier;
	public CharacterStat DamageReducitonModifier;
	public CharacterStat DrawModifier;
	public CharacterStat CostModifier;

	[Header("Deck")]
	public PlayerDeck Deck;

	private void Awake()
	{
		CurrentHealth = MaxHealth;
	}
}
