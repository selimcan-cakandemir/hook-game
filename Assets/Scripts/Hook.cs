using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public static Hook Instance;
    
    public Transform player;
    public Transform hookParent;
    public float rotateSpeed;

    private List<Transform> hookPoints = new List<Transform>();
    private LineRenderer _lr;
    private SpringJoint2D _sj;
    private bool _flag = true;
    
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
        }
    }

    void Start()
    {
        _lr = GetComponent<LineRenderer>();
        foreach (Transform hook in hookParent)
        {
            hookPoints.Add(hook);
        }
        
        rb = player.GetComponent<Rigidbody2D>();
        sr = player.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            DrawRope();
            if (_flag)
            {
                MouseHeldDown();
            }
        }else if (Input.GetMouseButtonUp(0))
        {
            MouseLetGo();
        }
        
        Animate();
    }

    public void MouseHeldDown()
    {
        Transform nearestHook = NearestHookPoint(hookPoints);
        float distanceFromHook = Vector2.Distance(player.position, nearestHook.position);
        _sj = player.gameObject.AddComponent<SpringJoint2D>();
        _sj.autoConfigureConnectedAnchor = false;
        _sj.enableCollision = false;
        _sj.distance = distanceFromHook;
        _sj.dampingRatio = 10;
        _sj.connectedAnchor = nearestHook.position;
        Debug.Log(nearestHook.name);
        _flag = false;

    }

    void DrawRope()
    {
        Transform nearestHook = NearestHookPoint(hookPoints);
        _lr.SetPosition(0, player.position);
        _lr.SetPosition(1, nearestHook.position);
    }

    void MouseLetGo()
    {
        _lr.SetPosition(0, Vector3.zero);
        _lr.SetPosition(1, Vector3.zero);
        Destroy(player.GetComponent<SpringJoint2D>());
        _flag = true;
    }
    
    Vector2 GetPosition() 
    {
        Vector2 _playerPos = player.transform.localPosition;
        return _playerPos;
    }

    public Transform NearestHookPoint(List<Transform> hookPoints)
    {
        Transform nearHook = null;
        float minDist = Mathf.Infinity;
        Vector3 pos = player.position;
        foreach (Transform hook in hookPoints)
        {
            float distBetween = Vector2.Distance(hook.position, pos);
            if (distBetween < minDist)
            {
                nearHook = hook;
                minDist = distBetween;
            }
        }
        return nearHook;
    }

    void Animate()
    {
        Transform target = NearestHookPoint(hookPoints);
        // Vector2 targetDir = player.position - target.position;
        // float angle = Vector2.Angle(player.right,targetDir );
        // Debug.Log(angle);
        //
        // if (Math.Abs(angle - 90) < 5)
        // {
        //     Debug.Log("MIDDLE ANIMS");
        // }
        
        Vector3 vectorToTarget = target.position - player.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * (Mathf.Rad2Deg) - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        player.rotation = Quaternion.Slerp(player.rotation, q, Time.deltaTime * rotateSpeed);
    }
}

