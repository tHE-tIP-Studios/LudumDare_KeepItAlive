using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Conversation
{
    public class ConversationMaster : MonoBehaviour
    {
        /// <summary>
        /// What the player can say
        /// </summary>
        [SerializeField] private PlayerPhraseDeck _answers = default;
        /// <summary>
        /// What the AI will say accordingly
        /// </summary>
        [SerializeField] private PlayerPhraseDeck _aiDialogue = default;
        /// <summary>
        /// Keeps track of phrases with certain ID and its answer
        /// </summary>
        private Dictionary<int, List<Phrase>> _dialogueNodes = default;

        private void Awake()
        {
            // Initialize dictionary with ids
            _dialogueNodes = new Dictionary<int, List<Phrase>>(_answers.Entries);
            // Add ids to the dictionary
            for (int i = 0; i < _answers.Entries; i++)
            {
                _dialogueNodes.Add(_answers[i][0], new List<Phrase>());
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

        public Phrase RequestAnswer(Phrase playerPhrase, int id = 0)
        {
            throw new System.NotImplementedException("please implement this you fucktard");
        }
    }
}