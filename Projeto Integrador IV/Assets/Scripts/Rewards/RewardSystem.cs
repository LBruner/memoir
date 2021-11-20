using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RewardSystem : MonoBehaviour
{
	public List<Reward> Rewards;

	[SerializeField] GameObject RewardParent;
	[SerializeField] GameObject RewardDisplayPrefab;

	private void Start()
	{

		AddRewards();
		DisplayRewards();

		//Destroy(BattleRewards.Instance.gameObject);
	}

	private void Update()
	{
		if (RewardParent.transform.childCount == 0)
		{
			Player.Instance.ItemSaveManager.SaveInventory();

			Destroy(BattleRewards.Instance.gameObject);
			SceneManager.LoadScene("Map");
		}
	}

	void AddRewards()
	{
		Rewards.Clear();

		if (BattleRewards.Instance.GoldCoins > 0)
		{
			Reward goldReward = new Reward();

			goldReward.Priority = 0;
			goldReward.Type = RewardType.Gold;
			goldReward.Description = "Gained " + BattleRewards.Instance.GoldCoins + " gold coins";

			Rewards.Add(goldReward);
		}

		BattleRewards.Instance.Trinkets.ForEach(item =>
		{
			Reward reward = new Reward();

			reward.Priority = 1;
			reward.Type = RewardType.Trinket;
			reward.Description = "Gained " + item.ItemName;

			Rewards.Add(reward);
		});

		BattleRewards.Instance.LevelUpCards.ForEach(card =>
		{
			Reward reward = new Reward();

			reward.Priority = 2;
			reward.Type = RewardType.LevelUp;
			reward.Description = "Level up " + card.Name;
			reward.PreviousCard = card;

			Rewards.Add(reward);
		});

		BattleRewards.Instance.NewCards.ForEach(card =>
		{
			Reward reward = new Reward();

			reward.Priority = card.Type != CardType.Item ? 3 : 4;
			reward.Type = card.Type != CardType.Item ? RewardType.Card : RewardType.Item;
			reward.Description = "Gained " + card.Name;

			Rewards.Add(reward);
		});

		Rewards.Sort(CompareModifierOrder);
	}

	void DisplayRewards()
	{
		Rewards.ForEach(reward =>
		{
			GameObject rewardDisplayObject = Instantiate(RewardDisplayPrefab, new Vector3(transform.position.x, transform.position.y, 10f), Quaternion.identity);
			rewardDisplayObject.transform.SetParent(RewardParent.transform);
			rewardDisplayObject.transform.SetSiblingIndex(RewardParent.transform.childCount - 1);
			rewardDisplayObject.transform.localScale = Vector3.one;
			rewardDisplayObject.SetActive(true);

			RewardDislay rewardDislay = rewardDisplayObject.GetComponent<RewardDislay>();
			rewardDislay.SetupDisplay(reward);
		});
	}

	protected int CompareModifierOrder(Reward a, Reward b)
	{
		if (a.Priority < b.Priority)
			return -1;
		else if (a.Priority > b.Priority)
			return 1;

		return 0; // if (a.Order == b.Order)
	}
}
