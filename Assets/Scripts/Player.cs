using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField][Range(0,10)]
    public float gravityModifier;
    
    [SerializeField][Range(1,10)]
    public float gravityAccModifier;

    private Vector2 _gravity;
    private Vector2 _velocity;
    private float _gravityAcceleration;

    void Start() {
        _gravity = Physics.gravity;
    }

    void Update() {

        // Gravity

        _gravityAcceleration += Time.deltaTime;
        _velocity = _gravity * (gravityModifier * Time.deltaTime);
        
        //Check Ground
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.5f);
        Debug.DrawRay(transform.position, -Vector2.up, Color.green );

        if (hit.collider != null) {
            
            if (hit.collider.CompareTag("Platform")) {
                _velocity.y = 0f;
            }
        }
        
        transform.Translate(_velocity * (_gravityAcceleration * gravityAccModifier));

    }
}
