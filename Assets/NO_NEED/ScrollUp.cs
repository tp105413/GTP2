using UnityEngine;
using UnityEngine.UI;

public class ScrollUp : MonoBehaviour
{
    public ScrollRect scrollRect;

    public void ChangeToAttack()
    {
        scrollRect.verticalNormalizedPosition = 1f;
        Debug.Log("Change");
    }
}
