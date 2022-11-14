using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace GGSTVoiceMod
{
    public class ModManifest
    {
        #region Properties

        public string NarrationLanguage  { get; set; }
        public string NarrationCharacter { get; set; }
        public bool SilencedNarration { get; set; }

        public string[] Characters => voiceSettings.Keys.ToArray();

        public LanguageSettings this[string key] {
            get => voiceSettings[key];
            set => voiceSettings[key] = value;
        }

        public Dictionary<string, LanguageSettings> VoiceSettings {
            get {
                Dictionary<string, LanguageSettings> copy = new Dictionary<string, LanguageSettings>(voiceSettings.Count);

                foreach (var pair in voiceSettings)
                    copy.Add(pair.Key, pair.Value.Clone());

                return copy;
            }
        }

        #endregion

        #region Fields

        private Dictionary<string, LanguageSettings> voiceSettings;

        #endregion

        #region Constructor

        public ModManifest()
        {
            NarrationLanguage  = "DEF";
            NarrationCharacter = "";

            voiceSettings = new Dictionary<string, LanguageSettings>();

            foreach (string charID in Constants.VOICE_CHAR_IDS)
                voiceSettings.Add(charID, new LanguageSettings(charID));
        }

        public ModManifest(string narrLang, string narrChar, bool silenced, Dictionary<string, LanguageSettings> voices)
        {
            NarrationLanguage  = narrLang;
            NarrationCharacter = narrChar;
            SilencedNarration  = silenced;

            voiceSettings = new Dictionary<string, LanguageSettings>();

            foreach (var pair in voices)
                voiceSettings.Add(pair.Key, pair.Value.Clone());
        }

        public ModManifest(ModManifest clone) : this(clone.NarrationLanguage, clone.NarrationCharacter, clone.SilencedNarration, clone.voiceSettings)
        {
        }

        public ModManifest(string filePath)
        {
            Load(filePath);
        }

        #endregion

        #region Methods

        public void Load(string filePath)
        {
            Dictionary<string, LanguageSettings> manifest = new Dictionary<string, LanguageSettings>();

            NarrationLanguage  = "DEF";
            NarrationCharacter = "";

            foreach (string charId in Constants.VOICE_CHAR_IDS)
                manifest.Add(charId, new LanguageSettings(charId));

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                for (int i = 0; i < lines.Length; ++i)
                {
                    string[] parts = lines[i].Split('=');

                    if (parts.Length < 2)
                        continue;

                    string key = parts[0].Trim().ToUpper();

                    if (key == "NARR")
                    {
                        string[] values = parts[1].Split('_');
                        string langId = values[0].Trim().ToUpper();
                        string charId = values[1].Trim().ToUpper();
                        string silent = values[2].Trim().ToLower();

                        NarrationLanguage  = langId;
                        NarrationCharacter = charId;

                        if (bool.TryParse(silent, out bool silenced))
                            SilencedNarration = silenced;
                        else
                            SilencedNarration = true;
                    }
                    else
                    {
                        string[] keys = parts[0].Split('_');
                        string charId = keys[0].Trim().ToUpper();
                        string langId = keys[1].Trim().ToUpper();
                        string useId = parts[1].Trim().ToUpper();

                        if (manifest.ContainsKey(charId))
                        {
                            if (Constants.VOICE_LANG_IDS.Contains(langId) && Constants.VOICE_LANG_IDS.Contains(useId))
                                manifest[charId][langId] = useId;
                        }
                    }
                }
            }

            voiceSettings = manifest;
        }

        public void Save(string filePath)
        {
            string manifestDir = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(manifestDir))
                Directory.CreateDirectory(manifestDir);

            using StreamWriter writer = File.CreateText(filePath);

            foreach (string charId in voiceSettings.Keys)
            {
                foreach (string langId in Constants.VOICE_LANG_IDS)
                {
                    // No point writing languages that haven't been changed
                    if (langId == voiceSettings[charId][langId])
                        continue;

                    writer.WriteLine($"{charId}_{langId}={voiceSettings[charId][langId]}");
                }
            }

            if (NarrationLanguage != "DEF")
                writer.WriteLine($"NARR={NarrationLanguage}_{NarrationCharacter}_{SilencedNarration}");
        }

        #endregion
    }
}
