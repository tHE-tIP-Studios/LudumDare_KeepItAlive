using System.Collections.Generic;
using UnityEngine;
using System;

namespace Scripts.Conversation
{
    public class ConversationMaster : MonoBehaviour
    {
        /// <summary>
        /// What the player can say
        /// </summary>
        [SerializeField] private PlayerPhraseDeck _playersAnswers = default;
        /// <summary>
        /// What the AI will say accordingly
        /// </summary>
        [SerializeField] private PlayerPhraseDeck _aiDialogue = default;
        /// <summary>
        /// Keeps track of phrases with certain ID and its answer
        /// </summary>
        private Dictionary<int, List<Phrase>> _dialogueNodes = default;

        private List<Phrase> _availablePhrases;

        public event Action outOfPhrases;

        private void Awake()
        {
            _availablePhrases = new List<Phrase>();
            // Initialize dictionary with ids
            _dialogueNodes = new Dictionary<int, List<Phrase>>(_playersAnswers.Entries);
            // Add ids to the dictionary
            for (int i = 0; i < _playersAnswers.Entries; i++)
            {
                _dialogueNodes.Add(_playersAnswers[i][0], new List<Phrase>());
                _availablePhrases.Add(_playersAnswers[i]);
                for (int j = 0; j < _aiDialogue.Entries; j++)
                {
                    for(int k = 0; k < _aiDialogue[j].IDs.Length; k++)
                    {
                        // Check IDS and insert them into the dictionary
                        if (_dialogueNodes.ContainsKey(_aiDialogue[j][k]))
                        {
                            _dialogueNodes[_aiDialogue[j][k]].Add(_aiDialogue[j]);
                        }
                    }
                }
            }
        }

        public Phrase NewPhrase()
        {
            // Declare a gameOver if this is a thing
            if (_availablePhrases.Count <= 0)
                outOfPhrases?.Invoke();
            
            Phrase newPhrase = _availablePhrases
                [UnityEngine.Random.Range(0, _availablePhrases.Count - 1)];
            
            _availablePhrases.Remove(newPhrase);
            return newPhrase;
        }

        public Phrase[] NewPhrase(int numberOfPhrases)
        {
            if (_availablePhrases.Count < numberOfPhrases)
                numberOfPhrases = _availablePhrases.Count;
            
            Phrase[] newPhrases = new Phrase[numberOfPhrases];
            
            for (int i = 0; i < numberOfPhrases; i++)
            {
                newPhrases[i] = NewPhrase();
            }

            return newPhrases;
        }
    }
}