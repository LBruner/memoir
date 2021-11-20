using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardEffect : ScriptableObject
{
    public bool Temp;
    public int Duration;

    public abstract void ExecuteEffect(Card card, Enemy enemy);

    public abstract void ExecuteEffect(Card card, Player player);

    public abstract string GetEffectDescription();

    public abstract void RemoveEffect(Player player);

    public abstract void RemoveEffect(Enemy enemy);
}
