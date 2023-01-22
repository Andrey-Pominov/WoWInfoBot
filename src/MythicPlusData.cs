namespace WowInfoBot;

public class MythicPlusData
{
    public long Rating { get; private set; } 
    public int HighestRun { get; private set; } 
    public MythicPlusData()
    {
        Rating = 0;
        HighestRun = 0;
    }

    public void Parse(MPlusSummaryJson summary, MPlusSeasonInfoJson season)
    {
        Rating = (long)Math.Round(summary.CurrentMythicRating.Rating);
        foreach (var run in season.BestRuns)
        {
            if (run.KeystoneLevel > HighestRun)
            {
                HighestRun = (int)run.KeystoneLevel;
            }
        }
    }

    public override string ToString() 
    {
        return $" Рейтинг: {Rating}\n  Самый высокий пройденный ключ: +{HighestRun}";
    }
}