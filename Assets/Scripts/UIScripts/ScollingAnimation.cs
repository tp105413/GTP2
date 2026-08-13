using System;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingAnimation : MonoBehaviour
{
    private Animation scrollAnimation;

    public Animator playerAnimator;

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
            scrollAnimation.Play("UI_ScrollDown");
            isAttack = true;
            Debug.Log("IdleToAttack");

            playerAnimator.SetLayerWeight(1, 1f);
            playerAnimator.Play("walking", 1, 0f);
            playerAnimator.SetTrigger("ToAttack");
        }
        else
        {
            scrollAnimation.Play("UI_ScrollUp");
            isAttack = false;
            Debug.Log("AttackToIdle");

            playerAnimator.SetLayerWeight(1, 0f);
            playerAnimator.Play("sitting down from idle 0", 0, 0f); 
            playerAnimator.SetTrigger("ToIdle");
        }
    }
}
