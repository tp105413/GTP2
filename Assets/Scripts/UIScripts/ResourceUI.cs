using UnityEngine;
using TMPro;
using System.Collections;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    public TextMeshProUGUI tempGoldText;
    public TextMeshProUGUI tempSoulText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI soulText;
    public TextMeshProUGUI RewardGoldText;
    public TextMeshProUGUI RewardSoulText;


    private void OnEnable()
    {
        // OnEnable() is running before ResourceManager to awake
        // Wait for ResourceManager Instance is set, then subscribe
        StartCoroutine(SubscribeWhenReady());
    }

    IEnumerator SubscribeWhenReady()
    {
        while(ResourceManager.Instance == null) yield return null;

        // Subscribe to temporary gold/souls change events
        if (ResourceManager.Instance != null)
        {
            ResourceManager.Instance.OnTempGoldChanged += UpdateTempGold;
            ResourceManager.Instance.OnTempSoulsChanged += UpdateTempSouls;
        }

        // Subscribe to player gold/souls change events
        if (playerStats != null)
        {
            playerStats.OnGoldChanged += UpdateGold;
            playerStats.OnSoulsChanged += UpdateSouls;
        }
    }

    // Unsubscribe from all events when GameObject is disable
    private void OnDisable()
    {
        if (ResourceManager.Instance != null)
        {
            ResourceManager.Instance.OnTempGoldChanged -= UpdateTempGold;
            ResourceManager.Instance.OnTempSoulsChanged -= UpdateTempSouls;
        }

        if (playerStats != null)
        {
            playerStats.OnGoldChanged -= UpdateGold;
            playerStats.OnSoulsChanged -= UpdateSouls;
        }
    }

    // Update temporary gold UI
    public void UpdateTempGold(int amount)
    {
        if (tempGoldText != null)
        {
            tempGoldText.text = amount.ToString();
            RewardGoldText.text = amount.ToString();
        }
    }

    // Update temporary souls UI
    public void UpdateTempSouls(int amount)
    {
        if (tempSoulText != null)
        {
            tempSoulText.text = amount.ToString();
            RewardSoulText.text = amount.ToString();
        }
    }

    // Update player gold UI
    public void UpdateGold(int amount)
    {
        if(goldText != null)
        {
            goldText.text = amount.ToString();
        }
    }

    // Update player souls UI
    public void UpdateSouls(int amount)
    {
        if (soulText != null)
        {
            soulText.text = amount.ToString();
        }
    }
}
