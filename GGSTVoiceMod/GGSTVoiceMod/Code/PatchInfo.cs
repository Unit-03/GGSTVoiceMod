using System;
using System.Collections;
using System.Collections.Generic;

namespace GGSTVoiceMod
{
    public class PatchInfo : IEnumerable<PatchInfo.LangPatch>
    {
        #region Types

        public struct LangPatch
        {
            public string Character; // ID of the character being patched
            public string UseLang;   // ID of the language that is being used
            public string OverLang;  // ID of the language being replaced

            public LangPatch(string charId, string useId, string overId)
            {
                Character = charId;
                UseLang   = useId;
                OverLang  = overId;
            }

            public override bool Equals(object obj) => obj is LangPatch patch ? this == patch : false;
            public override int GetHashCode() => base.GetHashCode();

            public static bool operator !=(LangPatch left, LangPatch right) => !(left == right);
            public static bool operator ==(LangPatch left, LangPatch right)
            {
                return left.Character == right.Character &&
                       left.UseLang   == right.UseLang   &&
                       left.OverLang  == right.OverLang;
                }
        }

        private class Enumerator : IEnumerator<LangPatch>
        {
            public LangPatch Current => patches[index];
            object IEnumerator.Current => Current;

            private List<LangPatch> patches;
            private int index;

            public Enumerator(List<LangPatch> patchList)
            {
                patches = patchList;
                index = -1;
            }

            ~Enumerator()
            {
                Dispose();
            }

            public bool MoveNext() => ++index < patches.Count;
            public void Reset() => index = -1;

            public void Dispose()
            {
                patches = null;
                index = -1;

                GC.SuppressFinalize(this);
            }
        }

        #endregion

        #region Properties

        public int Count => patches.Count;
        public LangPatch this[int index] => patches[index];

        #endregion

        #region Fields

        private List<LangPatch> patches = new List<LangPatch>();

        #endregion

        #region Constructor

        public PatchInfo()
        {
            patches = new List<LangPatch>();
        }

        public PatchInfo(Dictionary<string, LanguageSettings> settings)
        {
            foreach (var pair in settings)
            {
                foreach (var langId in Constants.LANGUAGE_IDS)
                {
                    // If this character's language is modified
                    if (pair.Value[langId] != langId)
                        patches.Add(new LangPatch(pair.Key, pair.Value[langId], langId));
                }
            }
        }

        #endregion

        #region Methods

        public PatchInfo Diff(PatchInfo other)
        {
            // This is mostly just to circumvent an issue when the 'other' patch has zero patches and so it never actually iterates and results in an empty diff
            //  I think it's also probably a little more efficient to do small loops in a big one instead of the other way around but idk it doesn't matter lmao
            PatchInfo big   = other.Count > Count ? other : this;
            PatchInfo small = other.Count < Count ? other : this;

            PatchInfo patch = new PatchInfo();

            for (int i = 0; i < big.Count; ++i)
            {
                bool skip = false;

                for (int u = 0; u < small.Count; ++u)
                {
                    if (big[i] == small[u])
                    {
                        skip = true;
                        break;
                    }
                }

                if (!skip)
                    patch.AddPatch(big[i]);
            }

            return patch;
        }

        public bool AddPatch(LangPatch patch)
        {
            for (int i = 0; i < patches.Count; ++i)
            {
                if (patches[i].Character == patch.Character && patches[i].OverLang == patch.OverLang)
                    return false;
            }

            patches.Add(patch);
            return true;
        }

        public bool AddPatch(string charId, string useId, string overId)
        {
            return AddPatch(new LangPatch(charId, useId, overId));
        }

        public bool RemovePatch(int index)
        {
            if (index >= 0 && index < patches.Count)
            {
                patches.RemoveAt(index);
                return true;
            }

            return false;
        }

        public bool RemovePatch(LangPatch patch)
        {
            for (int i = patches.Count - 1; i >= 0; --i)
            {
                if (patches[i] == patch)
                {
                    patches.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        public bool RemovePatch(string charId, string useId, string overId)
        {
            return RemovePatch(new LangPatch(charId, useId, overId));
        }

        public IEnumerator<LangPatch> GetEnumerator() => new Enumerator(patches);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion
    }
}
