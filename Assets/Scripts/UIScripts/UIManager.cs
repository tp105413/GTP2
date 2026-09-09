using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public GameObject DamagePrefab;


    void Start()
    {
        UpdateLevelUI();
    }

    // Update level text UI
    public void UpdateLevelUI()
    {
        levelText.text = "Lv." + GameManager.Instance.gameLevel.ToString();
    }

    // Spawn damage pop up UI
    public void SpawnDamagePopUpUI(int damage, bool isCrit, bool isSkill, Enemy enemy)
    {
        GameObject popUp = Instantiate(DamagePrefab, enemy.transform.position, Quaternion.identity);

        TMP_Text damageText = popUp.GetComponentInChildren<TMP_Text>();
        damageText.text = damage.ToString();

        if (isSkill)
        {
            // Change damage text color to blue if is skill attack
            damageText.color = Color.blue;
        }
        else
        {
            // Change damage text color to orange if is critical hit
            damageText.color = isCrit ? Color.orange : Color.white;
        }
    }
}
