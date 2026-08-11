using UnityEngine;

public class Background : MonoBehaviour
{
	public float moveSpeed = 30f;
	public float anchoredX;

	[SerializeField] private CountdownTimer countdownTimer;

	private RectTransform rectTransform;
	private Vector2 startPosition;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
		startPosition = rectTransform.anchoredPosition;
    }

    void Update()
	{
		if (GameManager.Instance.isAttacking || !(countdownTimer.IsTimerRunning)) return;

		rectTransform.anchoredPosition += Vector2.left * moveSpeed * Time.deltaTime;

		if (rectTransform.anchoredPosition.x < anchoredX)
		{
			rectTransform.anchoredPosition = startPosition;
		}
	}
}