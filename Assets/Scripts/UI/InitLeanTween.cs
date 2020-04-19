using UnityEngine;

public class InitLeanTween : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        LeanTween.init(1000);
        Destroy(this);
    }
}
