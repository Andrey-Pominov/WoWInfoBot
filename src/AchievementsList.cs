namespace WowInfoBot;

public class AchievementsList
{
    private readonly Dictionary<int, AchievementItem> _listAchievementItems; // список достижений 
    public AchievementsList()
    {
        _listAchievementItems = new Dictionary<int, AchievementItem>();
    }
    public void Add(long id, string name, LookupType type)
    {
        switch (type)
        {
            case LookupType.Pve:
                AddAchievement(id, name, Globals.AchievementsPve);
                break;
            case LookupType.Pvp:
               AddAchievement(id, name, Globals.AchievementsPvp);
                break;
            default:
                throw new Exception("Invalid type specified!");
        }
    }

    private void AddAchievement(long id, string name, Dictionary<long, AchievementItem> achievementType)
    {
        if (achievementType[id].Group == -1)
        {
            _listAchievementItems.Add((int)id * -1, new AchievementItem(achievementType[id].Group, achievementType[id].Value, name));
            return;
        }
        if (_listAchievementItems.ContainsKey(achievementType[id].Group))
        {
            if (achievementType[id].Value > _listAchievementItems[achievementType[id].Group].Value)
            {
                            
                _listAchievementItems.Remove(achievementType[id].Group);
                _listAchievementItems.Add(achievementType[id].Group, new AchievementItem(achievementType[id].Group, achievementType[id].Value, name));
            }
        }
        else
        {
            _listAchievementItems.Add(achievementType[id].Group, new AchievementItem(achievementType[id].Group, achievementType[id].Value, name));
        }
    }
    public override string ToString()
    {
        var output = _listAchievementItems.Aggregate("", (current, entry) => current + $"• {entry.Value.Name}\n");
        if (output.Length == 0) return "None";
        return output;
    }
}