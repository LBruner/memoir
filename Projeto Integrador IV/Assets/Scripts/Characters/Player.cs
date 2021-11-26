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

    public List<StatModifier> temporaryModifiers;

    [Header("Deck")]
    public PlayerDeck Deck;
    public DeckSystem DeckSystem;

    [Header("Equipment")]
    public List<Item> Items;
    public EquipmentPanel EquipmentPanel;

    [Header("System")]
    public CardSaveManager CardSaveManager;
    public ItemSaveManager ItemSaveManager;

    public bool CanSave;

    private void Awake()
    {
        if (Instance != null) return;

        CanSave = true;

        Deck = PlayerDeck.Instance;

        HealthModifier.BaseValue = 40;
        EnergyModifier.BaseValue = 3;

        CurrentHealth = MaxHealth;
        CurrentEnergy = MaxEnergy;

        if (CardSaveManager != null)
        {
            CardSaveManager.LoadDeck(Deck);
        }

        if (ItemSaveManager != null)
        {
            ItemSaveManager.LoadInventory();
        }

        //if (PlayerSaveManager != null)
        //{

        //}

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        if (CanSave)
        {
            if (CardSaveManager != null)
            {
                CardSaveManager.SavePlayerDeck(Deck);
            }

            if (ItemSaveManager != null)
            {
                ItemSaveManager.SaveInventory();
            }
        }
    }

    public void Equip(EquippableItem item)
    {
        Debug.Log("Equipping: " + item.ItemName);
        Items.Add(item);
        item.Equip(this);

        if (EquipmentPanel != null)
        {
            EquipmentPanel.AddItem(item);
        }
    }

    public void Equip(EquippableItem item, int index)
    {
        Debug.Log("Equipping: " + item.ItemName + " at slot " + index);
        Items.Insert(index, item);
        item.Equip(this);

        if (EquipmentPanel != null)
        {
            EquipmentPanel.AddItem(item, index);
        }
    }
}
