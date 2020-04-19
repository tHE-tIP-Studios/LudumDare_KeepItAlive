using UnityEngine;
using UnityEngine.UI;

public class ButtonTapWobble : MonoBehaviour
{
    [Range(0,2)]
    [SerializeField] private float _scaleFactor = 1.3f;   

    [SerializeField] private float _duration = 0.5f;

    private Button _button;
    private Vector3 _initialScale;

    private void Awake()
    {
        _initialScale = transform.localScale;
        _button = GetComponent<Button>();
        if (_button == null)
        {
            _button = GetComponentInChildren<Button>();
            if (_button == null)
            {
                Debug.LogWarning("Button component not found in " + name + " Object. Self destruct sequence initialized.");
                Destroy(this);
            }
        }

        _button.onClick.AddListener(Wobble);
    }

    private void Wobble()
    {
        transform.localScale = _initialScale;
        LeanTween.scale(gameObject, transform.localScale * _scaleFactor, _duration).setEasePunch();
    }
}