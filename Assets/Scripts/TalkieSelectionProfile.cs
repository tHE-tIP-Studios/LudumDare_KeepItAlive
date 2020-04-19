using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Talkie
{
    public class TalkieSelectionProfile : MonoBehaviour
    {
        [SerializeField] private Image _characterImg = null;
        [SerializeField] private Image _background = null;

        [SerializeField] private TextMeshProUGUI _namePro = null;
        [SerializeField] private TextMeshProUGUI _descriptionPro = null;

        private CharacterProfile _profile;

        public CharacterProfile Profile => _profile;

        public void Init(CharacterProfile profile)
        {
            transform.localPosition = Vector3.zero;
            _characterImg.sprite = profile.CharacterImage;
            _background.color = profile.IconicColor;
            _descriptionPro.SetText(profile.Description);
            _namePro.SetText(profile.Name);
        }

        public void MoveSideways(float xValue)
        {
            LeanTween.moveLocalX(gameObject, transform.localPosition.x + xValue, TalkieSelection.MOVE_COOLDOWN - 0.1f)
            .setEaseOutCirc();
        }
    }
}