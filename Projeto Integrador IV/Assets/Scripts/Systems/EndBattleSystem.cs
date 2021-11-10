using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState
{
	ON_GOING,
	VICTORY,
	DEFEAT
}

public class EndBattleSystem : MonoBehaviour
{
	[SerializeField] EndBattleDisplay endBattleDisplay;

	[SerializeField] Player player;
	[SerializeField] Enemy enemy;

	BattleState endState;

	private void Start()
	{
		endState = BattleState.ON_GOING;
	}


	void Update()
	{
		if (player.CurrentHealth == 0)
		{
			endState = BattleState.VICTORY;

			endBattleDisplay.HandleEndBattleDisplay(endState);


			return;
		}

		if (enemy.CurrentHealth == 0)
		{
			endState = BattleState.DEFEAT;

			endBattleDisplay.HandleEndBattleDisplay(endState);

			return;
		}
	}

	public void OnContinueClick()
	{
		switch(endState)
		{

		}
	}
}
