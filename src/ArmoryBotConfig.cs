namespace WowInfoBot;

public class ArmoryBotConfig // настройки с appsetting.json
{
    public string DiscordToken { get; set; }
    public char CmdPrefix { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    private string _locale;
    
    //TODO: вынести region в appsettings
    public string Locale
    {
        get => _locale;
        set
        {
            _locale = value.ToLower();
            switch (value.ToLower())
            {
                case "en_us":
                    Region = "us";
                    break;
                case "es_mx":
                    Region = "us";
                    break;
                case "pt_br":
                    Region = "us";
                    break;
                case "de_de":
                    Region = "eu";
                    break;
                case "en_gb":
                    Region = "eu";
                    break;
                case "es_es":
                    Region = "eu";
                    break;
                case "fr_fr":
                    Region = "eu";
                    break;
                case "it_it":
                    Region = "eu";
                    break;
                case "ru_ru":
                    Region = "eu";
                    break;
                default:
                    throw new Exception("Invalid locale specified in appsettings.json");
            } 
        } 
    } 
    public string Region { get; set; } 
}