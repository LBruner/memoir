using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardEffect : ScriptableObject
{
    public bool IsTemp;
    public abstract void ExecuteEffect(Card card, Enemy enemy);

    public abstract void ExecuteEffect(Card card, Player player);

    public abstract string GetEffectDescription();
}
