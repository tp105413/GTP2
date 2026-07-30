using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int playerLevel = 1;
    public int attackDamage = 10;
    public float attackInterval = 1f;

    public int gold = 0;
    public int souls = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddGold(int amount)
    {
        gold += amount;
    }

    public void AddSouls(int amount)
    {
        souls += amount;
    }
}
