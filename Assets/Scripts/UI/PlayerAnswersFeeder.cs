using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using Scripts.Conversation;

namespace Scripts.UI
{
    public class PlayerAnswersFeeder : MonoBehaviour
    {
        private const int PLAYER_CHOICES = 4;
        private List<TextMeshProUGUI> _answerSpaces;
        private List<Button> _buttons;
        private Transform _lastUsed;
        
        /// <summary>
        /// Oh yes whip me master
        /// </summary>
        private ConversationMaster _master;

        private void Start()
        {
            _master = FindObjectOfType<ConversationMaster>();
            _answerSpaces = new List<TextMeshProUGUI>(PLAYER_CHOICES);
            _buttons = new List<Button>(PLAYER_CHOICES);

            var buttons = GetComponentsInChildren<Button>();
            Debug.Log(buttons.Length);
            var textAssets = GetComponentsInChildren<TextMeshProUGUI>();

            for (int i = 0; i < PLAYER_CHOICES; i++)
            {
                Transform bTrans = buttons[i].transform;
                
                buttons[i].onClick.AddListener(() => 
                    {
                        OnChoicePick(bTrans);
                    });
                
                _buttons.Add(buttons[i]);
                _answerSpaces.Add(textAssets[i]);
            }

            InjectNewAnswers(_master.NewPhrase(PLAYER_CHOICES));
        }

        public void InjectNewAnswers(Phrase[] newPhrases)
        {
            for (int i = 0; i < newPhrases.Length; i++)
            {
                _answerSpaces[i].text = newPhrases[i].Answer;
            }
        }

        public void ReplaceUsed()
        {
            string newAnswer = _master.NewPhrase().Answer;
            _lastUsed.GetComponentInChildren<TextMeshProUGUI>().text = newAnswer;
        }

        private void OnChoicePick(Transform buttonPressed)
        {
            _lastUsed = buttonPressed;
            Debug.Log(buttonPressed.name);
        }
    }
}