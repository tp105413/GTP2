using System;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    [SerializeField] private ResourceUI resourceUI;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private CountdownTimer countdownTimer;

    private int tempGold = 0;
    private int tempSouls = 0;

    // Invoked when temporary gold/souls change
    public event Action<int> OnTempGoldChanged;
    public event Action<int> OnTempSoulsChanged;


    void Awake()
    {
        // Check only 1 ResourceManager instance exists and destroy duplicates
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddTempGold(int amount)
    {
        tempGold += amount;
        OnTempGoldChanged?.Invoke(tempGold);
    }

    public void AddTempSouls(int amount)
    {
        tempSouls += amount;
        OnTempSoulsChanged?.Invoke(tempSouls);
    }

    // Reset to 0 after temporary gold/souls is transferred to PlayerStats
    public void ResetTempGoldAndSouls()
    {
        tempGold = 0;
        OnTempGoldChanged?.Invoke(tempGold);
        tempSouls = 0;
        OnTempSoulsChanged?.Invoke(tempSouls);
    }

    // Calculate final reward then update reward UI and transfer to PlayerStats
    public void CalculateReward()
    {
        float multiplier = 1f + 0.25f * Mathf.Floor((float)countdownTimer.TotalTimeRun.TotalSeconds / 10f);

        int finalGold = Mathf.RoundToInt(tempGold * multiplier);
        int finalSouls = Mathf.RoundToInt(tempSouls * multiplier);

        resourceUI.UpdateRewardsUI(finalGold, finalSouls, multiplier, (int)countdownTimer.TotalTimeRun.TotalSeconds);
        playerStats.TransferCalculatedReward(finalGold, finalSouls);
    }
}
