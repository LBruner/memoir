using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Turn
{
	Player,
	Enemy
}

public class TurnSystem : MonoBehaviour
{
	[SerializeField] Player player;
	[SerializeField] Enemy enemy;

	[SerializeField] Turn turn;

	[SerializeField] Text energyText;
	[SerializeField] Text turnText;
	[SerializeField] Text nextTurnText;
	[SerializeField] Button turnButton;

	[SerializeField] ProgressBar playerHealth;
	[SerializeField] ProgressBar playerDefense;
	[SerializeField] ProgressBar enemyHealth;

	void Start()
	{
		player.Deck.ClearCardFlags();
		BeginPlayerTurn();

		playerHealth.Maximum = player.MaxHealth;

		playerDefense.Maximum = player.MaxHealth;

		enemyHealth.Maximum = enemy.MaxHealth;
	}

	void Update()
	{
		playerHealth.Current = player.CurrentHealth;
		playerHealth.GetCurrentFill();

		playerDefense.Current = player.Defense;
		playerDefense.GetCurrentFill();

		enemyHealth.Current = enemy.CurrentHealth;
		enemyHealth.GetCurrentFill();
	}

	public Turn GetTurn()
	{
		return turn;
	}

	public void BeginPlayerTurn()
	{
		turn = Turn.Player;

		turnButton.gameObject.SetActive(false);

		nextTurnText.gameObject.SetActive(true);
		nextTurnText.text = "Your turn begins!";
		LeanTween.alpha(nextTurnText.gameObject, 0f, 1f).setOnComplete(() => { nextTurnText.gameObject.SetActive(false); });

		turnText.text = "Your turn";
		player.CurrentEnergy = player.MaxEnergy;
		energyText.text = player.CurrentEnergy + "/" + player.MaxEnergy;

		// Passar pelos modificadores e reduzir duraçãos dos temporários

		player.Deck.DrawCards(5);

		turnButton.gameObject.SetActive(true);
	}

	public void EndPlayerTurn()
	{
		nextTurnText.gameObject.SetActive(true);
		nextTurnText.text = "Turn Ended!";
		turnText.text = "";

		LeanTween.alpha(nextTurnText.gameObject, 0f, 1f).setOnComplete(() => { nextTurnText.gameObject.SetActive(false); });

		player.Deck.ClearHand();

		BeginEnemyTurn();
	}

	public void BeginEnemyTurn()
	{
		turn = Turn.Enemy;

		nextTurnText.gameObject.SetActive(true);
		nextTurnText.text = "Enemy turn begins!";
		LeanTween.alpha(nextTurnText.gameObject, 0f, 1f).setOnComplete(() => { nextTurnText.gameObject.SetActive(false); });

		turnText.text = "Enemy turn";
		//enemy.CurrentEnergy = enemy.MaxEnergy;
		// Passar pelos modificadores e reduzir duraçãos dos temporários

		player.CurrentHealth -= 5;

		EndEnemyTurn();
	}

	public void EndEnemyTurn()
	{
		nextTurnText.gameObject.SetActive(true);
		nextTurnText.text = "Turn Ended!";
		turnText.text = "";

		LeanTween.alpha(nextTurnText.gameObject, 0f, 1f).setOnComplete(() => { nextTurnText.gameObject.SetActive(false); });

		BeginPlayerTurn();
	}

	public void UpdateEnergyDisplay()
	{
		energyText.text = player.CurrentEnergy + "/" + player.MaxEnergy;
	}
}
