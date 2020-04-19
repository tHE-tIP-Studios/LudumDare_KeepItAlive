using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LogoScreen : MonoBehaviour
    {
        [SerializeField] private Image _logoImage = null;
        [SerializeField] private Fader _fader = null;

        private void Awake()
        {
            Vector3 rlySmolScale = new Vector3(0.01f, 0.01f, 0.01f);
            _logoImage.transform.localScale = rlySmolScale;
        }

        private void Start()
        {
            StartAnim();
        }

        private void StartAnim()
        {
            LeanTween.scale(_logoImage.gameObject, Vector3.one, 1.0f).setEaseOutBack();
            LeanTween.rotateZ(_logoImage.gameObject, -45, 0.5f).setEasePunch().setDelay(1.3f);
            LeanTween.rotateZ(_logoImage.gameObject, 45, 0.5f).setEasePunch().setDelay(1.3f + 0.5f + 0.1f).setOnComplete(StartFade);
            LeanTween.scale(_logoImage.gameObject, Vector3.zero, .4f).setEaseInBack().setDelay(1.3f + 0.6f + 0.5f + 0.3f);
        }

        private void StartFade()
        {
            _fader.Fade(false, false, 1.6f, 0, DestroyHierarchy);
        }

        private void DestroyHierarchy()
        {
            Destroy(gameObject);
        }
    }
}