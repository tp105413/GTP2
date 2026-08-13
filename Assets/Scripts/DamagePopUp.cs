using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private float floatTime = 0.5f;

    public void Setup(int damageAmount)
    {
        damageText.text = damageAmount.ToString();
        Destroy(gameObject, floatTime);
    }
}