using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrinketChoiceDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public EquippableItem Trinket;
	private GameObject rootDisplay;

	[SerializeField] GameObject parent;
	[SerializeField] GameObject frame;

	[SerializeField] Text title;

	[SerializeField] Button button;

	[SerializeField] Image trinketSlot;
	[SerializeField] Image trinketIcon;

	[SerializeField] Color imgIdleColor;
	[SerializeField] Color imgHoveringColor;

	[SerializeField] Color txtIdleColor;
	[SerializeField] Color txtHoveringColor;

	private bool hoverign;

	void OnValidate()
	{
		hoverign = false;

		Text[] texts = GetComponentsInChildren<Text>();
		title = texts[0];

		Image[] images = GetComponentsInChildren<Image>();
		trinketSlot = images[0];
		trinketIcon = images[1];

		button = GetComponent<Button>();
		button.onClick.AddListener(OnClick);

		//parent = transform.parent.gameObject;
	}

	void Start()
	{
		hoverign = false;

		Text[] texts = GetComponentsInChildren<Text>();
		title = texts[0];

		Image[] images = GetComponentsInChildren<Image>();
		trinketSlot = images[0];
		trinketIcon = images[1];

		button = GetComponent<Button>();
		button.onClick.AddListener(OnClick);

		frame = transform.parent.gameObject;
		parent = frame.transform.parent.gameObject;
	}

	public void SetupDisplay(EquippableItem trinket, GameObject _root)
	{
		Trinket = trinket;

		rootDisplay = _root;

		title.text = Trinket.ItemName;
		trinketIcon.sprite = Trinket.Icon;
	}

	public void OnClick()
	{
		Player.Instance.Equip((EquippableItem)Trinket.GetCopy());

		LeanTween.alpha(frame, 0f, 1f).setOnComplete(() =>
		{
			parent.transform.localScale = Vector3.zero;

			for (int i = 0; i < frame.transform.childCount; i++)
			{
				if (i != transform.GetSiblingIndex())
					Destroy(frame.transform.GetChild(i));
			}

			Destroy(rootDisplay);
			Destroy(this);
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

				trinketSlot.color = imgLerp;
				trinketIcon.color = imgLerp;

				title.color = textLerp;
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
				Color imgLerp = Color.Lerp(imgHoveringColor, imgIdleColor, value);
				Color textLerp = Color.Lerp(txtHoveringColor, txtIdleColor, value);

				trinketSlot.color = imgLerp;
				trinketIcon.color = imgLerp;

				title.color = textLerp;
			});
		}
	}
}
