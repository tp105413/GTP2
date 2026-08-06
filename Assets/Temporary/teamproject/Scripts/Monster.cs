using UnityEngine;

public class Monster : MonoBehaviour
{
	private bool hasReachedPlayer = false;

	void Update()
	{
		if (hasReachedPlayer)
			return;

		if (transform.position.x > -1f)
		{
			transform.position +=
				Vector3.left *
				GameManager.Instance.worldMoveSpeed *
				Time.deltaTime;
		}
		else
		{
			transform.position = new Vector3(
				-1f,
				transform.position.y,
				transform.position.z
			);

			hasReachedPlayer = true;

			GameManager.Instance.StartFight();
		}
	}
}