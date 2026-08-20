using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;


    // Start the running animation
    public void StartRunningAnimation()
    {
        playerAnimator.SetBool("IsRunning", true);
    }

    // Stop the running animation
    public void StopRunningAnimation()
    {
        playerAnimator.SetBool("IsRunning", false);
    }

    // Trigger the attack animation once
    public void PlayAttackTrigger()
    {
        playerAnimator.SetTrigger("Attack");
    }

    // Set player to attack state, switching between attack and idle animation
    public void StartAttackAnimation()
    {
        playerAnimator.SetBool("HasTarget", true);
    }

    // Set player back to running state
    public void StopAttackAnimation()
    {
        playerAnimator.SetBool("HasTarget", false);
    }
}
