namespace WowInfoBot;

public class Raid 
{
    public string Name { get; private set; } 
    private Instance _raidInfo; 
    private string _locale;
    public Raid(Instance raidInfo, string locale)
    {
        Name = raidInfo.InstanceInstance.Name;
        _raidInfo = raidInfo;
        _locale = locale;
    }
    public override string ToString()
    {
        return _raidInfo.Modes.Aggregate("", (current, mode) => current + $"{mode.Progress.CompletedCount}/{mode.Progress.TotalCount} {mode.Difficulty.Name}\n");
    }
}