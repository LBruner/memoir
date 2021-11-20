using UnityEngine;

public enum EquipmentType
{
	Sword,
	Shoulders,
	Legs,
	Trinket
}

[CreateAssetMenu(menuName = "Items/Equippable Item")]
public class EquippableItem : Item
{
	public int HealthBonus;
	public int EnergyBonus;
	public int DamageBonus;
	public int DamageReductionBonus;
	public int DrawBonus;
	public int CostBonus;

	[Space]
	public float HealthPercentBonus;
	public float DamagePercentBonus;
	public float DamageReductionPercentBonus;

	[Space]
	public EquipmentType Type;

	public override Item GetCopy()
	{
		return Instantiate(this);
	}

	public override void Destroy()
	{
		Destroy(this);
	}

	public void Equip(Player c)
	{
		// TO-DO: Add bonuses

		if (HealthBonus != 0)
			c.HealthModifier.AddModifier(new StatModifier(HealthBonus, StatModType.Flat, false, this));

		if (EnergyBonus != 0)
			c.EnergyModifier.AddModifier(new StatModifier(EnergyBonus, StatModType.Flat, false, this));

		if (DamageBonus != 0)
			c.DamageModifier.AddModifier(new StatModifier(DamageBonus, StatModType.Flat, false, this));

		if (DamageReductionBonus != 0)
			c.DamageModifier.AddModifier(new StatModifier(DamageReductionBonus, StatModType.Flat, false, this));

		if (DrawBonus != 0)
			c.DrawModifier.AddModifier(new StatModifier(DrawBonus, StatModType.Flat, false, this));

		if (CostBonus != 0)
			c.CostModifier.AddModifier(new StatModifier(CostBonus, StatModType.Flat, false, this));

		//if (StrengthBonus != 0)
		//	c.Strength.AddModifier(new StatModifier(StrengthBonus, StatModType.Flat, this));
		//if (AgilityBonus != 0)
		//	c.Agility.AddModifier(new StatModifier(AgilityBonus, StatModType.Flat, this));
		//if (IntelligenceBonus != 0)
		//	c.Intelligence.AddModifier(new StatModifier(IntelligenceBonus, StatModType.Flat, this));
		//if (VitalityBonus != 0)
		//	c.Vitality.AddModifier(new StatModifier(VitalityBonus, StatModType.Flat, this));

		//if (StrengthPercentBonus != 0)
		//	c.Strength.AddModifier(new StatModifier(StrengthPercentBonus, StatModType.PercentMult, this));
		//if (AgilityPercentBonus != 0)
		//	c.Agility.AddModifier(new StatModifier(AgilityPercentBonus, StatModType.PercentMult, this));
		//if (IntelligencePercentBonus != 0)
		//	c.Intelligence.AddModifier(new StatModifier(IntelligencePercentBonus, StatModType.PercentMult, this));
		//if (VitalityPercentBonus != 0)
		//	c.Vitality.AddModifier(new StatModifier(VitalityPercentBonus, StatModType.PercentMult, this));
	}

	public override string GetItemType()
	{
		return Type.ToString();
	}

	public override string GetDescription()
	{
		// BUILD DESCRIPTION
		sb.Length = 0;

		//AddStat(StrengthBonus, "Strength");
		//AddStat(AgilityBonus, "Agility");
		//AddStat(IntelligenceBonus, "Intelligence");
		//AddStat(VitalityBonus, "Vitality");

		//AddStat(StrengthPercentBonus, "Strength", isPercent: true);
		//AddStat(AgilityPercentBonus, "Agility", isPercent: true);
		//AddStat(IntelligencePercentBonus, "Intelligence", isPercent: true);
		//AddStat(VitalityPercentBonus, "Vitality", isPercent: true);

		AddStat(DamageBonus, "Power");

		return sb.ToString();
	}

	private void AddStat(float value, string statName, bool isPercent = false)
	{
		if (value != 0)
		{
			if (sb.Length > 0)
				sb.AppendLine();

			if (value > 0)
				sb.Append(" ");
			if (isPercent) {
				sb.Append(value * 100);
				sb.Append("% ");
			} else {
				sb.Append(value);
				sb.Append(" ");
			}
			sb.Append(statName);
		}
	}
}
