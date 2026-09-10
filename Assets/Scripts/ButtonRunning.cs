using UnityEngine;

public class ButtonRunning : MonoBehaviour
{
    public float moveSpeed = 30f;
    [SerializeField] private float leftx;
    [SerializeField] private float rightx;
    //[SerializeField] private float rightx; 
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
        if (!isMovingLeft) return;
        if (GameManager.Instance.isAttacking || !(GameManager.Instance.isGameRunning)) return;

        rectTransform.anchoredPosition += Vector2.left * moveSpeed * Time.deltaTime;

        // After reaching the anchoredX position, stop moving the button
        if (rectTransform.anchoredPosition.x < leftx)
        {
            isMovingLeft = false;
            rectTransform.gameObject.SetActive(false);
            rectTransform.anchoredPosition = new Vector2(rightx,rectTransform.anchoredPosition.y);
        }
    }

    public void ChangetoRightSide()
    {
        //rectTransform.
    }
}

