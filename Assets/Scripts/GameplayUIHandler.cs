using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameplayUIHandler : MonoBehaviour
{
    [SerializeField] TMP_Text movesText;
    [SerializeField] Animator popupAnimator;
    [SerializeField] TMP_Text popupMovesText;
    [SerializeField] Image popupImage;
    [SerializeField] TMP_Text popupScoreText;
    [SerializeField] Sprite[] scoreSprites;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        LevelManager.instance.onMoveMade.AddListener(UpdateText);
        LevelManager.instance.onGameOver.AddListener(OpenPopup);
        UpdateText(0);
    }

    private void UpdateText(int num)
    {
        movesText.SetText("Moves: {0}", num);
    }

    private void OpenPopup(int num)
    {
        popupAnimator.SetTrigger("OpenPanel");
        int score = GameHandler.instance.FindTier(LevelManager.instance.currLevel);
        popupMovesText.SetText("Moves: {0}", num);
        if(score == 3)
        {
            popupScoreText.text = "You got a perfect score! Well done!";
        }
        else
        {
            popupScoreText.SetText("Complete the level in {0} moves or less to get a perfect score!", GameHandler.instance.GetThreeStarThreshold(LevelManager.instance.currLevel));
        }
        popupImage.sprite = scoreSprites[score];
    }
}
