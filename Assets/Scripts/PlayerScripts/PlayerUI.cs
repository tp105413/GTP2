using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;

    public void PlayerIdleToAttack()
    {
        playerAnimator.SetLayerWeight(1, 1f);
        playerAnimator.Play("walking", 1, 0f);
        playerAnimator.SetTrigger("ToAttack");
    }

    public void PlayerAttackToIdle()
    {
        playerAnimator.SetLayerWeight(1, 0f);
        playerAnimator.Play("sitting down from idle", 0, 0f);
        playerAnimator.SetTrigger("ToIdle");
    }

    public void PlayAttackTrigger()
    {
        playerAnimator.SetTrigger("AttackTrigger");
    }
}
