using UnityEngine;
using UnityEngine.UI;

public class Collection : MonoBehaviour
{
	[SerializeField] private Image objectImage;

    public GuideBook guideBook;

	private Image buttonImage;


    void Awake()
    {
        buttonImage = GetComponent<Image>();
    }

    public void ChangeImage()
	{
		objectImage.sprite = buttonImage.sprite;
        guideBook.ChangeTentImage(buttonImage.sprite);
    }
}