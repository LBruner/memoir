using System;

[Serializable]
public enum StatModType
{
	Flat = 100,
	PercentAdd = 200,
	PercentMult = 300,
}
[Serializable]
public class StatModifier
{
	public float Value;
	public StatModType Type;
	public bool BattleOnly;
	public int Order;
	public object Source;

	public StatModifier(float value, StatModType type, bool battleOnly, int order, object source)
	{
		Value = value;
		Type = type;
		BattleOnly = battleOnly;
		Order = order;
		Source = source;
	}

	public StatModifier(float value, StatModType type, bool battleOnly) : this(value, type, battleOnly, (int)type, null) { }

	public StatModifier(float value, StatModType type, bool battleOnly, int order) : this(value, type, battleOnly, order, null) { }

	public StatModifier(float value, StatModType type, bool battleOnly, object source) : this(value, type, battleOnly, (int)type, source) { }
}
