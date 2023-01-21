namespace WowInfoBot;

public class RaidData 
{
    public List<Raid> Raids { get; private set; }   // список всех рейдов
    private readonly string _locale;                // локализация( устанавливается в appsettings.json)
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