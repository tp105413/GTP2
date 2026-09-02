using UnityEngine;

public class CollectionItem : MonoBehaviour
{
	public GameObject mask;

	public void Unlock()
	{
		mask.SetActive(false);
	}

	public void Lock()
	{
		mask.SetActive(true);
	}
}