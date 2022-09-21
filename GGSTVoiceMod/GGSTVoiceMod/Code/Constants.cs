﻿using System.Collections.Generic;

namespace GGSTVoiceMod
{
    public static class Constants
    {
        #region Paths

        public const string ROOT_KEY = "${root}";
        public const string GAME_KEY = "${game}";
        public const string LANG_KEY = "${lang}";
        public const string CHAR_KEY = "${char}";

        public const string ASSET_DOWNLOAD_URL = "https://github.com/Unit-03/GGSTVoiceMod/raw/main/Assets/" + LANG_KEY + "/" + CHAR_KEY + ".zip";

        public const string UNREAL_PAK_PATH = ROOT_KEY + "/UnrealPak/UnrealPak.exe";
        public const string MODS_PATH       = GAME_KEY + "/RED/Content/Paks/~mods";

        #endregion

        #region Languages

        public static readonly string[] LANGUAGE_IDS = new string[] {
            "ENG", // English
            "JPN", // Japanese
            "KOR"  // Korean
        };

        // Handy little struct for storing some info about languages!
        public struct LanguageInfo
        {
            public string ID;
            public string FullName;

            public LanguageInfo(string id, string fullName)
            {
                ID = id;
                FullName = fullName;
            }
        }

        public static readonly Dictionary<string, LanguageInfo> Languages = new Dictionary<string, LanguageInfo>() {
            { "ENG", new LanguageInfo("ENG", "English" ) },
            { "JPN", new LanguageInfo("JPN", "Japanese") },
            { "KOR", new LanguageInfo("KOR", "Korean"  ) }
        };

        #endregion

        #region Characters

        public static readonly string[] CHARACTER_IDS = new string[] {
            "ANJ", // Anji Mito
            "AXL", // Axl Low
            "BGT", // Bridget
            "BKN", // Baiken
            "CHP", // Chipp Zanuff
            "COS", // Happy Chaos
            "FAU", // Faust
            "GIO", // Giovanna
            "GLD", // Goldlewis Dickinson
            "INO", // I-No
            "JKO", // Jack-O' Valentine
            "KYK", // Ky Kiske
            "LEO", // Leo Whitefang
            "MAY", // May
            "MLL", // Millia Rage
            "NAG", // Nagoriyuki
            "POT", // Potemkin
            "RAM", // Ramlethal Valentine
            "SOL", // Sol Badguy
            "TST", // Testament
            "ZAT"  // Zato-ONE
        };

        // Another handy little struct but for storing some info about characters!
        public struct CharacterInfo
        {
            public string ID;
            public string FullName;
            public string ShortName;

            public CharacterInfo(string id, string fullName, string shortName = null)
            {
                ID = id;
                FullName = fullName;
                ShortName = shortName ?? fullName;
            }
        }

        public static readonly Dictionary<string, CharacterInfo> Characters = new Dictionary<string, CharacterInfo>() {
            { "ANJ", new CharacterInfo("ANJ", "Anji Mito",           "Anji"     ) },
            { "AXL", new CharacterInfo("AXL", "Axl Low",             "Axl"      ) },
            { "BGT", new CharacterInfo("BGT", "Bridget"                         ) },
            { "BKN", new CharacterInfo("BKN", "Baiken"                          ) },
            { "CHP", new CharacterInfo("CHP", "Chipp Zanuff",        "Chipp"    ) },
            { "COS", new CharacterInfo("COS", "Happy Chaos",         "Chaos"    ) },
            { "FAU", new CharacterInfo("FAU", "Faust"                           ) },
            { "GIO", new CharacterInfo("GIO", "Giovanna"                        ) },
            { "GLD", new CharacterInfo("GLD", "Goldlewis Dickinson", "Goldlewis") },
            { "INO", new CharacterInfo("INO", "I-No"                            ) },
            { "JKO", new CharacterInfo("JKO", "Jack-O' Valentine",   "Jack-O'"  ) },
            { "KYK", new CharacterInfo("KYK", "Ky Kiske",            "Ky"       ) },
            { "LEO", new CharacterInfo("LEO", "Leo Whitefang",       "Leo"      ) },
            { "MAY", new CharacterInfo("MAY", "May"                             ) },
            { "MLL", new CharacterInfo("MLL", "Millia Rage",         "Millia"   ) },
            { "NAG", new CharacterInfo("NAG", "Nagoriyuki",          "Nago"     ) },
            { "POT", new CharacterInfo("POT", "Potemkin"                        ) },
            { "RAM", new CharacterInfo("RAM", "Ramlethal Valentine", "Ramlethal") },
            { "SOL", new CharacterInfo("SOL", "Sol Badguy",          "Sol"      ) },
            { "TST", new CharacterInfo("TST", "Testament"                       ) },
            { "ZAT", new CharacterInfo("ZAT", "Zato-ONE",            "Zato"     ) }
        };

        #endregion
    }
}