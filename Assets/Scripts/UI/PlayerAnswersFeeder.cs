using UnityEngine;
using System.Collections.Generic;
using Scripts.Conversation;
using WALTApp;

namespace Scripts.UI
{
    public class PlayerAnswersFeeder : MonoBehaviour
    {
        private const int PLAYER_CHOICES = 4;
        private List<AnswerButton> _buttons;
        private AnswerButton _lastUsed;
        
        /// <summary>
        /// Oh yes whip me master
        /// </summary>
        private ConversationMaster _master;

        private void Start()
        {
            _master = FindObjectOfType<ConversationMaster>();
            _buttons = new List<AnswerButton>(PLAYER_CHOICES);

            var buttons = GetComponentsInChildren<AnswerButton>();
            for (int i = 0; i < PLAYER_CHOICES; i++)
            {
                AnswerButton bTrans = buttons[i];
                buttons[i].AssignOnClick<AnswerButton>(OnChoicePick, bTrans);
                _buttons.Add(buttons[i]);
            }

            if (_master == null) 
                Debug.LogError("Place a Conversation master in scene please");
            else
                InjectNewAnswers(_master.NewPhrase(PLAYER_CHOICES));
        }

        public void InjectNewAnswers(Phrase[] newPhrases)
        {
            for (int i = 0; i < newPhrases.Length; i++)
            {
                _buttons[i].AssignNewText(newPhrases[i]);
            }
        }

        private void ReplaceUsed()
        {
            Phrase phrase = _master.NewPhrase();
            if (phrase.Answer == null)
            {
                _lastUsed.gameObject.SetActive(false);
                return;
            }
            _lastUsed.AssignNewText(phrase);
        }

        private void OnChoicePick(AnswerButton buttonPressed)
        {
            _lastUsed = buttonPressed;
            _master.LastChoice = _lastUsed.CurrentPhrase;

            if(MessageSender.SendMessage(_lastUsed.CurrentPhrase.Answer))
            {
                ReplaceUsed();
                // Call for the ai to answer
                _master.Answer();
            }
        }
    }
}