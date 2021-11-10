using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[Header("Status")]
	public int Level;
	public int MaxEnergy
	{
		get
		{
			return (int)(3 + EnergyModifier.Value);
		}
	}
	public int CurrentEnergy;
	public int GoldCoins;

	[SerializeField] int BaseHealth;
	public int MaxHealth
	{
		get
		{
			return (int)(BaseHealth + HealthModifier.Value);
		}
	}
	public int CurrentHealth;

	public int Defense;

	[Header("Modifiers")]
	public CharacterStat HealthModifier;
	public CharacterStat DamageModifier;
	public CharacterStat EnergyModifier;
	public CharacterStat DamageReducitonModifier;

	private void Awake()
	{
		CurrentHealth = MaxHealth;
	}

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}
