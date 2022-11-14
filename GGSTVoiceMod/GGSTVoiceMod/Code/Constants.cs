using System.Collections.Generic;
using System.Drawing;

namespace GGSTVoiceMod
{
    public static class Constants
    {
        #region Containers

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

        #endregion

        #region Voice

        public static readonly string[] VOICE_LANG_IDS = new string[] {
            "ENG", // English
            "JPN", // Japanese
            "KOR"  // Korean
        };

        public static readonly Dictionary<string, LanguageInfo> VoiceLanguages = new Dictionary<string, LanguageInfo>() {
            { "ENG", new LanguageInfo("ENG", "English",  Color.FromArgb(255, 192, 192)) },
            { "JPN", new LanguageInfo("JPN", "Japanese", Color.FromArgb(192, 255, 192)) },
            { "KOR", new LanguageInfo("KOR", "Korean",   Color.FromArgb(192, 128, 255)) }
        };

        public static readonly string[] VOICE_CHAR_IDS = new string[] {
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

        public static readonly Dictionary<string, CharacterInfo> VoiceCharacters = new Dictionary<string, CharacterInfo>() {
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

        #region Narration

        public static readonly string[] NARR_LANG_IDS = new string[] {
            "ENG", // English
            "JPN"  // Japanese
        };

        public static Dictionary<string, LanguageInfo> NarrLanguages = new Dictionary<string, LanguageInfo>() {
            { "ENG", new LanguageInfo("ENG", "English",  Color.FromArgb(255, 255, 255)) },
            { "JPN", new LanguageInfo("JPN", "Japanese", Color.FromArgb(255, 255, 255)) }
        };

        public static readonly Dictionary<string, string[]> NARR_CHAR_IDS = new Dictionary<string, string[]> {
            // English
            { "ENG", new string[] { "00", 
                                    "AXL",
                                    "BED",
                                    "CHP",
                                    "ELP",
                                    "FAU",
                                    "INO",
                                    "KYK",
                                    "LEO",
                                    "MAY",
                                    "MLL",
                                    "POT",
                                    "RAM",
                                    "SIN",
                                    "SLY",
                                    "SOL",
                                    "VEN",
                                    "ZAT",
            }},
            // Japanese
            { "JPN", new string[] { "00",      
                                    "ANS",     
                                    "AXL",     
                                    "BED",     
                                    "BKN",     
                                    "CHP",     
                                    "DZY",     
                                    "ELP",     
                                    "FAU",     
                                    "INO",     
                                    "JAM",     
                                    "JHN",     
                                    "JKO_Adult",
                                    "JKO_Child",
                                    "KUM",     
                                    "KYK",     
                                    "LEO",     
                                    "MAY",     
                                    "MLL",     
                                    "POT",     
                                    "RAM",     
                                    "RVN",     
                                    "SIN",     
                                    "SLY",     
                                    "SOL",     
                                    "VEN",     
                                    "ZAT",     
            }}
        };

        public static Dictionary<string, CharacterInfo> NarrCharacters = new Dictionary<string, CharacterInfo>() {
            { "00",        new CharacterInfo("00",        "Default"                                     ) },
            { "ANS",       new CharacterInfo("ANS",       "Answer"                                      ) },
            { "AXL",       new CharacterInfo("AXL",       "Axl Low",                   "Axl"            ) },
            { "BED",       new CharacterInfo("BED",       "Bedman"                                      ) },
            { "BKN",       new CharacterInfo("BKN",       "Baiken"                                      ) },
            { "CHP",       new CharacterInfo("CHP",       "Chipp Zanuff",              "Chipp"          ) },
            { "DZY",       new CharacterInfo("DZY",       "Dizzy"                                       ) },
            { "ELP",       new CharacterInfo("ELP",       "Elphelt Valentine",         "Elphelt"        ) },
            { "FAU",       new CharacterInfo("FAU",       "Faust"                                       ) },
            { "INO",       new CharacterInfo("INO",       "I-No"                                        ) },
            { "JAM",       new CharacterInfo("JAM",       "Jam Kuradoberi",            "Jam"            ) },
            { "JHN",       new CharacterInfo("JHN",       "Johnny"                                      ) },
            { "JKO_Adult", new CharacterInfo("JKO_Adult", "Jack-O' Valentine (Adult)", "Jack-O' (Adult)") },
            { "JKO_Child", new CharacterInfo("JKO_Child", "Jack-O' Valentine (Child)", "Jack-O' (Child)") },
            { "KUM",       new CharacterInfo("KUM",       "Kum Haehyun",               "Haehyun"        ) },
            { "KYK",       new CharacterInfo("KYK",       "Ky Kiske",                  "Ky"             ) },
            { "LEO",       new CharacterInfo("LEO",       "Leo Whitefang",             "Leo"            ) },
            { "MAY",       new CharacterInfo("MAY",       "May"                                         ) },
            { "MLL",       new CharacterInfo("MLL",       "Millia Rage",               "Millia"         ) },
            { "POT",       new CharacterInfo("POT",       "Potemkin"                                    ) },
            { "RAM",       new CharacterInfo("RAM",       "Ramlethal Valentine",       "Ramlethal"      ) },
            { "RVN",       new CharacterInfo("RVN",       "Raven"                                       ) },
            { "SIN",       new CharacterInfo("SIN",       "Sin Kiske",                 "Sin"            ) },
            { "SLY",       new CharacterInfo("SLY",       "Slayer"                                      ) },
            { "SOL",       new CharacterInfo("SOL",       "Sol Badguy",                "Sol"            ) },
            { "VEN",       new CharacterInfo("VEN",       "Venom"                                       ) },
            { "ZAT",       new CharacterInfo("ZAT",       "Zato-ONE",                  "Zato"           ) },
        };

        #endregion
    }
}