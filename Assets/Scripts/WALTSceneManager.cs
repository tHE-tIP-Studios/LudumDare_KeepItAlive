using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WALTSceneManager : MonoBehaviour
{
    private static string _sceneToLoad;
    private static GameObject _fadeIn;

    public static void LoadMessageScene()
    {
        if (_fadeIn == null) _fadeIn = Resources.Load<GameObject>("Prefabs/Logo Screen FadeIn");
        _sceneToLoad = "MessageScene";
        CreateObj();
    }

    public static void LoadMainMenu()
    {
        if (_fadeIn == null) _fadeIn = Resources.Load<GameObject>("Prefabs/Logo Screen FadeIn");
        _sceneToLoad = "TalkieSelection";
        CreateObj();
    }

    private static void CreateObj()
    {
        new GameObject("Loading New Scene").AddComponent<WALTSceneManager>();
    }

    private void Awake()
    {
        GameObject.Instantiate(_fadeIn, GameObject.FindObjectOfType<Canvas>().transform);
        StartCoroutine(CLoadSceneDelay());
    }

    private IEnumerator CLoadSceneDelay()
    {
        yield return new WaitForSeconds(UI.LogoScreen.ANIMATION_DURATION + 0.05f);
        SceneManager.LoadScene(_sceneToLoad);
    }
}