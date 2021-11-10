using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RewardType
{
    Gold,
    Trinket,
    Card,
    LevelUp,
    Item,
}
public class Reward
{
    public RewardType Type;
    public int Priority;
}
