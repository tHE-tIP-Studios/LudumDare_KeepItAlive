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
        private RottenConversation _evaluator;
        
        public Phrase LastChoice {get; set;}
        public Phrase LastAnswer {get; set;}
        public event Action outOfPhrases;
        
        private void Awake()
        {
            _evaluator = new RottenConversation();
            _evaluator.DebugValues();
            _availablePhrases = new List<Phrase>();
            // Initialize dictionary with ids
            _dialogueNodes = new Dictionary<int, List<Phrase>>(_playersAnswers.Entries);
            // Add ids to the dictionary
            for (int i = 0; i < _playersAnswers.Entries; i++)
            {
                if (_dialogueNodes.ContainsKey(_playersAnswers[i][0]))
                    Debug.LogError("Check youw pwayew deck keys pwease!");

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
                [UnityEngine.Random.Range(0, _availablePhrases.Count)];
            
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

        public Phrase GetAnswerFor(Phrase playerPhrase)
        {
            if (!_dialogueNodes.ContainsKey(playerPhrase.IDs[0])) Debug.LogError("Yo, you forgot to put this ID in");
            Phrase aiPhrase;
            aiPhrase = _dialogueNodes[playerPhrase.IDs[0]][UnityEngine.Random.Range(0, _dialogueNodes[playerPhrase.IDs[0]].Count)];

            // Update the AI answer
            LastAnswer = aiPhrase;

            // Update Score as we now have the answer from the player and the ai
            UpdateScore();
            return aiPhrase;
        }

        public Phrase GetAnswerForCurrent()
        {
            if (!_dialogueNodes.ContainsKey(LastChoice.IDs[0])) Debug.LogError("Yo, you forgot to put this ID in");
            Phrase aiPhrase;
            aiPhrase = _dialogueNodes[LastChoice.IDs[0]][UnityEngine.Random.Range(0, _dialogueNodes[LastChoice.IDs[0]].Count)];
            
            // Update the AI answer
            LastAnswer = aiPhrase;

            // Update Score as we now have the answer from the player and the ai
            UpdateScore();
            return aiPhrase;
        }

        private void UpdateScore()
        {
            _evaluator.Evaluate(LastChoice.PhraseType, LastAnswer.PhraseType);
        }
    }
}