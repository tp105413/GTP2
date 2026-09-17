using UnityEngine;
using UnityEngine.UI;

public class ButtonRunning : MonoBehaviour
{
    [SerializeField] private float leftx;
    [SerializeField] private float rightx;
    [SerializeField] private float stopx;

    public float moveSpeed = 300f;
    private RectTransform rectTransform;
    private Vector2 startPosition;
    public bool isMovingLeft = true;


    // Initialize the RectTransform and store its starting position
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;
    }

    // Move the background to the left while game is running
    void Update()
    {
        ButtonGoLeftAndHide();
    }

    public void CallButtonAgain()
    {
        gameObject.SetActive(true);
        rectTransform.anchoredPosition = new Vector2(rightx, rectTransform.anchoredPosition.y);
        isMovingLeft = true;
    }

    public void ButtonGoLeftAndHide()
    {
        if (!isMovingLeft) return;
        if (GameManager.Instance.isAttacking || !(GameManager.Instance.isGameRunning)) return;

        rectTransform.anchoredPosition += Vector2.left * moveSpeed * Time.deltaTime;

        // After reaching the anchoredX position, stop moving the button
        if (rectTransform.anchoredPosition.x < leftx)
        {
            isMovingLeft = false;
            rectTransform.anchoredPosition = new Vector2(rightx, rectTransform.anchoredPosition.y);
            rectTransform.gameObject.SetActive(false);
        }
    }
}