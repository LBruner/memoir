using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRewards : MonoBehaviour
{
	public static BattleRewards Instance;

	public int GoldCoins;
	public List<EquippableItem> Droppables;

	public List<EquippableItem> Trinkets;
	public List<Card> NewCards;
	public List<Card> LevelUpCards;

	void Awake()
	{
		Instance = this;

		DontDestroyOnLoad(gameObject);
	}

	public void SetLevelUpCards()
	{
		LevelUpCards.Clear();

		Player.Instance.Deck.Cards.ForEach(card =>
		{
			Debug.Log("Card - " + card.name + ": " + card.Used + " | " + card.ReqExperience + "(" + card.CanEvolve + ")");

			if (card.Used >= card.ReqExperience && card.CanEvolve)
			{
				LevelUpCards.Add(card);
			}
		});
	}

	public void SetTrinkets()
	{
		Trinkets.Clear();

		//for (int i = 0; i < 3; i++)
		//{

		//Random.InitState(System.DateTime.Now.Millisecond);
		//int index = Random.Range(0, Droppables.Count - 1);

		//while (Trinkets.Contains(Droppables[index]))
		//{
		Random.InitState(System.DateTime.Now.Millisecond);
		int index = Random.Range(0, Droppables.Count - 1);
		//}

		Trinkets.Add(Droppables[index]);
		//}
	}
}
