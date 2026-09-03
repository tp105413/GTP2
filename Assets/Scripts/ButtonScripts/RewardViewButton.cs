using UnityEngine;

public class RewardViewButton : MonoBehaviour
{
    public bool isRewardViewOpen = false;
    public GameObject RewardView;


    void Start()
    {
        if (RewardView != null)
        {
            isRewardViewOpen = RewardView.activeSelf;
        }
    }

    public void RewardViewShow()
    {
        // Toggle the Reward View state
        isRewardViewOpen = !isRewardViewOpen;
        if (RewardView != null)
        {
            RewardView.SetActive(isRewardViewOpen);

            // Reset the temporary resources after Reward View is closed
            if (!isRewardViewOpen)
            {
                ResourceManager.Instance.ResetTempGoldAndSouls();
            }
        }
    }
}