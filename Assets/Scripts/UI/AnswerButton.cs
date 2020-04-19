using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Scripts.UI
{
    public class AnswerButton : MonoBehaviour
    {
        private Phrase _currentPhrase;
        private Button _button;
        private TextMeshProUGUI _buttonText;
        public Phrase CurrentPhrase => _currentPhrase;

        private void Awake() 
        {
            _button = GetComponent<Button>();
            _buttonText = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetActive(bool value)
        {
            _button.interactable = value;
        }

        public void AssignOnClick<T>(Action<T> functionToAssign, T something)
        {
            _button.onClick.AddListener(() => functionToAssign?.Invoke(something));
        }
        
        public void AssignOnClick(Action functionToAssign)
        {
            _button.onClick.AddListener(() => functionToAssign?.Invoke());
        }

        public void AssignNewText(Phrase text)
        {
            if (text.Answer.Contains("\n"))
            {
                string[] final = text.Answer.Split('\n');
                _buttonText.text = final[0] + "...";
            }
            else
                _buttonText.text = text.Answer;
            _currentPhrase = text;
        }
    }
}