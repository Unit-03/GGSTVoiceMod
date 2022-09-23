using System;
using System.Collections.Generic;

namespace GGSTVoiceMod
{
    public class LanguageSettings
    {
        #region Properties

        public string CharacterID { get; }

        public string this[string key] {
            get => languages[key];
            set {
                if (!languages.ContainsKey(key))
                    throw new KeyNotFoundException($"Index key '{key}' is not a valid language ID");

                if (!Constants.Languages.ContainsKey(value))
                    throw new ArgumentException($"Value '{value}' is not a valid language ID");

                languages[key] = value;
            }
        }

        #endregion

        #region Fields

        private Dictionary<string, string> languages;

        #endregion

        #region Constructor

        public LanguageSettings(string charId)
        {
            CharacterID = charId;
            languages = new Dictionary<string, string>();

            foreach (string langId in Constants.LANGUAGE_IDS)
                languages.Add(langId, langId);
        }

        private LanguageSettings(string charId, Dictionary<string, string> settings)
        {
            CharacterID = charId;
            languages = new Dictionary<string, string>(settings);
        }

        #endregion

        #region Methods

        public LanguageSettings Clone() => new LanguageSettings(CharacterID, languages);

        #endregion
    }
}
