using UnityEngine;

public class DamagePopUpTemp : MonoBehaviour
{
    [SerializeField] private GameObject DamagePopUpPrefab;
    [SerializeField] private Transform canvasTransform;


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
