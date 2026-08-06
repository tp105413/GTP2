using UnityEngine;

public class LoopBackground : MonoBehaviour
{
	private float backgroundWidth;

	private void Start()
	{
		backgroundWidth = GetComponent<SpriteRenderer>().bounds.size.x;
	}

	void Update()
	{
		if (GameManager.Instance.isAttacking) return;

		transform.position += Vector3.left * GameManager.Instance.worldMoveSpeed * Time.deltaTime;

		if (transform.position.x <= -backgroundWidth)
		{
			transform.position += new Vector3(backgroundWidth * 2f, 0f, 0f);
		}
	}
}