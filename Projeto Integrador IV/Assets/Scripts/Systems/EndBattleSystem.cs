using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState
{
	ON_GOING,
	VICTORY,
	DEFEAT
}

public class EndBattleSystem : MonoBehaviour
{
	[SerializeField] EndBattleDisplay endBattleDisplay;

	[SerializeField] Enemy enemy;
	[SerializeField] TurnSystem turnSystem;

	BattleState endState;

	bool triggered;

	private void Start()
	{
		endState = BattleState.ON_GOING;

		triggered = false;

		Player.Instance.CanSave = false;
	}


	void Update()
	{
		if (!triggered)
		{
			if (endState != BattleState.ON_GOING)
			{
				turnSystem.endBattle();

				triggered = true;
			}

			if (Player.Instance.CurrentHealth <= 0)
			{
				endState = BattleState.DEFEAT;


				endBattleDisplay.HandleEndBattleDisplay(endState);

				return;
			}

			if (enemy.CurrentHealth <= 0)
			{
				endState = BattleState.VICTORY;

				endBattleDisplay.HandleEndBattleDisplay(endState);

				return;
			}
		}
	}

	public void OnContinueClick()
	{
		switch (endState)
		{
			case BattleState.VICTORY:
				BattleRewards.Instance.SetLevelUpCards();
				BattleRewards.Instance.SetTrinkets();
				BattleRewards.Instance.GoldCoins = enemy.GoldCoins;
				SceneManager.LoadScene("RewardScene");
				break;

			case BattleState.DEFEAT:
				Destroy(BattleRewards.Instance.gameObject);
				break;

			default:
				Debug.LogError("BattleState is not set correctly");
				break;
		}
	}
}
