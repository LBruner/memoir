using UnityEngine;
using UnityEngine.EventSystems;

public class Playzone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] DropType dropType = DropType.All;
	[SerializeField] Enemy enemy;
	[SerializeField] Player player;
	[SerializeField] TurnSystem turnSystem;

	Draggable carDropped = null;

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
			carDropped = d;
			Card card = carDropped.gameObject.GetComponent<CardDisplay>().card;
			if ((card.Cost - (int)player.CostModifier.Value) > player.CurrentEnergy)
			{
				//d.transform.SetParent(d.originalParent);
				//LeanTween.scale(d.gameObject, new Vector3(0.75f, 0.75f, 0.75f), 0.5f);
				return;
			}

			player.CurrentEnergy -= (card.Cost - (int)player.CostModifier.Value);
			turnSystem.UpdateEnergyDisplay();

			d.originalParent = transform;
			d.blockRaycast();

			LeanTween.scale(d.gameObject, new Vector3(1f, 1f, 1f), 1f).setOnComplete(ExecuteCardEffect);
		}
	}

	void ExecuteCardEffect()
	{
		if (turnSystem.GetTurn() == Turn.Player)
		{
			Card card = carDropped.gameObject.GetComponent<CardDisplay>().card;

			if (card != null)
				Debug.Log("Playing: " + card.Name);

			if (card.Targetable)
			{
				Debug.Log("Enemy: " + enemy.Level);
				card.ExecuteEffect(enemy);
				Destroy(carDropped.gameObject);
				return;
			}

			card.ExecuteEffect(player);
			Destroy(carDropped.gameObject);
			return;
		}
	}
}
