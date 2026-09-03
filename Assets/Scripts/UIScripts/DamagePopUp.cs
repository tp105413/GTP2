using UnityEngine;

public class DamagePopUp : MonoBehaviour
{
    public Vector2 InitialVelocity;
    public Rigidbody2D rb;
    public float lifetime = 1.5f;


    void Start()
    {
        // Set the pop up starting velocity
        rb.linearVelocity = InitialVelocity;
        Destroy(gameObject, lifetime);
    }
}
