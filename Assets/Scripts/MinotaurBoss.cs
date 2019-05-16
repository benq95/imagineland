using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MinotaurBoss : MonoBehaviour
{
    public Transform[] NeutralToVulnerablePosition;

    public Transform AxePosition1;
    public Transform AxePosition2;
    public Transform AxePosition3;

    public TerrainTrigger FloorCollider;
    public TerrainTrigger WallCollider;

    public GameObject Axe;
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
            if(FloorCollider.IsActive)
                _animator.SetTrigger("Land");
        }
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
        yield return new WaitForSeconds(1.7f);

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
        //transform.position = Vector3.Lerp(transform.position, NeutralPosition.position, 0.05f * _timeCounter);
        //if (transform.position == NeutralPosition.position)
        //{
        //    _currentState = Phase1Attack;
        //    _timeCounter = 0.0f;
        //}
    }

    private void Phase1Attack()
    {
        if(_coroutine == null)
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
        
        var axe = Instantiate(Axe);
        axe.transform.position = transform.position;
        axe.GetComponent<Rigidbody2D>().AddForce((Player.transform.position.ToVec2() - transform.position.ToVec2() + new Vector2(0.0f, 2.5f)) * 50);
    }

    private void Phase1Vulnerable()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(Movement(NeutralToVulnerablePosition, Phase1Return));
        };
        //transform.position = Vector3.Lerp(transform.position, VulnerablePosition.position, 0.05f * _timeCounter);
        //if (_timeCounter > 5.0f)
        //{
        //    _timeCounter = 0.0f;
        //    _currentState = Phase1;
        //}
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
        //transform.position = Vector3.Lerp(transform.position, NeutralPosition.position, 0.05f * _timeCounter);
        //if (transform.position == NeutralPosition.position)
        //{
        //    _currentState = Phase2Attack;
        //    _timeCounter = 0.0f;
        //}
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
        yield return new WaitForSeconds(0.5f);
        for(int i = 0; i < 3; i++)
        {
            List<int> positionList = new List<int> { 0, 1, 2 };
            for(int j = 0; j < 3; j++)
            {
                int index = 0;
                if(positionList.Count > 1)
                {
                    index = (int)UnityEngine.Random.Range(0.0f, (float)positionList.Count - 0.001f);
                }
                int positionIndex = positionList[index];
                _animator.SetTrigger("Throw");
                yield return new WaitForSeconds(0.5f);
                //Throw an axe at position
                switch (positionIndex)
                {
                    case 0:
                        ThrowAxeAtPosition(AxePosition1);
                        break;
                    case 1:
                        ThrowAxeAtPosition(AxePosition2);
                        break;
                    case 2:
                        ThrowAxeAtPosition(AxePosition3);
                        break;
                    default:
                        break;
                }
                positionList.RemoveAt(index);
                yield return new WaitForSeconds(1.0f);
            }
            yield return new WaitForSeconds(3);
        }
        _currentState = Phase2Vulnerable;
        _timeCounter = 0.0f;
        _coroutine = null;
    }

    private void ThrowAxeAtPosition(Transform position)
    {
        var axe = Instantiate(Axe);
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
        yield return new WaitForSeconds(1.31f);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        return;
        if (other.tag == "PlayerAttack")
        {
            if ((_lastHitTime + 2.0f) > Time.time)
                return;
            _lastHitTime = Time.time;
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
            _timeCounter = 0;
            
            //Take DMG, move to next phase
            switch (--HP)
            {
                case 2:
                    _currentState = Phase2;
                    break;
                case 1:
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
        }
        else if (other.tag == "Player")
        {
            //Deal DMG to player
        }
    }

    // return true if object is dead
    public bool Damage()
    {
        this.HP--;
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
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
                //Death?.Invoke();
                Destroy(this.gameObject);
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
