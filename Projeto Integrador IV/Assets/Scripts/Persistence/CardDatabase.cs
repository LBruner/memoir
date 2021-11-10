using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class CardDatabase : ScriptableObject
{
	[SerializeField] Card[] cards;

	public Card GetCardReference(string cardId)
	{
		foreach (Card card in cards)
		{
			if (card.ID == cardId)
			{
				return card;
			}
		}

		return null;
	}

	public Card GetCardCopy(string cardId)
	{
		Card card = GetCardReference(cardId);
		return card != null ? card.GetCopy() : null;
	}

#if UNITY_EDITOR
	private void OnValidate()
	{
		LoadCards();
	}

	private void OnEnable()
	{
		EditorApplication.projectChanged -= LoadCards;
		EditorApplication.projectChanged += LoadCards;
	}

	private void OnDisable()
	{
		EditorApplication.projectChanged -= LoadCards;
	}

	private void LoadCards()
	{
		//Card[] attackCards = FindAssetsByType<Card>("Assets/Cards/Attacks");
		//Card[] abilityCards = FindAssetsByType<Card>("Assets/Cards/Ability");

		//cards.AddRange(abilityCards);
		//cards.AddRange(attackCards);

		cards = FindAssetsByType<Card>("Assets/Cards");
	}

	// Slightly modified version of this answer: http://answers.unity.com/answers/1216386/view.html
	public static T[] FindAssetsByType<T>(params string[] folders) where T : Object
	{
		string type = typeof(T).Name;

		string[] guids;
		if (folders == null || folders.Length == 0)
		{
			guids = AssetDatabase.FindAssets("t:" + type);
		}
		else
		{
			guids = AssetDatabase.FindAssets("t:" + type, folders);
		}

		T[] assets = new T[guids.Length];

		for (int i = 0; i < guids.Length; i++)
		{
			string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
			assets[i] = AssetDatabase.LoadAssetAtPath<T>(assetPath);
		}
		return assets;
	}
#endif
}
