using System;
using System.Threading.Tasks;
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

	[SerializeField] DeckSystem playerDeck;

	[SerializeField] Turn turn;

	[SerializeField] Text energyText;
	[SerializeField] Text turnText;
	[SerializeField] Text nextTurnText;
	[SerializeField] Button turnButton;

	[SerializeField] ProgressBar playerHealth;
	[SerializeField] ProgressBar playerDefense;
	[SerializeField] ProgressBar enemyHealth;
	[SerializeField] ProgressBar enemyDefense;

	void Start()
	{
		playerDeck.ClearCardFlags();
		playerDeck.InitPlayerDeck();
		playerDeck.Shuffle();

		BeginPlayerTurn();

		playerHealth.Maximum = player.MaxHealth;

		playerDefense.Maximum = player.MaxHealth;

		enemyHealth.Maximum = enemy.MaxHealth;
		enemyDefense.Maximum = enemy.MaxHealth;
	}

	void Update()
	{
		playerHealth.Current = player.CurrentHealth;
		playerHealth.GetCurrentFill();

		playerDefense.Current = player.Defense;
		playerDefense.GetCurrentFill();

		enemyHealth.Current = enemy.CurrentHealth;
		enemyHealth.GetCurrentFill();

		enemyDefense.Current = enemy.Defense;
		enemyDefense.GetCurrentFill();
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

		playerDeck.DrawCards(5);

		turnButton.gameObject.SetActive(true);
	}

	public void EndPlayerTurn()
	{
		nextTurnText.gameObject.SetActive(true);
		nextTurnText.text = "Turn Ended!";
		turnText.text = "";

		LeanTween.alpha(nextTurnText.gameObject, 0f, 1f).setOnComplete(() => { nextTurnText.gameObject.SetActive(false); });

		playerDeck.ClearHand();

		BeginEnemyTurn();
	}

	public async void BeginEnemyTurn()
	{
		turn = Turn.Enemy;

		nextTurnText.gameObject.SetActive(true);
		nextTurnText.text = "Enemy turn begins!";
		LeanTween.alpha(nextTurnText.gameObject, 0f, 1f).setOnComplete(() => { nextTurnText.gameObject.SetActive(false); });

		turnText.text = "Enemy turn";

		await Task.Delay(TimeSpan.FromSeconds(1f));
		//enemy.CurrentEnergy = enemy.MaxEnergy;
		// Passar pelos modificadores e reduzir duraçãos dos temporários

		int damage = 5;
		int defenseAux = player.Defense;

		if (damage > player.Defense)
		{
			player.Defense = 0;
		}
		else
		{
			player.Defense -= damage;
		}

		damage -= defenseAux;

		if (damage > 0)
		{
			player.CurrentHealth -= damage;
		}

		await Task.Delay(TimeSpan.FromSeconds(0.25f));

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
