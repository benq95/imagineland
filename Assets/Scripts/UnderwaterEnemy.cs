using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderwaterEnemy : DealDamageBase
{
    public TerrainTrigger WallCollider;
    public float Speed = 2.0f;
    public float PlayerDetectionDistance = 2.0f;
    public float AttackWindup = 0.5f;
    public float AttackTime = 0.2f;
    public float AttackCooldown = 0.3f;
    public int Health = 3;
    public float MinimalTimeInOneDirection = 5.0f;
    public float MaximalTimeInOneDirection = 8.0f;
    public Vector3 MovementDirection = new Vector3(1.0f, 0.0f, 0.0f);
    public bool FlipSpriteOnDirectionChange = true;

    protected Action _currentState = null;
    public float _timeCounter = 0.0f;
    private Coroutine _coroutine = null;

    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("Walk", true);
        _currentState = IddleState;
    }

    void Update()
    {
        _timeCounter += Time.deltaTime;
        _currentState?.Invoke();
    }

    // return true if object is dead
    public override bool Damage()
    {
        this.Health--;
        if (Health <= 0)
        {
            Destroy(this.gameObject);
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
            _timeCounter = 0.0f;
            _currentState = AttackState;
            return;
        }
        if (!WallCollider.IsActive && (UnityEngine.Random.Range(MinimalTimeInOneDirection, MaximalTimeInOneDirection) > _timeCounter))
        {
            transform.position += MovementDirection * Time.deltaTime * Speed;
        }
        else
        {
            if(FlipSpriteOnDirectionChange)
                transform.Rotate(transform.up * 180);
            MovementDirection = -MovementDirection;
            _timeCounter = 0.0f;
        }
    }

    protected void AttackState()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(Attack());
        };
    }

    private IEnumerator Attack()
    {
        _currentState = null;
        _animator.SetBool("Walk", false);
        _animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.3f);
        var rch = Physics2D.Raycast(transform.position.ToVec2(), transform.right.ToVec2(), PlayerDetectionDistance, LayerMask.GetMask("Player"));
        Debug.DrawRay(transform.position, (transform.right * PlayerDetectionDistance));
        if (rch.collider != null)
        {
            rch.collider.GetComponentInParent<FightScript>().DealDamage();
        }
        yield return new WaitForSeconds(1.01f);

        _currentState = IddleState;
        _animator.SetBool("Walk", true);
        _coroutine = null;
    }
}
