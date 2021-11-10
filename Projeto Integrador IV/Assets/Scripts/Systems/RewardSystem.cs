using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardSystem : MonoBehaviour
{
	public static RewardSystem Instance;

	public List<Reward> Rewards;

	private void Awake()
	{
		Instance = this;

		DontDestroyOnLoad(gameObject);
	}


	public void ClearRewards()
	{
		Rewards.Clear();
	}

	public void AddReward(Reward reward)
	{
		Rewards.Add(reward);
	}
}
