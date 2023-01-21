using System.Text.Json.Serialization;

namespace WowInfoBot;

public class BlizzardAccessToken
{
    [JsonInclude]
    [JsonPropertyName("access_token")]
    public string AccessToken { get; private set; }
    
    [JsonInclude]
    [JsonPropertyName("token_type")]
    public string TokenType { get; private set; }
    private int _expiresIn;
    
    [JsonInclude]
    [JsonPropertyName("expires_in")]
    public int ExpiresIn 
    {
        get => _expiresIn;
        private set
        {
            _expiresIn = value;
            var expTime = new TimeSpan(0, 0, value);
            ExpireDate = DateTime.Now.Add(expTime);
        }
    }
    public DateTime ExpireDate { get; private set; }
    
    [JsonInclude]
    [JsonPropertyName("scope")]
    public string Scope { get; private set; }
}