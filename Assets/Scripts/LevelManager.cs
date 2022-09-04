using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject player;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(player.transform.position);

        if (pos.y < 0.0) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
