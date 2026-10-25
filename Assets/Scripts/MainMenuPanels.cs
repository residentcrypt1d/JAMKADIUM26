using UnityEngine;

public class MainMenuPanels : MonoBehaviour
{
    [SerializeField] Animator RulesPanelAnimator;
    [SerializeField] Animator CreditsPanelAnimator;

    private bool rulesOpen = false;
    private bool creditsOpen = false;
    
    public void OpenCredits()
    {
        CreditsPanelAnimator.SetTrigger("OpenPanel");
        creditsOpen = true;
    }

    public void OpenRules()
    {
        RulesPanelAnimator.SetTrigger("OpenPanel");
        rulesOpen = true;
    }

    public void CloseCredits()
    {
        CreditsPanelAnimator.SetTrigger("ClosePanel");
        creditsOpen = false;
    }

    public void CloseRules()
    {
        RulesPanelAnimator.SetTrigger("ClosePanel");
        rulesOpen = false;
    }
}
