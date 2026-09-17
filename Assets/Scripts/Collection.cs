using UnityEngine;
using UnityEngine.UI;

public class Collection : MonoBehaviour
{
	[SerializeField] private Image objectImage;

	private Image buttonImage;


    void Awake()
    {
        buttonImage = GetComponent<Image>();
    }

    public void ChangeImage()
	{
		objectImage.sprite = buttonImage.sprite;
	}
}