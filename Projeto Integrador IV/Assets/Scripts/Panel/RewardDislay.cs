using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RewardDislay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] RewardType type;
	[SerializeField] Sprite[] typeSprites;

	[SerializeField] Button button;

	[SerializeField] Text title;
	[SerializeField] Text description;

	[SerializeField] Image typeSlot;
	[SerializeField] Image typeIcon;
	[SerializeField] Image textBackground;

	[SerializeField] Color imgIdleColor;
	[SerializeField] Color imgHoveringColor;

	[SerializeField] Color txtIdleColor;
	[SerializeField] Color txtHoveringColor;

	private bool hoverign;

	[SerializeField] GameObject choiceCanvas;
	[SerializeField] GameObject choiceParent;
	[SerializeField] GameObject trinketChoicePrefab;

	[SerializeField] GameObject levelupCanvas;
	[SerializeField] GameObject levelupParent;
	[SerializeField] GameObject levelupPlaceholder;
	[SerializeField] GameObject levelupChoicePrefab;
	[SerializeField] GameObject CardPrefab;

	private void OnValidate()
	{
		hoverign = false;

		Text[] texts = GetComponentsInChildren<Text>();
		title = texts[0];
		description = texts[1];

		Image[] images = GetComponentsInChildren<Image>();
		typeSlot = images[1];
		typeIcon = images[2];
		textBackground = images[3];

		button = GetComponentInChildren<Button>();

		//choiceCanvas = GameObject.Find("Choices Parent");
		//choiceParent = GameObject.Find("Choices Parent/Frame");

		//choiceCanvas.transform.localScale = Vector3.zero;
	}

	void Start()
	{
		hoverign = false;

		Text[] texts = GetComponentsInChildren<Text>();
		title = texts[0];
		description = texts[1];

		Image[] images = GetComponentsInChildren<Image>();
		typeSlot = images[1];
		typeIcon = images[2];
		textBackground = images[3];

		choiceCanvas = GameObject.Find("Choices Parent");
		choiceParent = GameObject.Find("Choices Parent/Frame");

		choiceCanvas.transform.localScale = Vector3.zero;

		levelupCanvas = GameObject.Find("Levelup Parent");
		levelupParent = GameObject.Find("Levelup Parent/Frame");
		levelupPlaceholder = GameObject.Find("Levelup Parent/Card Placeholder");

		levelupCanvas.transform.localScale = Vector3.zero;
	}

	public void SetupDisplay(Reward reward)
	{
		type = reward.Type;

		switch (type)
		{
			case RewardType.Card:
				title.text = "A new Card";
				typeIcon.sprite = typeSprites[(int)RewardType.Card];

				button.onClick.AddListener(() =>
				{

				});

				break;
			case RewardType.Gold:
				title.text = "Gold coins";
				typeIcon.sprite = typeSprites[(int)RewardType.Gold];

				button.onClick.AddListener(() =>
				{
					Player.Instance.GoldCoins += BattleRewards.Instance.GoldCoins;
					Destroy(gameObject);
				});

				break;
			case RewardType.LevelUp:
				title.text = "A Card evolved";
				typeIcon.sprite = typeSprites[(int)RewardType.LevelUp];

				button.onClick.AddListener(() =>
				{
					LevelupSetup(reward.PreviousCard);
				});

				break;
			case RewardType.Trinket:
				title.text = "A new Trinket";
				typeIcon.sprite = typeSprites[(int)RewardType.Trinket];

				button.onClick.AddListener(TrinketChoiceSetup);

				break;
			case RewardType.Item:
				title.text = "A new Item";
				typeIcon.sprite = typeSprites[(int)RewardType.Item];

				button.onClick.AddListener(() =>
				{

				});

				break;
		}

		description.text = reward.Description;
	}

	private void TrinketChoiceSetup()
	{
		choiceCanvas.transform.localScale = Vector2.one;

		BattleRewards.Instance.Trinkets.ForEach(trinket =>
		{
			GameObject trinketChoiceObj = Instantiate(trinketChoicePrefab, Vector2.zero, Quaternion.identity);
			trinketChoiceObj.transform.SetParent(choiceParent.transform);
			trinketChoiceObj.transform.localScale = Vector2.one;
			trinketChoiceObj.SetActive(true);

			TrinketChoiceDisplay trinketChoice = trinketChoiceObj.GetComponent<TrinketChoiceDisplay>();
			trinketChoice.SetupDisplay(trinket, gameObject);
		});
	}

	private void LevelupSetup(Card oldCard)
	{
		levelupCanvas.transform.localScale = Vector3.one;

		GameObject cardDisplay = Instantiate(CardPrefab, new Vector3(transform.position.x, transform.position.y, 10f), Quaternion.identity);
		cardDisplay.transform.SetParent(levelupPlaceholder.transform);
		cardDisplay.transform.localScale = new Vector3(0.75f, 0.75f, 1f);

		List<Card> upgrades = new List<Card>();
		for (int i = 0; i < 2; i++)
		{

			Random.InitState(System.DateTime.Now.Millisecond);
			int index = Random.Range(0, oldCard.Evolutions.Count - 1);

			while (upgrades.Contains(oldCard.Evolutions[index]))
			{
				Random.InitState(System.DateTime.Now.Millisecond);
				index = Random.Range(0, oldCard.Evolutions.Count - 1);
			}

			upgrades.Add(oldCard.Evolutions[index]);
		}

		upgrades.ForEach(up =>
		{
			GameObject upgradeChoiceObj = Instantiate(levelupChoicePrefab, Vector3.zero, Quaternion.identity);
			upgradeChoiceObj.transform.SetParent(levelupParent.transform);
			upgradeChoiceObj.transform.localScale = Vector3.one;
			upgradeChoiceObj.SetActive(true);

			LevelUpChoiceDisplay upgradeChoice = upgradeChoiceObj.GetComponent<LevelUpChoiceDisplay>();
			upgradeChoice.SetupDisplay(oldCard, up, gameObject);
		});
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!hoverign)
		{
			hoverign = true;

			LeanTween.value(gameObject, 0.1f, 1f, 0.5f).setEaseLinear().setOnUpdate((value) =>
			{
				Color imgLerp = Color.Lerp(imgIdleColor, imgHoveringColor, value);
				Color textLerp = Color.Lerp(txtIdleColor, txtHoveringColor, value);

				typeSlot.color = imgLerp;
				typeIcon.color = imgLerp;
				textBackground.color = imgLerp;

				title.color = textLerp;
				description.color = textLerp;
			});
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (hoverign)
		{
			hoverign = false;

			LeanTween.value(gameObject, 0.1f, 1f, 0.5f).setEaseLinear().setOnUpdate((value) =>
			{
				Color lerp = Color.Lerp(imgHoveringColor, imgIdleColor, value);

				typeSlot.color = lerp;
				typeIcon.color = lerp;
				textBackground.color = lerp;
				title.color = lerp;
				description.color = lerp;
			});
		}
	}
}
