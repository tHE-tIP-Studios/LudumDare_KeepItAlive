using System.Collections.Generic;
using Talkie;
using UnityEngine;
using Scripts;

namespace WALTApp
{
    public static class MessageSender
    {
        public const float MESSAGE_DELAY = 1.6f;

        private static GameObject _msgDisplayPrefab;
        private static List<MessageDisplay> _sentMessages;
        private static Transform _mainCanvas;

        private static float _timeOfLastMessage;
        private static AudioClip _playerSound = Resources.Load<AudioClip>("Audio/PlayerMessageSound");
        private static AudioClip _aiSound = Resources.Load<AudioClip>("Audio/AIMessageSound");

        public static bool CanSendMessage => Time.time - _timeOfLastMessage >= MESSAGE_DELAY;

        static MessageSender()
        {
            _sentMessages = new List<MessageDisplay>(10);
            _msgDisplayPrefab = Resources.Load<GameObject>("Prefabs/Sent Text");
            _mainCanvas = GameObject.Find("Canvas").transform;
            _timeOfLastMessage = -9000;
        }

        // For talkies
        public static bool SendMessage(CharacterProfile talkieProfile, string text)
        {
            if (!CanSendMessage) return false;
            AudioManager.PlaySound(_aiSound);
            _timeOfLastMessage = Time.time;
            DisplayMessage(talkieProfile.IconicColor, text, false);
            return true;
        }

        // For player
        public static bool SendMessage(string text)
        {
            if (!CanSendMessage) return false;
            AudioManager.PlaySound(_playerSound);
            DisplayMessage(Color.white, text, true);
            return true;
        }

        private static void DisplayMessage(Color bgColor, string text, bool isPlayer)
        {
            _timeOfLastMessage = Time.time;
            MessageDisplay newMsg = CreateNewMessageDisplay();

            // Initialize Message
            newMsg.transform.SetParent(_mainCanvas.GetChild(1));
            newMsg.transform.localScale = new Vector3(1,1,1);
            newMsg.SetText(text);
            newMsg.SetColor(bgColor);
            if (isPlayer) newMsg.SetPositionOnRight();
            else newMsg.SetPositionOnLeft();
            newMsg.SlideIn();

            // Finalize
            MoveAllUp();
            _sentMessages.Add(newMsg);
        }

        public static void MessageIsGone(MessageDisplay whoGone)
        {
            _sentMessages.Remove(whoGone);
        }

        private static void MoveAllUp()
        {
            foreach (MessageDisplay m in _sentMessages.ToArray())
                m.ScrollUp();
        }

        private static MessageDisplay CreateNewMessageDisplay() =>
            GameObject.Instantiate(_msgDisplayPrefab).GetComponent<MessageDisplay>();

    }
}