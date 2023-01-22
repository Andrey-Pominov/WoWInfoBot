namespace WowInfoBot;

public class AchievementItem
{
    public int Group { get; private set; }
    public int Value { get; private set; }
    public string Name { get; private set; }

    public AchievementItem(int group, int value, string name = null)
    {
        Group = group;
        Value = value;
        Name = name;
    }
}