namespace WowInfoBot
{
    public enum Id : long
    {
        AdditionShadowlands = 499,
        AdditionDragonflight = 503,
    }

    public enum Namespace
    {
        Profile,
        Static,
        Dynamic
    }

    public enum LookupType
    {
        Pve,
        Pvp
    }

    internal static class Globals
    {
        public const string QuestionMarkIconUrl =
            @"https://render-us.worldofwarcraft.com/icons/56/inv_misc_questionmark.jpg";

        public const string TomeIconUrl = @"https://render-us.worldofwarcraft.com/icons/56/inv_misc_book_07.jpg";
        public const string WowTokenIconUrl = @"https://render-us.worldofwarcraft.com/icons/56/wow_token01.jpg";

        // Shadowlands
        // public static readonly Dictionary<long, AchievementItem> AchievementsPVE = new()
        //     {
        //         // Raid Overall
        //         { 14460, new AchievementItem(10, 0) }, // Ahead of the Curve: Sire Denathrius
        //         { 14461, new AchievementItem(10, 1) }, // Cutting Edge: Sire Denathrius
        //
        //         { 15134, new AchievementItem(11, 0) }, // Ahead of the Curve: Sylvanas Windrunner
        //         { 15135, new AchievementItem(11, 1) }, // Cutting Edge: Sylvanas Windrunner
        //
        //         { 15470, new AchievementItem(12, 0) }, // Ahead of the Curve: The Jailer
        //         { 15471, new AchievementItem(12, 1) }, // Cutting Edge: The Jailer
        //
        //         // Keystone Hero
        //         { 15051, new AchievementItem(-1, 0) }, // Keystone Hero: De Other Side
        //         { 15048, new AchievementItem(-1, 0) }, // Keystone Hero: Halls of Atonement
        //         { 15047, new AchievementItem(-1, 0) }, // Keystone Hero: Mists of Tirna Scithe
        //         { 15046, new AchievementItem(-1, 0) }, // Keystone Hero: Plaguefall
        //         { 15052, new AchievementItem(-1, 0) }, // Keystone Hero: Sanguine Depths
        //         { 15049, new AchievementItem(-1, 0) }, // Keystone Hero: Spires of Ascension
        //         { 15045, new AchievementItem(-1, 0) }, // Keystone Hero: The Necrotic Wake
        //         { 15050, new AchievementItem(-1, 0) }, // Keystone Hero: Theater of Pain
        //
        //
        //         // Season 1
        //         { 14938, new AchievementItem(100, 0) }, // Shadowlands Keystone Explorer: Season One
        //         { 14531, new AchievementItem(100, 1) }, // Shadowlands Keystone Conqueror: Season One
        //         { 14532, new AchievementItem(100, 2) }, // Shadowlands Keystone Master: Season One
        //         // Season 2
        //         { 15073, new AchievementItem(101, 0) }, // Shadowlands Keystone Explorer: Season Two
        //         { 15077, new AchievementItem(101, 1) }, // Shadowlands Keystone Conqueror: Season Two
        //         { 15078, new AchievementItem(101, 2) }, // Shadowlands Keystone Master: Season Two
        //         //Season 3 
        //         { 15496, new AchievementItem(103, 0) }, // Shadowlands Keystone Explorer: Season Three
        //         { 15498, new AchievementItem(103, 1) }, // Shadowlands Keystone Conqueror: Season Three
        //         { 15499, new AchievementItem(103, 2) }, // Shadowlands Keystone Master: Season Three
        //         //Season 4 
        //         { 15688, new AchievementItem(104, 0) }, // Shadowlands Keystone Explorer: Season Four
        //         { 15689, new AchievementItem(104, 1) }, // Shadowlands Keystone Conqueror: Season Four
        //         { 15690, new AchievementItem(104, 2) }, // Shadowlands Keystone Master: Season Four
        //     };

        // Dragonflight
        public static readonly Dictionary<long, AchievementItem> AchievementsPve = new()
        {
            // Raid Overall 
            { 17107, new AchievementItem(20, 0) }, // Ahead of the Curve: Raszageth the Storm-Eater
            { 17108, new AchievementItem(20, 1) }, // Cutting Edge: Raszageth the Storm-Eater

            // Keystone Hero
            { 16645, new AchievementItem(-1, 0) }, // Keystone Hero: The Azure Vault
            { 16641, new AchievementItem(-1, 0) }, // Keystone Hero: The Nokhud Offensive
            { 16640, new AchievementItem(-1, 0) }, // Keystone Hero: Ruby Life Pools
            { 16643, new AchievementItem(-1, 0) }, // Keystone Hero: Algeth'ar Academy
            { 16659, new AchievementItem(-1, 0) }, // Keystone Hero: Halls of Valor
            { 16658, new AchievementItem(-1, 0) }, // Keystone Hero: Court of Stars
            { 16660, new AchievementItem(-1, 0) }, // Keystone Hero: Shadowmoon Burial Grounds
            { 16661, new AchievementItem(-1, 0) }, // Keystone Hero: Temple of the Jade Serpent

            // Season 1
            { 16647, new AchievementItem(200, 0) }, // Dragonflight Keystone Explorer: Season One
            { 16648, new AchievementItem(200, 1) }, // Dragonflight Keystone Conqueror: Season One
            { 16649, new AchievementItem(200, 2) }, // Dragonflight Keystone Master: Season One
            { 16650, new AchievementItem(200, 3) }, // Dragonflight Keystone Hero: Season One
        };

