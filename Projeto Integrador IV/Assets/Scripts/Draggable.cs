using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum DropType
{
	All,
	Hand,
	Table
}

public class Draggable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler //IPointerEnterHandler, IPointerExitHandler
{

	public DropType dropType = DropType.All;

	public new Camera camera;
	public Transform originalParent = null;

	CanvasGroup canvasGroup;

	GameObject placeholder = null;

	public void blockRaycast()
	{
		canvasGroup.blocksRaycasts = true;
	}


	private void Awake()
	{
		canvasGroup = GetComponent<CanvasGroup>();
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		Rect rect = GetComponent<RectTransform>().rect;

		placeholder = new GameObject();
		placeholder.name = "Drag Placeholder";
		placeholder.transform.SetParent(transform.parent);

		LayoutElement le = placeholder.AddComponent<LayoutElement>();
		le.preferredWidth = rect.width;
		le.preferredHeight = rect.height;
		le.flexibleWidth = 0;
		le.flexibleHeight = 0;

		placeholder.transform.SetSiblingIndex(transform.GetSiblingIndex());

		originalParent = transform.parent;
		transform.SetParent(transform.parent.parent);

		canvasGroup.blocksRaycasts = false;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, 10f);
		transform.SetParent(originalParent);
		transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());

		canvasGroup.blocksRaycasts = true;

		Destroy(placeholder);
	}

	public void OnDrag(PointerEventData eventData)
	{
		for (int i = 0; i < originalParent.childCount; i++)
		{
			if (transform.position.x < originalParent.GetChild(i).position.x)
			{
				placeholder.transform.SetSiblingIndex(i);
				break;
			}
		}

		var screenPoint = new Vector3(eventData.position.x, eventData.position.y, 0);

		float x = camera.ScreenToWorldPoint(screenPoint).x;
		float y = camera.ScreenToWorldPoint(screenPoint).y;

		transform.position = new Vector3(x, y, 9f);

		//LeanTween.scale(gameObject, new Vector3(0.5f, 0.5f, 0.5f), 0.25f);
	}

	public void OnDrop(PointerEventData eventData)
	{

	}

	public void DestroyMe()
	{
		Destroy(placeholder);
		Destroy(gameObject);
	}
}
