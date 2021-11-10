using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public static Player Instance;

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
	public DeckSystem DeckSystem;

	[Header("Equipment")]
	public List<Item> Items;
	public EquipmentPanel EquipmentPanel;

	[Header("System")]
	public CardSaveManager CardSaveManager;

	private void Awake()
	{
		HealthModifier.BaseValue = 40;
		EnergyModifier.BaseValue = 3; 

		CurrentHealth = MaxHealth;
		CurrentEnergy = MaxEnergy;

		if (CardSaveManager != null)
		{
			CardSaveManager.LoadDeck(Deck);
		}

		//if (PlayerSaveManager != null)
		//{

		//}

		Instance = this;

		DontDestroyOnLoad(gameObject);
	}

	private void OnDestroy()
	{
		if (CardSaveManager != null)
		{
			CardSaveManager.SavePlayerDeck(Deck);
		}
	}

	public void Equip(EquippableItem item)
	{
		if (EquipmentPanel.AddItem(item))
		{
			Debug.Log("Equipping: " + item.ItemName);
			Items.Add(item);
			item.Equip(this);
		}
	}

	public void Equip(EquippableItem item, int index)
	{
		if (EquipmentPanel.AddItem(item, index))
		{
			Debug.Log("Equipping: " + item.ItemName + " at slot " + index);
			Items.Insert(index, item);
			item.Equip(this);
		}
	}
}
