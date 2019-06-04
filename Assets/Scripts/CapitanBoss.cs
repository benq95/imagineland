using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CapitanBoss : DealDamageBase
{
    public Transform[] NeutralToVulnerablePosition;

    public Transform PirateSpawnPosition1;
    public Transform PirateSpawnPosition2;

    public TerrainTrigger FloorCollider;
    public TerrainTrigger WallCollider;

    public GameObject Bomb;
    public GameObject Pirate;
    public GameObject Player;

    public int HP = 3;
    public float PlayerDetectionDistance = 3.0f;
    public float Speed = 8.0f;
    public float EnemyInvincibleTime = 2.0f;

    private Action _currentState = null;
    private float _timeCounter = 0.0f;
    private Coroutine _coroutine = null;
    private Animator _animator;

    private float _lastHitTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _currentState = Phase1;
    }

    // Update is called once per frame
    void Update()
    {
        _timeCounter += Time.deltaTime;
        _currentState?.Invoke();
    }

    private IEnumerator Movement(Transform[] transforms, Action endingDelegate)
    {
        _currentState = null;
        _animator.SetTrigger("Jump");
        for (int i = 0; i < (transforms.Length - 1); i++)
        {
            var transitionTime = 0.0f;
            while (transitionTime < 1)
            {
                transform.position = Vector3.Lerp(transforms[i].position, transforms[i + 1].position, transitionTime);
                yield return new WaitForSeconds(0.01f);
                transitionTime += Time.deltaTime * Speed;
            }
        }
        _animator.SetTrigger("Land");
        yield return new WaitForSeconds(2.0f);
        if (endingDelegate != null)
            _currentState = endingDelegate;
        _timeCounter = 0.0f;
        _coroutine = null;
    }

    private IEnumerator MoveToStartPosition(Transform startPos, Action endingDelegate)
    {
        _currentState = null;
        transform.position = startPos.transform.position;
        _animator.SetTrigger("Cheer");
        yield return new WaitForSeconds(0.3f);
        Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-15, 4) * 30);
        yield return new WaitForSeconds(1.0f);

        if (endingDelegate != null)
        {
            _currentState = endingDelegate;
            _timeCounter = 0.0f;
            _coroutine = null;
        }
    }

    private void Phase1()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(MoveToStartPosition(NeutralToVulnerablePosition[0], Phase1Attack));
        };
    }

    private void Phase1Attack()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(AttackSequenceP1());
        }
    }

    private IEnumerator AttackSequenceP1()
    {
        _currentState = null;
        yield return new WaitForSeconds(0.5f);
        _animator.SetTrigger("Throw");
        yield return new WaitForSeconds(0.5f);
        ThrowAxeAtPlayer();
        yield return new WaitForSeconds(1.5f);
        _animator.SetTrigger("Throw");
        yield return new WaitForSeconds(0.5f);
        ThrowAxeAtPlayer();
        yield return new WaitForSeconds(1.5f);
        _animator.SetTrigger("Throw");
        yield return new WaitForSeconds(0.5f);
        ThrowAxeAtPlayer();
        yield return new WaitForSeconds(1);
        _currentState = Phase1Vulnerable;
        _timeCounter = 0.0f;
        _coroutine = null;
    }

    private void ThrowAxeAtPlayer()
    {

        var axe = Instantiate(Bomb);
        axe.transform.position = transform.position;
        axe.GetComponent<Rigidbody2D>().AddForce((Player.transform.position.ToVec2() - transform.position.ToVec2() + new Vector2(0.0f, 2.5f)) * 50);
    }

    private void Phase1Vulnerable()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(Movement(NeutralToVulnerablePosition, Phase1Return));
        };
    }

    private void Phase1Return()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(Movement(NeutralToVulnerablePosition.Reverse().ToArray(), Phase1Attack));
        };
    }

    private void Phase2()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(MoveToStartPosition(NeutralToVulnerablePosition[0], Phase2Attack));
        };
    }

    private void Phase2Attack()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(AttackSequenceP2());
        }
    }

    private IEnumerator AttackSequenceP2()
    {
        _currentState = null;
        _animator.SetTrigger("Cheer");
        yield return new WaitForSeconds(0.5f);
        GameObject pirate1 = Instantiate(Pirate, PirateSpawnPosition1.position, transform.rotation);
        GameObject pirate2 = Instantiate(Pirate, PirateSpawnPosition2.position, transform.rotation);
        while (true)
        {
            if ((pirate1 == null) && (pirate2 == null))
                break;
            yield return new WaitForSeconds(0.5f);
        }
        _currentState = Phase2Vulnerable;
        _timeCounter = 0.0f;
        _coroutine = null;
    }

    private void ThrowAtPosition(Transform position)
    {
        var axe = Instantiate(Bomb);
        axe.transform.position = transform.position;
        axe.GetComponent<Rigidbody2D>().AddForce((position.position.ToVec2() - transform.position.ToVec2() + new Vector2(0.0f, 9f)) * 25);
    }

    private void Phase2Vulnerable()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(Movement(NeutralToVulnerablePosition, Phase2Return));
        };
    }

    private void Phase2Return()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(Movement(NeutralToVulnerablePosition.Reverse().ToArray(), Phase2));
        };
    }

    private void Phase3()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(MoveToStartPosition(NeutralToVulnerablePosition[NeutralToVulnerablePosition.Length - 1], Phase3Attack));
        };
    }

    private void Phase3Attack()
    {
        _animator.SetBool("Walk", true);
        var origin = transform.position;
        origin.y -= 1;
        var rch = Physics2D.Raycast(origin.ToVec2(), transform.right.ToVec2(), PlayerDetectionDistance, LayerMask.GetMask("Player"));
        Debug.DrawRay(origin, (transform.right * PlayerDetectionDistance));
        if (rch.collider != null)
        {
            if (_coroutine == null)
                _coroutine = StartCoroutine(AttackSequenceP3());
            return;
        }
        if (FloorCollider.IsActive && !WallCollider.IsActive)
            transform.position += transform.right * Time.deltaTime * Speed;
        else
            transform.Rotate(transform.up * 180);
    }

    private IEnumerator AttackSequenceP3()
    {
        _currentState = null;
        _animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.3f);
        var rch = Physics2D.Raycast(transform.position.ToVec2(), transform.right.ToVec2(), PlayerDetectionDistance, LayerMask.GetMask("Player"));
        Debug.DrawRay(transform.position, (transform.right * PlayerDetectionDistance));
        if (rch.collider != null)
        {
            rch.collider.GetComponentInParent<FightScript>().DealDamage();
        }
        yield return new WaitForSeconds(1.01f);
        //DEAL DMG TO PLAYER
        _currentState = Phase3Attack;
        _timeCounter = 0.0f;
        _coroutine = null;
    }

    private void Death()
    {
        _currentState = null;
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        _coroutine = StartCoroutine(DeathCorutine());
    }

    private IEnumerator DeathCorutine()
    {
        _animator.SetTrigger("Death");
        yield return new WaitForSeconds(5f);
        _coroutine = null;
        Destroy(this.gameObject);
    }

    // return true if object is dead
    public override bool Damage()
    {
        this.HP--;
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        _timeCounter = 0;
        //Take DMG, move to next phase
        switch (HP)
        {
            case 2:
                _currentState = Phase2;
                break;
            case 1:
                _animator.SetBool("Walk", true);
                _currentState = Phase3;
                break;
            case 0:
                GetComponent<Collider2D>().enabled = false;
                Death();
                break;
            default:
                _currentState = Phase3;
                break;
        }
        if (HP <= 0)
        {
            return true;
        }
        return false;
    }
}
