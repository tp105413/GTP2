using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private float stopAtPositionX = 0f;
    [SerializeField] private float runAwaySpeed = 0f;
    [SerializeField] Slider healthBarSlider;

    private RectTransform rectTransform;
    private Animator animator;
    private float currentHealth = 0f;
    private float maxHealth = 0f;

    private bool hasReachedPlayer = false;
    private bool isRunningAway = false;

    // Invoked when the player is reached
    public event Action OnReachedPlayer;


    void Awake()
    {
        // Get the RectTransform and Animator component attached to the same GameObject
        rectTransform = GetComponent<RectTransform>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        healthBarSlider.value = currentHealth / maxHealth;

        // Stop updating once player is reached
        if (hasReachedPlayer) return;

        if (isRunningAway)
        {
            // Move right if run away state is true
            rectTransform.anchoredPosition += Vector2.right * runAwaySpeed * Time.deltaTime;
        }
        // Move left until the stop position is reached
        else if(rectTransform.anchoredPosition.x > stopAtPositionX)
        {
            rectTransform.anchoredPosition += Vector2.left * GameManager.Instance.worldMoveSpeed * Time.deltaTime;
        }
        else
        {
            Vector2 position = rectTransform.anchoredPosition;
            position.x = stopAtPositionX;
            rectTransform.anchoredPosition = position;

            hasReachedPlayer = true;
            // ?. checks only call if action isnt null
            OnReachedPlayer?.Invoke();
        }
    }

    // Set starting position of the object
    public void SetSpawnPosition(Vector2 position)
    {
        rectTransform.anchoredPosition = position;
    }

    // Set enemy to run away state and start animation
    public void RunAway()
    {
        hasReachedPlayer = false;
        isRunningAway = true;
        animator.SetBool("RunAway", true);
    }

    // Set health for health bar UI
    public void SetHealthUI(int amount)
    {
        currentHealth = amount;
        maxHealth = amount;
    }

    // Update enemy health UI after taking dmg
    public void UpdateHealthUI(int amount)
    {
        currentHealth -= amount;
    }
}
