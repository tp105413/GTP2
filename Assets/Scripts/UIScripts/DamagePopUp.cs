using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{
    public Vector2 InitialVelocity;
    public Rigidbody2D rb;
    public float lifetime = 1.5f;

    void Start()
    {
        rb.linearVelocity = InitialVelocity;
        Destroy(gameObject, lifetime);
    }
}
