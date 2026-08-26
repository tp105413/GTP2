using UnityEngine;
using UnityEngine.UI;

public class RewardViewButton : MonoBehaviour
{
    public bool isRewardViewOpen = false;
    public GameObject RewardView;

    void Start()
    {
        if (RewardView != null)
        {
            isRewardViewOpen = RewardView.activeSelf;
            Debug.Log("Hide");
        }
    }

    public void RewardViewShow()
    {
        isRewardViewOpen = !isRewardViewOpen;
        if (RewardView != null)
        {
            RewardView.SetActive(isRewardViewOpen);
            Debug.Log("RewardView");
        }
    }
}