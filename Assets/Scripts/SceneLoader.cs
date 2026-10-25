using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    [SerializeField] private string transitionSceneName;
    private string targetScene;
    private string prevScene;
    public UnityEvent doneLoadingEvent = new UnityEvent();
    
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void LoadScene(string sceneName)
    {
        prevScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadSceneAsync(transitionSceneName, LoadSceneMode.Additive); 
        targetScene = sceneName;
    }

    public void UnloadTransitionScene()
    {
        SceneManager.UnloadSceneAsync(transitionSceneName);
    }

    public void SwapScenes()
    {
        StartCoroutine(nameof(SceneSwapEnumerator));
    }

    private IEnumerator SceneSwapEnumerator()
    {
        yield return SceneManager.UnloadSceneAsync(prevScene);
        yield return SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Additive);
        doneLoadingEvent.Invoke();
    }
}
