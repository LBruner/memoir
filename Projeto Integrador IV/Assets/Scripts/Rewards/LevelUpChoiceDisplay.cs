using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelUpChoiceDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] Card oldCard;
	public Card NewCard;

	private GameObject rootDisplay;

	[SerializeField] GameObject parent;
	[SerializeField] GameObject frame;

	[SerializeField] GameObject cardDisplayPrefab;
	private CardDisplay cardDisplay;

	[SerializeField] Button button;

	private bool hoverign;

	private void OnValidate()
	{
		hoverign = false;

		//parent = transform.parent.gameObject;

		button = GetComponentInChildren<Button>();
		button.onClick.AddListener(OnClick);
	}

	void Start()
	{
		hoverign = false;

		frame = transform.parent.gameObject;
		parent = frame.transform.parent.gameObject;

		button = GetComponentInChildren<Button>();
		button.onClick.AddListener(OnClick);
	}

	public void SetupDisplay(Card _oldCard, Card card, GameObject _root)
	{
		NewCard = card;
		oldCard = _oldCard;
		rootDisplay = _root;

		GameObject displayObj = Instantiate(cardDisplayPrefab, new Vector3(transform.position.x, transform.position.y, 10f), Quaternion.identity);
		displayObj.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
		displayObj.transform.SetParent(transform);

		cardDisplay = displayObj.GetComponent<CardDisplay>();
		cardDisplay.card = card;
	}

	public void OnClick()
	{
		Player.Instance.Deck.Cards.Remove(oldCard);
		Player.Instance.Deck.Cards.Add(NewCard);

		frame.transform.localScale = Vector3.zero;

		Destroy(rootDisplay);
		Destroy(gameObject);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!hoverign)
		{
			hoverign = true;

			LeanTween.value(gameObject, 0.75f, 1.25f, 0.5f).setEaseLinear().setOnUpdate((value) =>
			{
				cardDisplay.transform.localScale = new Vector3(value, value, 1f);
			});
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (hoverign)
		{
			hoverign = false;

			LeanTween.value(gameObject, 1.25f, 0.75f, 0.5f).setEaseLinear().setOnUpdate((value) =>
			{
				cardDisplay.transform.localScale = new Vector3(value, value, 1f);
			});
		}
	}
}
