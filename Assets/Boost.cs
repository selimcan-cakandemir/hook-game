using System;
using UnityEngine;

public class Boost : MonoBehaviour
{
    public float speedBoostX;
    public float speedBoostY;

    private void OnTriggerEnter2D(Collider2D col)
    {
        col.transform.GetComponent<Rigidbody2D>().velocity = new Vector3(speedBoostX, speedBoostY, 0);
        Debug.Log("Test");
    }
}
