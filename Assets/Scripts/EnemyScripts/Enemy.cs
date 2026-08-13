using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PlayerCombat playerCombat;
    [SerializeField] private EnemyUI enemyUI;
    [SerializeField] private GameObject DamagePopUpPrefab;
    [SerializeField] private Transform canvasTransform;
    [SerializeField] private Image Healthbar;
    [SerializeField] private float HealthAmount;

    private CountdownTimer countdownTimer;

    private int maxHealth;
    private int currentHealth;
    private int goldDrop = 0;
    private int soulsDrop = 0;

    public void Initialize(PlayerCombat combat, CountdownTimer timer, int level, string type)
    {
        playerCombat = combat;
        countdownTimer = timer;
        countdownTimer.OnTimerStop += HandleTimerStop;

        CalculateHealth(level, type);
        CalculateGoldDrop(level, type);
        CalcualateSoulsDrop(level, type);
        Debug.Log(type + " Health: " + currentHealth);
    }

    void Awake()
    {
        if(enemyUI == null)
        {
            enemyUI = GetComponent<EnemyUI>();
            // subscribe function to action, when action is invoked will run the function
            enemyUI.OnReachedPlayer += HandleReachedPlayer;
        }
    }

    // unsubscribe automatically
    private void OnDestroy()
    {
        if (enemyUI != null) enemyUI.OnReachedPlayer -= HandleReachedPlayer;
        if(countdownTimer != null) countdownTimer.OnTimerStop -= HandleTimerStop;
    }

    private void CalculateHealth(int level, string type)
    {
        // will add in formula, current just for testing
        if (type == "Enemy")
        {
            maxHealth = 50 + (level - 1) * 10;
            currentHealth = maxHealth;
        }
        else if(type == "Boss")
        {
            maxHealth = 100 + (level - 1) * 10;
            currentHealth = maxHealth;
        }
    }

    private int CalculateGoldDrop(int level, string type)
    {
        // temporary formula will be changed in future
        if (type == "Enemy")
        {
            goldDrop = 10 * level;
        }
        else if (type == "Boss")
        {
            goldDrop = 30 * level;
        }
        return goldDrop;
    }

    private int CalcualateSoulsDrop(int level, string type)
    {
        // temporary formula will be changed in future
        if (type == "Enemy")
        {
            soulsDrop = 0;
        }
        else if (type == "Boss")
        {
            soulsDrop = 1 * level;
        }
        return soulsDrop;
    }

    // need clarify code logic
    public void TakeDamage(int damage, PlayerStats stats)
    {
        playerStats = stats;

        Debug.Log("Enemy is taking " + damage + " damage!");
        currentHealth -= damage;
        //Healthbar.fillAmount = HealthAmount / 100f; // health component

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Enemy dead!");
        DropReward();
        GameManager.Instance.EndAttack();
        Destroy(gameObject);
        GameManager.Instance.EnemyDefeated();
    }

    public void RunAway()
    {
        Debug.Log("Enemy run away!");
        GameManager.Instance.EndAttack();
        Destroy(gameObject);
    }

    public void DropReward()
    {
        playerStats.AddGold(goldDrop);
        playerStats.AddSouls(soulsDrop);
    }

    private void HandleReachedPlayer()
    {
        GameManager.Instance.StartAttack();
        playerCombat.SetTarget(this);
    }

    private void HandleTimerStop()
    {
        RunAway();
    }

    private void DamageUI(int damageToShow)
    {
        if (canvasTransform == null)
        {
            GameObject mainCanvas = GameObject.Find("Canvas");
            if (mainCanvas != null)
            {
                canvasTransform = mainCanvas.transform;
            }
        }

        if (DamagePopUpPrefab == null || canvasTransform == null) return;


        GameObject PopUp = Instantiate(DamagePopUpPrefab, canvasTransform);


        Camera uiCamera = Camera.main;
        if (uiCamera != null)
        {
            Vector3 screenPos = uiCamera.WorldToScreenPoint(transform.position + Vector3.up * 2f);
            RectTransform canvasRect = canvasTransform.GetComponent<RectTransform>();
            RectTransform popupRect = PopUp.GetComponent<RectTransform>();

            if (canvasRect != null && popupRect != null)
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    canvasRect,
                    screenPos,
                    uiCamera,
                    out Vector2 localPoint
                );
                popupRect.anchoredPosition = localPoint;
            }
        }


        DamagePopup popup = PopUp.GetComponent<DamagePopup>();

        if (popup != null)
        {
            popup.Setup(damageToShow);
        }

    }
}
