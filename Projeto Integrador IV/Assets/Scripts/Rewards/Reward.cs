public enum RewardType
{
	Gold,
	Trinket,
	Card,
	LevelUp,
	Item,
}

[System.Serializable]
public class Reward
{
	public RewardType Type;
	public int Priority;

	public string Description;

	public Card PreviousCard;

	public Reward()
	{

	}
}
