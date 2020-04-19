using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Talkie
{
    public class TalkieArea : MonoBehaviour
    {
        [SerializeField] private CharacterProfile _profile = null;

        [SerializeField] private Image _talkieImg = null;
        [SerializeField] private Image _backgroundImage = null;
        [SerializeField] private TextMeshProUGUI _namePro = null;
        [SerializeField] private Image _wallpaper = null;

        public CharacterProfile Profile => _profile;

        private void Awake()
        {
            _talkieImg.sprite = _profile.CharacterImage;    
            _wallpaper.sprite = _profile.Background;
            _backgroundImage.color = _profile.IconicColor;
            _namePro.SetText(_profile.Name);
        }
    }
}