using System;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    [SerializeField] private ResourceUI resourceUI;
    [SerializeField] private PlayerStats playerStats;

    private int tempGold = 0;
    private int tempSouls = 0;

    // Event triggered when temporary gold/souls change
    public event Action<int> OnTempGoldChanged;
    public event Action<int> OnTempSoulsChanged;

    void Awake()
    {
        // Check only 1 Resource Manager instance exists and destroy duplicates
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

    // Calculate final reward then transfer to PlayerStats
    public void CalculateReward()
    {
        // ======calculate here=====
        int finalGold = tempGold;
        int finalSouls = tempSouls;

        playerStats.TransferCalculatedReward(finalGold, finalSouls);
    }
}
