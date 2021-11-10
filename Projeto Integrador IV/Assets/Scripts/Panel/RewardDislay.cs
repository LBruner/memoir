using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RewardDislay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] RewardType type;
	[SerializeField] Sprite[] typeSprites;

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
	}

	// Start is called before the first frame update
	void Awake()
	{
		hoverign = false;

		Text[] texts = GetComponentsInChildren<Text>();
		title = texts[0];
		description = texts[1];

		Image[] images = GetComponentsInChildren<Image>();
		typeSlot = images[1];
		typeIcon = images[2];
		textBackground = images[3];
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void SetupDisplay(RewardType _type, string title, string description)
	{
		type = _type;

		switch (type) {
			case RewardType.Card:
				typeIcon.sprite = typeSprites[(int) RewardType.Card];
				break;
			case RewardType.Gold:
				typeIcon.sprite = typeSprites[(int)RewardType.Gold];
				break;
			case RewardType.LevelUp:
				typeIcon.sprite = typeSprites[(int)RewardType.LevelUp];
				break;
			case RewardType.Trinket:
				typeIcon.sprite = typeSprites[(int)RewardType.Trinket];
				break;
			case RewardType.Item:
				typeIcon.sprite = typeSprites[(int)RewardType.Item];
				break;
		}
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
