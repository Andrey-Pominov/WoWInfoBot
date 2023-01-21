namespace WowInfoBot;

public class AchievementItem 
{
    public int Group { get; private set; }  // группа достижений одной значимости(например одного сезона)
    public int Value { get; private set; }  // значение достижения, чем выше значение, тем лучше достижение
    public string Name { get; private set; } // название достижения 
    public AchievementItem(int group, int value, string name = null)
    {
        Group = group;
        Value = value;
        Name = name;
    }
}