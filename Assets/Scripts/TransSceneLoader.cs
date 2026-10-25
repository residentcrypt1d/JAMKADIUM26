using UnityEngine;

public class TransSceneLoader : MonoBehaviour
{
    [SerializeField] Animator transAnim;
    void OnEnable()
    {
        SceneLoader.instance.doneLoadingEvent.AddListener(StartTransOut);
    }
    void OnDisable()
    {
        SceneLoader.instance.doneLoadingEvent.RemoveListener(StartTransOut);
    }
    public void DoneTransIn()
    {
        SceneLoader.instance.SwapScenes();
    }
    public void StartTransOut()
    {
        transAnim.SetTrigger("StartTransOut");
    }
}
