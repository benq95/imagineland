﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public TerrainTrigger FloorCollider;
    public TerrainTrigger WallCollider;
    public float Speed = 2.0f;
    public float PlayerDetectionDistance = 2.0f;
    public float AttackWindup = 0.5f;
    public float AttackTime = 0.2f;
    public float AttackCooldown = 0.3f;
    public int Health = 3;

    protected Action _currentState = null;
    private float _attackTimeCounter = 0.0f;

    void Start()
    {
        _currentState = IddleState;
    }

    void Update()
    {
        _attackTimeCounter += Time.deltaTime;
        _currentState?.Invoke();
    }

    // return true if object is dead
    public bool Damage()
    {
        this.Health--;
        if(Health <= 0)
        {
            return true;
        }
        return false;
    }

    protected void IddleState()
    {
        var rch = Physics2D.Raycast(transform.position.ToVec2(), transform.right.ToVec2(), PlayerDetectionDistance, LayerMask.GetMask("Player"));
        Debug.DrawRay(transform.position, (transform.right * PlayerDetectionDistance));
        if (rch.collider != null)
        {
            _attackTimeCounter = 0.0f;
            _currentState = AttackState;            
            return;
        }
        if (FloorCollider.IsActive && !WallCollider.IsActive)
            transform.position += transform.right * Time.deltaTime * Speed;
        else
            transform.Rotate(transform.up * 180);        
    }

    protected void AttackState()
    {
        if (_attackTimeCounter > (AttackCooldown + AttackTime + AttackWindup))
            //Return to "normal" state
            _currentState = IddleState;
        else if (_attackTimeCounter > (AttackTime + AttackWindup))
        {
            //Attack cooldown
        }
        else if (_attackTimeCounter > AttackWindup)
        {
            //Attack happening
        }
        else
        {
            //Attack windup
        }
    }
}
