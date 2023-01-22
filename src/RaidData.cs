namespace WowInfoBot;

public class RaidData 
{
    public List<Raid> Raids { get; private set; }   
    private readonly string _locale;                
    public RaidData(string locale)
    {
        Raids = new List<Raid>();
        _locale = locale;
    }
    public void Add(Instance raid)
    {
        Raids.Add(new Raid(raid, _locale));
    }
}