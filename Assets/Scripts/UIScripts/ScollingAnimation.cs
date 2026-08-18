using System;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingAnimation : MonoBehaviour
{ 
    [SerializeField] private CountdownTimer countdownTimer;
    [SerializeField] private PlayerUI playerUI;


    private Animation scrollAnimation;

    private bool isTimerStarted = false;

    void Start()
    {
        scrollAnimation = GetComponent<Animation>();
    }

    public void ToggleScrollPosition()
    {
        if (scrollAnimation == null)
        {
            return;
        }


        if (!isTimerStarted)
        {
            if (countdownTimer.TimeRemaining == TimeSpan.Zero) return;

            scrollAnimation.Play("UI_ScrollDown");
            isTimerStarted = true;
            playerUI.PlayerIdleToAttack();
        }
        else
        {
            scrollAnimation.Play("UI_ScrollUp");
            isTimerStarted = false;
            playerUI.PlayerAttackToIdle();
        }
    }
}
