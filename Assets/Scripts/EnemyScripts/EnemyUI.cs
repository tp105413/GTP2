using System;
using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private float stopAtPositionX = 0f;

    private RectTransform rectTransform;
    private bool hasReachedPlayer = false;

    // Invoked when the player is reached
    public event Action OnReachedPlayer;


    void Awake()
    {
        // Get the RectTransform component attached to the same GameObject
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Stop updating once player is reached
        if (hasReachedPlayer) return;

        // Move left until the stop position is reached
        if(rectTransform.anchoredPosition.x > stopAtPositionX)
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
}
