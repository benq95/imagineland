using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public TerrainTrigger FrontTerrainCollider;
    public float Speed = 2.0f;
    public float PlayerDetectionDistance = 20.0f;

    protected Action _currentState = null;

    void Start()
    {
        _currentState = IddleState;
    }

    void Update()
    {
        _currentState?.Invoke();
    }

    protected void IddleState()
    {
        if (FrontTerrainCollider.IsActive)
            transform.position -= transform.right * Time.deltaTime * Speed;
        else
            transform.Rotate(transform.up, 180, Space.Self);
        var rch = Physics2D.Raycast(transform.position.ToVec2(), -transform.right.ToVec2(), PlayerDetectionDistance, LayerMask.GetMask("Player"));
        if (rch.collider != null)
            _currentState = AttackState;
    }

    protected void AttackState()
    {
        Debug.Log("Attack!");
    }
}
