using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;

    [SerializeField] private float xOffset;
    
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x + xOffset,0, -10);
    }
}
