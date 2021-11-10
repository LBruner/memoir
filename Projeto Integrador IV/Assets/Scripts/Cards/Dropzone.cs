using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dropzone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	public DropType dropType = DropType.All;

	public void OnPointerEnter(PointerEventData eventData)
	{

	}

	public void OnPointerExit(PointerEventData eventData)
	{

	}

	public virtual void OnDrop(PointerEventData eventData)
	{
		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

		if (d != null && (dropType == DropType.All || d.dropType == dropType || d.dropType == DropType.All))
		{
			d.originalParent = transform;
			d.dropType = dropType;

			LeanTween.scale(d.gameObject, new Vector3(0.75f, 0.75f, 0.75f), 0.25f);
		} else
		{
			Debug.Log("Draggable Drop: " + d.dropType.ToString() + " / " + dropType.ToString());
		}
	}
}
