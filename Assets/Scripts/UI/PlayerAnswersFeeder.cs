using UnityEngine;
using System.Collections.Generic;
using Scripts.Conversation;
using WALTApp;

namespace Scripts.UI
{
    public class PlayerAnswersFeeder : MonoBehaviour
    {
        private const int PLAYER_CHOICES = 4;
        
        [SerializeField] private AudioClip _buttonPressSound;
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

        public void ReplaceUsed()
        {
            _master.LastChoice = _lastUsed.CurrentPhrase;
            _lastUsed.AssignNewText(_master.NewPhrase());
        }

        private void OnChoicePick(AnswerButton buttonPressed)
        {
            _lastUsed = buttonPressed;

            // Play a funny bloop sound
            AudioManager.PlaySound(_buttonPressSound, 1, Random.Range(0.9f, 1f));
            MessageSender.SendMessage(_lastUsed.CurrentPhrase.Answer);
            Debug.Log(buttonPressed.name);
        }
    }
}