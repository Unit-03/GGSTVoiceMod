using System.Collections.Generic;
using System.Drawing;

namespace GGSTVoiceMod
{
    public static class Constants
    {
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
            public Color Colour;

            public LanguageInfo(string id, string fullName, Color colour)
            {
                ID = id;
                FullName = fullName;
                Colour = colour;
            }
        }

        public static readonly Dictionary<string, LanguageInfo> Languages = new Dictionary<string, LanguageInfo>() {
            { "ENG", new LanguageInfo("ENG", "English",  Color.FromArgb(255, 128, 128)) },
            { "JPN", new LanguageInfo("JPN", "Japanese", Color.FromArgb(128, 255, 128)) },
            { "KOR", new LanguageInfo("KOR", "Korean",   Color.FromArgb(128, 128, 255)) }
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