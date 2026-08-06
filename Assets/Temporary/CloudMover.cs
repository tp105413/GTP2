using UnityEngine;

public class CloudMover : MonoBehaviour
{
	[Header("移动速度")]
	public float moveSpeed = 0.5f;

	[Header("离开屏幕后重新出现的位置")]
	public float respawnX = 15f;

	[Header("随机高度范围")]
	public float minY = 2f;
	public float maxY = 5f;

	[Header("消失位置")]
	public float destroyX = -15f;

	void Update()
	{
		

		transform.position +=
			Vector3.left *
			moveSpeed *
			Time.deltaTime;

		if (transform.position.x < destroyX)
		{
			Respawn();
		}
	}

	void Respawn()
	{
		float randomY = Random.Range(minY, maxY);

		transform.position = new Vector3(
			respawnX,
			randomY,
			transform.position.z
		);
	}
}