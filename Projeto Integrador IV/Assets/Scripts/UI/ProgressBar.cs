using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
	public int Maximum;
	public int Minimum;
	public int Current;

	public bool OnlyCurrent;
	public bool HideOnZero;

	public Color Color;
	[SerializeField] Image mask;
	[SerializeField] Image fill;
	[SerializeField] Text text;

	private void OnValidate()
	{
		GetCurrentFill();
	}

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		GetCurrentFill();
	}

	public void GetCurrentFill()
	{
		float currentOffset = Current - Minimum;
		float maximumOffset = Maximum - Minimum;
		float fillAmount = currentOffset / maximumOffset;
		mask.fillAmount = fillAmount;

		mask.color = Color;

		if (HideOnZero && Current == 0)
		{
			transform.localScale = new Vector3(0, 0, 0);
			return;
		}

		transform.localScale = new Vector3(1, 1, 1);

		if (OnlyCurrent)
		{
			text.text = Current.ToString();
			return;
		}

		text.text = Current.ToString() + "/" + Maximum.ToString();
	}
}
