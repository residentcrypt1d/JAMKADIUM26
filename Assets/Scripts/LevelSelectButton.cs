using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
    [SerializeField] private int levelNum;
    [SerializeField] private Sprite[] completionSprites;
    [SerializeField] private Image completionImage;
    [SerializeField] private GameObject lockOverlay;
    [SerializeField] private TMP_Text levelNumText;

    void Start()
    {
        CheckScore();
        levelNumText.SetText("{0}", levelNum);
    }
    public void StartLevel()
    {
        SceneLoader.instance.StartLevelWithNum(levelNum);
    }

    public void CheckScore()
    {
        if(GameHandler.instance.completedLevels < levelNum - 1)
        {
            lockOverlay.SetActive(true);
        }
        else
        {
            lockOverlay.SetActive(false);
        }
        completionImage.sprite = completionSprites[GameHandler.instance.FindTier(levelNum - 1)];
        Debug.Log(completionSprites[GameHandler.instance.FindTier(levelNum - 1)]);
    }
}
