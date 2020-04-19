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

        private void Awake()
        {
            WALTApp.MessageSender.NewTalk();
            if (ActiveProfile == null)
                ActiveProfile = _debugProfile;
            _talkieImg.sprite = ActiveProfile.CharacterImage;
            _wallpaper.sprite = ActiveProfile.Background;
            _backgroundImage.color = ActiveProfile.IconicColor;
            _namePro.SetText(ActiveProfile.Name);
        }
    }
}