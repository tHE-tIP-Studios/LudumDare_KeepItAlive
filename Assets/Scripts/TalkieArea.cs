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

        private void Awake()
        {
            _talkieImg.sprite = _profile.CharacterImage;    
            _backgroundImage.color = _profile.ImageBgColor;
            _namePro.SetText(_profile.Name);
        }
    }
}