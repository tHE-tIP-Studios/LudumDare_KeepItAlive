using UnityEngine;

namespace Scripts
{
    [CreateAssetMenu(menuName = "Player Phrases")]
    public class PlayerPhraseDeck : ScriptableObject
    {
        [SerializeField] private Phrase[] _phrases = default;

        public int Entries => _phrases.Length;
        public Phrase this[int i] => _phrases[i];
    }
}