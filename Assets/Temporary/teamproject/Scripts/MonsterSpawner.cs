using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
	public GameObject monsterPrefab;

	void Start()
	{
		SpawnMonster();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.K))
		{
			KillCurrentMonster();
		}
	}

	void KillCurrentMonster()
	{
		Monster monster = FindFirstObjectByType<Monster>();

		if (monster != null)
		{
			Destroy(monster.gameObject);

			GameManager.Instance.EndFight();

			SpawnMonster();
		}
	}

	void SpawnMonster()
	{
		Debug.Log("生成怪物");

		Instantiate(
			monsterPrefab,
			new Vector3(10f, -2f, 0f),
			Quaternion.identity
		);
	}
}