using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class DeckSystem : MonoBehaviour
{
	public Player player;
	public PlayerDeck playerDeck;

	public List<Card> Deck;
	public List<Card> Discard;

	public Text deckText;
	public Text discardText;

	public GameObject HandParent;
	public GameObject DrawPanel;
	public GameObject DiscardPanel;

	public Camera canvasCamera;

	public GameObject CardPrefab;
	public GameObject PlaceholderCardPrefab;

	private void Start()
	{
		player = Player.Instance;
		playerDeck = Player.Instance.Deck;
	}

	public void InitPlayerDeck()
	{
		Deck.AddRange(playerDeck.Cards);
	}

	public void Shuffle()
	{
		for (int i = 0; i < Deck.Count; i++)
		{
			int r = i + (int)(Random.Range(0f, 1f) * (Deck.Count - i));
			Card aux = Deck[i];

			Deck[i] = Deck[r];
			Deck[r] = aux;

			deckText.text = (i + 1).ToString();
		}
	}

	public void ClearCardFlags()
	{
		foreach (Card card in playerDeck.Cards)
		{
			card.Used = 0;
		}
	}

	private void RebuilDeck()
	{
		Discard.Clear();
		discardText.text = Discard.Count.ToString();

		List<Card> tempCards = new List<Card>();
		tempCards.AddRange(playerDeck.Cards);

		Debug.Log("TempSize: " + tempCards.Count);

		CardDisplay[] cardsInHand = HandParent.GetComponentsInChildren<CardDisplay>();
		for (int i = 0; i < cardsInHand.Length; i++)
		{
			Debug.Log("Remove at: " + tempCards.FindIndex(c => c.Equals(cardsInHand[i].card)));
			tempCards.RemoveAt(tempCards.FindIndex(c => c.Equals(cardsInHand[i].card)));

			Debug.Log("[New] TempSize: " + tempCards.Count);
		}

		Deck.Clear();
		Deck.AddRange(tempCards);

		Shuffle();
	}

	public async void DrawCards(int quantity)
	{

		for (int i = 0; i < (quantity + player.DrawModifier.Value); i++)
		{
			if (Deck.Count == 0)
				RebuilDeck();

			GameObject cardDisplay = Instantiate(CardPrefab, new Vector3(transform.position.x, transform.position.y, 10f), Quaternion.identity);
			cardDisplay.transform.SetParent(HandParent.transform);
			cardDisplay.transform.SetSiblingIndex(HandParent.transform.childCount - 1);
			cardDisplay.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
			cardDisplay.SetActive(false);

			GameObject placeholderCardDisplay = Instantiate(PlaceholderCardPrefab, new Vector3(DrawPanel.transform.position.x, cardDisplay.transform.position.y, 9f), Quaternion.identity);
			placeholderCardDisplay.transform.SetParent(HandParent.transform.parent);
			placeholderCardDisplay.transform.Rotate(new Vector3(0, 180, 0), Space.World);
			placeholderCardDisplay.transform.localScale = new Vector3(0.75f, 0.75f, 1f);

			CardDisplay display = cardDisplay.GetComponent<CardDisplay>();
			display.card = Deck[0];

			display.FrameRenderer.materials[0].color = Deck[0].getCardFrameColor();
			display.UpdateDisplay();

			CardDisplay placeholderDisplay = placeholderCardDisplay.GetComponent<CardDisplay>();
			placeholderDisplay.card = Deck[0];

			placeholderDisplay.FrameRenderer.materials[0].color = Deck[0].getCardFrameColor();
			placeholderDisplay.UpdateDisplay();

			Deck.RemoveAt(0);
			deckText.text = Deck.Count.ToString();

			LeanTween.rotateY(placeholderCardDisplay, 0, 0.75f);
			LeanTween.moveX(placeholderCardDisplay, HandParent.transform.position.x, 0.75f).setOnComplete(async () =>
			{
				//LeanTween.alpha(placeholderCardDisplay, 0f, 0.5f).setOnComplete(() =>
				//{
				await WaitOneSecondAsync(0.5f);
				Destroy(placeholderCardDisplay);
				cardDisplay.SetActive(true);
				//});
			});

			await WaitOneSecondAsync(2f);
		}

		deckText.text = Deck.Count.ToString();
	}

	public void ClearHand()
	{
		Debug.Log("Hand Size: " + HandParent.transform.childCount);

		for (int i = HandParent.transform.childCount - 1; i >= 0; i--)
		{
			Debug.Log("Card: " + i);

			GameObject cardObject = HandParent.transform.GetChild(i).gameObject;
			CardDisplay cardDisplay = cardObject.GetComponent<CardDisplay>();

			Discard.Add(cardDisplay.card);

			GameObject placeholderObject = Instantiate(PlaceholderCardPrefab, new Vector3(cardObject.transform.position.x, cardObject.transform.position.y, 9f), Quaternion.identity);
			placeholderObject.transform.SetParent(HandParent.transform.parent);
			placeholderObject.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
			placeholderObject.layer = 5;

			cardObject.SetActive(false);

			CardDisplay placeholderDisplay = placeholderObject.GetComponent<CardDisplay>();
			placeholderDisplay.card = cardDisplay.card;

			placeholderDisplay.FrameRenderer.materials[0].color = placeholderDisplay.card.getCardFrameColor();
			placeholderDisplay.UpdateDisplay();

			LeanTween.moveX(placeholderObject, DiscardPanel.transform.position.x, 0.5f).setOnComplete(() =>
			{
				Destroy(cardObject);
				Destroy(placeholderObject);

				discardText.text = Discard.Count.ToString();
			}).delay = i > 0 ? (float)i / 5 : 0;
		}
	}

	private async Task WaitOneSecondAsync(float seconds)
	{
		await Task.Delay(System.TimeSpan.FromSeconds(seconds));
	}
}
