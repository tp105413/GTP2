using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuideBook : MonoBehaviour
{
    public TMP_Text titleLabel;
    public TMP_Text descriptionText;
    public GameObject[] tutorialImages;
    public GameObject[] glowingImages;
    public Button nextButton;
    public TMP_Text buttonText;
    public Image tentImage;

    public string[] titles;
    [TextArea(3, 8)] public string[] descriptions;
    private int currentPage = 0;
    

    void Start()
    {
        nextButton.onClick.AddListener(NextPage);
        ShowPage(currentPage);
    }

    private void ShowPage(int page)
    {
        titleLabel.text = titles[page];
        descriptionText.text = descriptions[page];
        buttonText.text = "Next (" + (page + 1) + "/4)";

        // Turn every images off
        for(int i = 0; i < tutorialImages.Length; i++)
        {
            tutorialImages[i].SetActive(false);
            glowingImages[i].SetActive(false);
        }

        // Only turn on the current page image and glowing image
        tutorialImages[page].SetActive(true);
        glowingImages[page].SetActive(true);
    }

    private void NextPage()
    {
        currentPage++;

        // Check if current page is last page
        if(currentPage >= titles.Length)
        {
            gameObject.SetActive(false);
            currentPage = 0;
        }

        ShowPage(currentPage);
    }

    public void ChangeTentImage(Sprite sprite)
    {
        tentImage.sprite = sprite;
        tutorialImages[1].GetComponent<Image>().sprite = sprite;
    }
}
