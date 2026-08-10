using System;
using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private float stopAtPositionX = 0f;

    private RectTransform rectTransform;
    private bool hasReachedPlayer = false;

    public event Action OnReachedPlayer;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (hasReachedPlayer) return;

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

    public void SetSpawnPosition(Vector2 position)
    {
        rectTransform.anchoredPosition = position;
    }
}