        public static readonly Dictionary<long, AchievementItem> AchievementsPvp = new()
        {
            // PVP Lifetime
            { 2090, new AchievementItem(10, 0, "Претендент") }, // Challenger
            { 2093, new AchievementItem(10, 1, "Фаворит") }, // Rival
            { 2092, new AchievementItem(10, 2, "Дуэлянт") }, // Duelist
            { 2091, new AchievementItem(10, 3, "Гладиатор") }, // Gladiator
            // RBG Lifetime
            // Horde
            { 6941, new AchievementItem(20, 5) }, // Hero of the Horde (top .5%)
            { 5356, new AchievementItem(20, 4) }, // High Warlord (2400)
            { 5355, new AchievementItem(20, 3) }, // General (2200)
            { 5353, new AchievementItem(20, 2) }, // Champion (2000)
            { 5352, new AchievementItem(20, 1) }, // Legionnaire (1800)
            { 5349, new AchievementItem(20, 0) }, // First Sergeant (1500)
            // Alliance
            { 6942, new AchievementItem(21, 5) }, // Hero of the Alliance (top .5%)
            { 5343, new AchievementItem(21, 4) }, // Grand Marshal (2400)
            { 5341, new AchievementItem(21, 3) }, // Marshal (2200)
            { 5339, new AchievementItem(21, 2) }, // Lieutenant Commander (2000)
            { 5337, new AchievementItem(21, 1) }, // Knight-Captain (1800)
            { 5334, new AchievementItem(21, 0) }, // Sergeant Major (1500)
            //  PVP Season 1
            { 14686, new AchievementItem(100, 0) }, // Challenger: Shadowlands Season 1
            { 14687, new AchievementItem(100, 1) }, // Rival: Shadowlands Season 1
            { 14688, new AchievementItem(100, 2) }, // Duelist: Shadowlands Season 1
            { 14691, new AchievementItem(100, 3) }, // Elite: Shadowlands Season 1
            { 14689, new AchievementItem(100, 4) }, // Gladiator: Shadowlands Season 1
            { 14690, new AchievementItem(100, 5) }, // Sinful Gladiator: Shadowlands Season 1
            //  PVP Season 2
            { 14969, new AchievementItem(101, 0) }, // Challenger: Shadowlands Season 2
            { 14970, new AchievementItem(101, 1) }, // Rival: Shadowlands Season 2
            { 14971, new AchievementItem(101, 2) }, // Duelist: Shadowlands Season 2
            { 14974, new AchievementItem(101, 3) }, // Elite: Shadowlands Season 2
            { 14972, new AchievementItem(101, 4) }, // Gladiator: Shadowlands Season 2
            { 14973, new AchievementItem(101, 5) }, // Unchained Gladiator: Shadowlands Season 2
            // PVP Season 3 
            { 15348, new AchievementItem(102, 0) }, // Combatant I: Shadowlands Season 3
            { 15380, new AchievementItem(102, 1) }, // Combatant II: Shadowlands Season 3
            { 15349, new AchievementItem(102, 2) }, // Challenger I: Shadowlands Season 3
            { 15379, new AchievementItem(102, 3) }, // Challenger II: Shadowlands Season 3
            { 15350, new AchievementItem(102, 4) }, // Rival I: Shadowlands Season 3
            { 15378, new AchievementItem(102, 5) }, // Rival II: Shadowlands Season 3
            { 15351, new AchievementItem(102, 6) }, // Duelist: Shadowlands Season 3
            { 15354, new AchievementItem(102, 7) }, // Elite: Shadowlands Season 3
            { 15352, new AchievementItem(102, 8) }, // Gladiator: Shadowlands Season 3
            { 15353, new AchievementItem(102, 9) }, // Cosmic Gladiator: Shadowlands Season 3
            // PVP Season 4 
            { 15609, new AchievementItem(103, 0) }, // Combatant I: Shadowlands Season 4
            { 15610, new AchievementItem(103, 1) }, // Combatant II: Shadowlands Season 4
            { 15600, new AchievementItem(103, 2) }, // Challenger I: Shadowlands Season 4
            { 15601, new AchievementItem(103, 3) }, // Challenger II: Shadowlands Season 4
            { 15602, new AchievementItem(103, 4) }, // Rival I: Shadowlands Season 4
            { 15603, new AchievementItem(103, 5) }, // Rival II: Shadowlands Season 4
            { 15604, new AchievementItem(103, 6) }, // Duelist: Shadowlands Season 4
            { 15639, new AchievementItem(103, 7) }, // Elite: Shadowlands Season 4
            { 15605, new AchievementItem(103, 8) }, // Gladiator: Shadowlands Season 4
            { 15606, new AchievementItem(103, 9) }, // Eternal Gladiator: Shadowlands Season 4
            // PVP Season 1 
            { 15960, new AchievementItem(200, 0) }, // Combatant I: Dragonflight Season 1
            { 15961, new AchievementItem(200, 1) }, // Combatant II: Dragonflight Season 1
            { 15955, new AchievementItem(200, 2) }, // Challenger I: Dragonflight Season 1
            { 15956, new AchievementItem(200, 3) }, // Challenger II: Dragonflight Season 1
            { 15952, new AchievementItem(200, 4) }, // Rival I: Dragonflight Season 1
            { 15953, new AchievementItem(200, 5) }, // Rival II: Dragonflight Season 1
            { 15954, new AchievementItem(200, 6) }, // Duelist: Dragonflight Season 1
            { 15984, new AchievementItem(200, 7) }, // Elite: Dragonflight Season 1
            { 15957, new AchievementItem(200, 8) }, // Gladiator: Dragonflight Season 1
            { 15951, new AchievementItem(200, 9) }, // Crimson Gladiator: Dragonflight Season 1
        };
    }
}