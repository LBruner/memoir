using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardDisplay : MonoBehaviour
{
	public Card card;

	public Text nameText;
	public Text descriptionText;
	//public Text typeText;
	public Text costText;
	public Text minDamageText;
	public Text maxDamageText;

	public Image artworkImage;

	public Renderer FrameRenderer;

	public Material[] materials;

	private void OnValidate()
	{

		for(int i = 0; i < materials.Length; i++)
		{
			FrameRenderer.sharedMaterials[i] = materials[i];
		}
		
		UpdateDisplay();
	}

	public void Awake()
	{
		for (int i = 0; i < materials.Length; i++)
		{
			FrameRenderer.sharedMaterials[i] = materials[i];
		}
	}

	void Start()
	{
		//Debug.Log(Camera.main.ScreenToWorldPoint(transform.position));
		UpdateDisplay();
	}

	void Update()
	{
		
	}

	public void UpdateDisplay()
	{
		if (card != null)
		{
			nameText.text = card.Name;
			descriptionText.text = card.GetCardDescription();
			//typeText.text = card.getCardTypeText();

			artworkImage.sprite = card.Artwork;

			costText.text = card.Cost.ToString();

			if (card is AttackCard)
			{
				AttackCard aux = (AttackCard)card;

				minDamageText.text = aux.MinDamage.ToString();
				maxDamageText.text = aux.MaxDamage.ToString();
			}

			if (FrameRenderer.sharedMaterials[0] != null)
			{
				FrameRenderer.sharedMaterials[0].color = card.getCardFrameColor();
			}
		}
	}
}
