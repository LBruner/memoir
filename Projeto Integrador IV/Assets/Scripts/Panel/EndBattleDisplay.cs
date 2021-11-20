using UnityEngine;
using UnityEngine.UI;

public class EndBattleDisplay : MonoBehaviour
{
	[SerializeField] Image stateImage;
	[SerializeField] Text stateText;
	[SerializeField] Button continueButton;

	private void Awake()
	{

		gameObject.SetActive(false);
	}

	public void HandleEndBattleDisplay(BattleState battleState)
	{
		gameObject.SetActive(true);

		LeanTween.textAlpha(stateText.GetComponent<RectTransform>(), 0f, 0f);
		LeanTween.textAlpha(stateText.GetComponent<RectTransform>(), 1f, 1f);

		//LeanTween.alpha(stateImage.gameObject, 0f, 0f);
		//LeanTween.alpha(stateImage.gameObject, 1f, 1f);

		LeanTween.alpha(continueButton.gameObject, 0f, 0f);
		LeanTween.alpha(continueButton.gameObject, 1f, 1f);

		stateText.text = (battleState == BattleState.DEFEAT) ? "Defeat" : "Victory";
	}
}
