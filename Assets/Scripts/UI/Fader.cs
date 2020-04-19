using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] private Image _faderImg;
        public void Fade(bool fadeIn, bool destroyOnDone, float time, float delay = 0, Action onDone = null)
        {
            Color startColor;
            Color endColor;

            if (fadeIn)
            {
                startColor = default;
                endColor = Color.black;
            }
            else
            {
                startColor = Color.black;
                endColor = default;
            }

            OnDone = onDone;
            if (destroyOnDone) OnDone += DestroySelf;

            LeanTween.value(_faderImg.gameObject, ValueCallback, startColor, endColor, time).setEaseLinear().setOnComplete(OnFinished).setDelay(delay);
        }

        private void ValueCallback(Color val)
        {
            _faderImg.color = val;
        }

        private void OnFinished()
        {
            OnDone?.Invoke();
        }

        private void DestroySelf()
        {
            Destroy(gameObject);
        }

        Action OnDone;
    }
}