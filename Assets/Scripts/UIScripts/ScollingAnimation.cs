using System;
using UnityEngine;

public class ScrollingAnimation : MonoBehaviour
{ 
    [SerializeField] private CountdownTimer countdownTimer;

    private Animation scrollAnimation;

    private bool isTimerStarted = false;


    void Start()
    {
        // Get the Animation component attached to the same Game Object
        scrollAnimation = GetComponent<Animation>();
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
}
