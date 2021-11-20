﻿using System.Text;
using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public enum CardType
{
	Attack,
	Ability,
	Item
}

[System.Serializable]
public class Card : ScriptableObject
{
	[SerializeField] string id;
	public string ID { get { return id; } }

	public string Name;
	public Sprite Artwork;

	public CardType Type;
	public bool Targetable;

	public int Cost;
	public int Used;
	public bool Expendable;

	public bool CanEvolve;
	public int Experience;
	public int ReqExperience;
	public bool IsEvolution;

	public List<CardEffect> Effects;

	public List<Card> Evolutions;

	protected static readonly StringBuilder sb = new StringBuilder();

#if UNITY_EDITOR
	protected virtual void OnValidate()
	{
		string path = AssetDatabase.GetAssetPath(this);
		id = AssetDatabase.AssetPathToGUID(path);
	}
#endif

	public virtual Card GetCopy()
	{
		return Instantiate(this);
	}

	public virtual void Destroy()
	{

	}

	public virtual void ExecuteEffect()
	{



	}
	public virtual void ExecuteEffect(Card card, Player player, Enemy enemy)
	{

	}

	public virtual void ExecuteEffect(Card card, Player player)
	{

	}

	public virtual string GetCardDescription()
	{
		sb.Length = 0;
		foreach (CardEffect effect in Effects)
		{
			sb.AppendLine(effect.GetEffectDescription());
		}

		if (Expendable)
		{
			sb.AppendLine("Expend");
		}

		return sb.ToString();
	}

	public virtual string GetCardType()
	{
		return "";
	}

	public Color getCardFrameColor()
	{
		Color color;

		switch (Type)
		{
			case CardType.Attack:
				//switch (Level)
				//{
				//	case 2:
				//		ColorUtility.TryParseHtmlString("#09FF0064", out color);
				//		break;

				//	case 3:
				//		ColorUtility.TryParseHtmlString("#09FF0063", out color);
				//		break;

				//	default:
				//		ColorUtility.TryParseHtmlString("#2b2927", out color);
				//		break;
				//}

				ColorUtility.TryParseHtmlString("#2b2927", out color);

				break;

			case CardType.Ability:
				ColorUtility.TryParseHtmlString("#1d7dd7", out color);
				break;

			case CardType.Item:
				ColorUtility.TryParseHtmlString("#38b121", out color);
				break;

			default:
				ColorUtility.TryParseHtmlString("#2b2927", out color);
				break;
		}

		return color;
	}
}
