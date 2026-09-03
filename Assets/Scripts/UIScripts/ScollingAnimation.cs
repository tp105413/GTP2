using System;
using System.Collections;
using UnityEngine;

public class ScrollingAnimation : MonoBehaviour
{
    [SerializeField] private CountdownTimer countdownTimer;

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
        }
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
