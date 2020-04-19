using UnityEngine;

namespace Scripts
{
    [System.Serializable]
    public struct Phrase
    {
        [SerializeField] private int[] _id;
        [TextArea]
        [SerializeField] private string _phrase;
        [SerializeField] private PhraseType _type;

        public string Answer => _phrase;
        public PhraseType PhraseType => _type;
        public int[] IDs => _id;

        public int this[int i] => _id[i];

        public override string ToString()
        {
            return $"{_phrase} with Type: {_type}";
        }
    }
}