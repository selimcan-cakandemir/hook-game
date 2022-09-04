using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ReSharper disable All

public class Animate : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        sr.flipX = !(rb.velocity.x > 0);
    }
}
