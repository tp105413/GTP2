using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI levelText;


    void Start()
    {
        UpdateLevelUI();
    }

    // Update level text UI
    public void UpdateLevelUI()
    {
        levelText.text = "Lv." + GameManager.Instance.gameLevel.ToString();
    }
}
