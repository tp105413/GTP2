using System;
using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private float stopAtPositionX = 0f;
    [SerializeField] private float runAwaySpeed = 0f;

    private RectTransform rectTransform;
    private Animator animator;

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
}
