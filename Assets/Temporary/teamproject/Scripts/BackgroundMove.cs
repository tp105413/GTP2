using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
	public float moveSpeed = 1f;

	void Update()
	{
		if (GameManager.Instance.IsFighting)
			return;

		transform.position += Vector3.left * moveSpeed * Time.deltaTime;
	}
}