using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingAnimation : MonoBehaviour
{
    [SerializeField] private CountdownTimer countdownTimer;
    [SerializeField] private EachMenuForBTN eachMenuForBTN;

    private Animation scrollAnimation;
    private bool isTimerStarted = false;


    void Start()
    {
        // Get the Anim ation component attached to the same GameObject
        scrollAnimation = GetComponent<Animation>();
        PlayLogoAnimation();
    }

    public void ToggleScrollPosition()
    {
        // Exit if Animation component is not found
        if (scrollAnimation == null) return;

        if (!isTimerStarted)
        {
            // Exit when Timer is set to 0
            if (countdownTimer.TimeRemaining == TimeSpan.Zero) return;

            // Play scroll down animation and mark the timer as started
            scrollAnimation.Play("UI_ScrollDown");
            isTimerStarted = true;
        }
        else
        {
            // Play scroll up animation and mark the timer as stopped
            scrollAnimation.Play("UI_ScrollUp");
            isTimerStarted = false;
        }
    }

    public void MainMenuAnimation()
    {
        {
            // Play MainMenu animation to NavigationView
            scrollAnimation.Play("MainMenu(Test)");
            isTimerStarted = false;

            // If player first time play show guide book on start
            if (!GameManager.Instance.firstTimePlay)
            {
                GameManager.Instance.firstTimePlay = true;
                StartCoroutine(WaitForScrollingCompleted());
            }
            else return;
        }
    }

    // Wait for scrolling animation completed then show guide book
    IEnumerator WaitForScrollingCompleted()
    {
        yield return new WaitForSeconds(1f);
        eachMenuForBTN.GuideBook();
    }

    // Play logo animation loop
    public void PlayLogoAnimation()
    {
        if (scrollAnimation != null)
        {
            if (scrollAnimation["LogoAnimation"] != null)
            {
                scrollAnimation["LogoAnimation"].wrapMode = WrapMode.Loop;
            }
            scrollAnimation.Play("LogoAnimation");
        }
    }

    // Wait for the scroll animation to complete then start the logo animation
    private IEnumerator BackThenLoopLogoRoutine()
    {
        scrollAnimation.Play("BackToMainMenu");

        if (scrollAnimation["BackToMainMenu"] != null)
        {
            yield return new WaitForSeconds(scrollAnimation["BackToMainMenu"].length);
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
        }
        PlayLogoAnimation();
    }

    // Return to the MainMenu
    public void BackToMainMenu()
    {
        if (scrollAnimation == null) return;

        StopAllCoroutines();
        StartCoroutine(BackThenLoopLogoRoutine());
    }
}
