using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace WALTApp
{
    public class MessageDisplay : MonoBehaviour
    {
        private const byte MAX_SCROLL_UP = 10;
        private const float SCROLL_AMOUNT = 320;
        private const float CENTER_OFFSET = 195;
        private const float INITIAL_HEIGHT = -220;

        private byte _scrollCount;

        private Image _bgImage;
        [SerializeField] private TextMeshProUGUI _textPro = null;
        [SerializeField] private TextMeshProUGUI _timePro = null;

        private float _x;

        private void Awake()
        {
            _bgImage = GetComponent<Image>();
        }

        public void SetText(string txt)
        {
            _textPro.SetText(txt);
        }

        public void SetColor(Color c)
        {
            _bgImage.color = c;
        }

        public void SetPositionOnRight()
        {
            _x = CENTER_OFFSET;
        }

        public void SetPositionOnLeft()
        {
            _x = -CENTER_OFFSET;
        }

        public void SlideIn()
        {
            DateTime now = DateTime.Now;
            string hour = (now.Hour == 12 ? 12 : now.Hour % 12).ToString().PadLeft(2, '0');
            string minutes = now.Minute.ToString().PadLeft(2, '0');
            string time = $"{hour}:{minutes}";
            _timePro.SetText(time);
            transform.localPosition = new Vector3(_x * 7, INITIAL_HEIGHT, transform.position.z);
            LeanTween.moveLocalX(gameObject, _x, MessageSender.MESSAGE_DELAY - 0.1f).setEaseOutElastic();
        }

        public void ScrollUp()
        {
            _scrollCount++;
            if (_scrollCount == MAX_SCROLL_UP)
            {
                MessageSender.MessageIsGone(this);
                Destroy(gameObject);
                return;
            }

            LeanTween.moveLocalY(gameObject, transform.localPosition.y + SCROLL_AMOUNT, MessageSender.MESSAGE_DELAY - 1.0f).setEaseOutCirc();
        }
    }
}