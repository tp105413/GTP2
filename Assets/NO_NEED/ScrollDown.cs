using UnityEngine;
using UnityEngine.UI;

public class ScrollDown : MonoBehaviour
{
    public ScrollRect scrollRect;
    public Animation Scroll;
    public void ChangeToIdle()
    {
        //scrollRect.verticalNormalizedPosition = 0f;
        Scroll.Play("UI_ScrollDown");
        Debug.Log("Change");
    }
}
