using UnityEngine;

public class Background : MonoBehaviour
{
	public float moveSpeed = 30f;
	public float anchoredX;

	private RectTransform rectTransform;
	private Vector2 startPosition;


	// Initialize the RectTransform and store its starting position
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
		startPosition = rectTransform.anchoredPosition;
    }

	// Move the background to the left while game is running
    void Update()
	{
		if (GameManager.Instance.isAttacking || !(GameManager.Instance.isGameRunning)) return;

		rectTransform.anchoredPosition += Vector2.left * moveSpeed * Time.deltaTime;

		// Reset to starting position when reach x position
		if (rectTransform.anchoredPosition.x < anchoredX)
		{
			rectTransform.anchoredPosition = startPosition;
		}
	}
}