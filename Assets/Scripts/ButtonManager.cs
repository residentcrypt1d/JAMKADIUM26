using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void Load(string sceneName)
    {
        SceneLoader.instance.LoadScene(sceneName);
    }

    public void NextLevel()
    {
        SceneLoader.instance.StartLevelWithNum(LevelManager.instance.currLevel + 1);
    }
}
