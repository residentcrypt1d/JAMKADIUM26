using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void Load(string sceneName)
    {
        SceneLoader.instance.LoadScene(sceneName);
    }

    public void StartLevel(int levelNum)
    {
        SceneLoader.instance.StartLevelWithNum(levelNum);
    }
}
