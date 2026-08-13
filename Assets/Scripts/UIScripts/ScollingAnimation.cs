using System;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEngine.Analytics.IAnalytic;

public class ScrollingAnimation : MonoBehaviour
{
    [SerializeField] private CountdownTimer countdownTimer;

    //public ScrollRect scrollRect;
    private Animation scrollAnimation;

    private bool isAttack = false;

    void Start()
    {
        scrollAnimation = GetComponent<Animation>();
    }

    public void ToggleScrollPosition()
    {
        if (scrollAnimation == null)
        {
            Debug.LogError("Animation File Lose");
            return;
        }

        if (!isAttack)
        {
            if (countdownTimer.TimeRemaining == TimeSpan.Zero) return;

            scrollAnimation.Play("UI_ScrollDown");
            isAttack = true;
            Debug.Log("IdletoAttack");
        }
        else
        {
            scrollAnimation.Play("UI_ScrollUp");
            isAttack = false; 
            Debug.Log("AttackToIdle");
        }
    }
}
