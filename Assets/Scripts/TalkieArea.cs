using System.Collections;
using System.Collections.Generic;
using Scripts.Conversation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Talkie
{
    public class TalkieArea : MonoBehaviour
    {
        private static CharacterProfile _activeProfile;
        public static CharacterProfile ActiveProfile { get => _activeProfile; set { _activeProfile = value; } }

        // This is for debug only
        [SerializeField] private CharacterProfile _debugProfile = null;

        [SerializeField] private Image _talkieImg = null;
        [SerializeField] private Image _backgroundImage = null;
        [SerializeField] private TextMeshProUGUI _namePro = null;
        [SerializeField] private Image _wallpaper = null;
        [SerializeField] private GameObject _dots = null;
        private GameObject[] _individualDots;

        private ConversationMaster _conversationMaster;

        private void Awake()
        {
            WALTApp.MessageSender.NewTalk();
            if (ActiveProfile == null)
                ActiveProfile = _debugProfile;
            _talkieImg.sprite = ActiveProfile.CharacterImage;
            _wallpaper.sprite = ActiveProfile.Background;
            _backgroundImage.color = ActiveProfile.IconicColor;
            _namePro.SetText(ActiveProfile.Name);

            _conversationMaster = FindObjectOfType<ConversationMaster>();

            _individualDots = new GameObject[3]
            {
                _dots.transform.GetChild(0).gameObject,
                _dots.transform.GetChild(1).gameObject,
                _dots.transform.GetChild(2).gameObject
            };
        }

        private void Start()
        {
            StartCoroutine(CDoDotCycle());
        }

        private void Update()
        {
            _dots.SetActive(_conversationMaster.IsAITyping);
        }

        private IEnumerator CDoDotCycle()
        {
            DoDotAnimation(0);
            yield return new WaitForSeconds(0.6f);
            DoDotAnimation(1);
            yield return new WaitForSeconds(0.6f);
            DoDotAnimation(2);
            yield return new WaitForSeconds(1.2f);

            while (!_conversationMaster.IsAITyping)
                yield return null;

            StartCoroutine(CDoDotCycle());
        }

        private void DoDotAnimation(byte dotIndex)
        {
            LeanTween.moveY(_individualDots[dotIndex], _individualDots[dotIndex].transform.position.y + 20, 0.4f).setEasePunch();
        }
    }
}