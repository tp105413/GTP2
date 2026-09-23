using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public enum Direction { Left, Right }

    public Direction direction = Direction.Left;
    public float cloudSpeed = 10f;
    public float anchoredX;
    public float anchoredX2;

    private RectTransform rectTransform;
    private Vector2 startPosition;


    // Initialize the RectTransform and store its starting position
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;
    }

    // Move the cloud to the left while game is running
    void Update()
    {
        float speed = cloudSpeed;

        // When character is running, cloud will move slightly faster
        if(GameManager.Instance.isGameRunning && !GameManager.Instance.isAttacking)
        {
            if(direction == Direction.Left)
            {
                speed += (GameManager.Instance.worldMoveSpeed / 10);
            }
            else if(direction == Direction.Right)
            {
                speed -= (GameManager.Instance.worldMoveSpeed / 10);
            }
        }

        // Determine cloud is moving left or right
        Vector2 movingDirection = direction == Direction.Left ? Vector2.left : Vector2.right;
        rectTransform.anchoredPosition += movingDirection * speed * Time.deltaTime;

        // Reset to starting position when reach x position
        if (direction == Direction.Left && rectTransform.anchoredPosition.x < anchoredX)
        {
            rectTransform.anchoredPosition = startPosition;
        }
        else if(direction == Direction.Right && (rectTransform.anchoredPosition.x > anchoredX|| rectTransform.anchoredPosition.x < anchoredX2))
        {
            rectTransform.anchoredPosition = startPosition;
        }
    }
}