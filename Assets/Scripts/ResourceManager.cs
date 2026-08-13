using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    public TextMeshProUGUI goldText;
    public TextMeshProUGUI soulText;

    void Awake()
    { 
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void UpdateGold(int amount)
    {
        if (goldText != null)
        {
            goldText.text = amount.ToString();
        }
    }

    public void UpdateSoul(int amount)
    {
        if (soulText != null)
        {
            soulText.text = amount.ToString();
        }
    }
}
