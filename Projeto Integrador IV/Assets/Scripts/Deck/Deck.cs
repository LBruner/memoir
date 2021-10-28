using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Deck")]
public class Deck : ScriptableObject
{

	public List<Card> Cards = new List<Card>();
}
