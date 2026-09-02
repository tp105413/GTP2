using UnityEngine;

public class CollectionUI : MonoBehaviour
{
	public GameObject collectionPanel;

	public void OpenCollection()
	{
		collectionPanel.SetActive(true);
	}

	public void CloseCollection()
	{
		collectionPanel.SetActive(false);
	}
}