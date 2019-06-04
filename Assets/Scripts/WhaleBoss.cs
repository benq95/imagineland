using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WhaleBoss : DealDamageBase
{
    public Transform[] SpitPositions;
    public int LastSpitIndex = 0;
    public float SpitForce = 100.0f;

    public Transform[] DropPositions;

    public GameObject[] JunkToThrow;

    public GameObject MinionHorizontal;
    public GameObject MinionVertical;

    public Transform[] HorizontalPositions;
    public Transform[] VerticalPositions;

    public GameObject Player;

    private List<GameObject> _minions = new List<GameObject>(10);
    private bool _minionsCreated = false;

    public int HP = 3;
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

    private void Phase1()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(Roar(Phase1Attack));
        }
    }

    private void Phase1Attack()
    {
        if (_coroutine == null)
        {
            LastSpitIndex = (SpitPositions.Length + 1) / 2;
            _coroutine = StartCoroutine(AttackSequenceP1());
        }
    }

    private IEnumerator Roar(Action nextPhase)
    {
        _currentState = null;
        _animator.SetTrigger("Hit");
        yield return new WaitForSeconds(0.3f);
        Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-15, 0) * 20);
        yield return new WaitForSeconds(0.7f);
        _currentState = nextPhase;
        _timeCounter = 0.0f;
        _coroutine = null;
    }

    private IEnumerator AttackSequenceP1()
    {
        _currentState = null;        
        for(int i = 0; i < 5; i++)
        {
            _animator.SetTrigger("Spit");
            yield return new WaitForSeconds(0.2f);
            Spit();
            yield return new WaitForSeconds(3f);
        }
        _currentState = Phase1Attack;
        yield return new WaitForSeconds(3f);        
        _timeCounter = 0.0f;
        _coroutine = null;
    }

    private void Spit()
    {
        for (int i = 0; i < SpitPositions.Length; i++)
        {
            if (LastSpitIndex == i)
                continue;
            var axe = Instantiate(JunkToThrow[UnityEngine.Random.Range(0, JunkToThrow.Length)], SpitPositions[i].position, SpitPositions[i].rotation);
            axe.GetComponent<Rigidbody2D>().AddForce(axe.transform.right * SpitForce);
        }
        if(LastSpitIndex == 0)
        {
            LastSpitIndex++;
        }
        else if(LastSpitIndex == (SpitPositions.Length - 1))
        {
            LastSpitIndex--;
        }
        else
        {
            if(UnityEngine.Random.Range(0, 2) == 0)
                LastSpitIndex--;
            else
                LastSpitIndex++;
        }
    }

    private void Drop()
    {
        var positionIndex = UnityEngine.Random.Range(0, DropPositions.Length);
        var axe = Instantiate(JunkToThrow[UnityEngine.Random.Range(0, JunkToThrow.Length)], DropPositions[positionIndex].position, DropPositions[positionIndex].rotation);
        axe.GetComponent<Rigidbody2D>().AddForce(axe.transform.right * SpitForce);
    }

    private void Phase2()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(Roar(Phase2Attack));
        }
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

        if (!_minionsCreated)
        {
            MakeMinions(HorizontalPositions, MinionHorizontal);
            MakeMinions(VerticalPositions, MinionVertical);
            _minionsCreated = true;
        }
        yield return new WaitForSeconds(0.7f);
        for (int i = 0; i < 5; i++)
        {
            _animator.SetTrigger("Spit");
            yield return new WaitForSeconds(0.2f);
            Spit();
            yield return new WaitForSeconds(2.2f);
        }
        _currentState = Phase2Attack;
        yield return new WaitForSeconds(3f);
        _timeCounter = 0.0f;
        _coroutine = null;
    }

    private void MakeMinions(Transform[] positions, GameObject minion)
    {
        foreach(var pos in positions)
        {
            if(_minions.Count < _minions.Capacity)
                _minions.Add(Instantiate(minion, pos.position, pos.rotation));
        }
    }

    private void DestroyMinions()
    {
        for(int i = (_minions.Count - 1); i >= 0; i--)
        {
            if (_minions[i] != null)
                Destroy(_minions[i]);
        }
        _minions.Clear();
        _minionsCreated = false;
    }

    private void Phase3()
    {
        DestroyMinions();
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(Roar(Phase3Attack));
        }
    }

    private void Phase3Attack()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(AttackSequenceP3());
        }
    }

    private IEnumerator AttackSequenceP3()
    {
        _currentState = null;
        if (!_minionsCreated)
        {
            MakeMinions(HorizontalPositions, MinionHorizontal);
            MakeMinions(VerticalPositions, MinionVertical);
            _minionsCreated = true;
        }
        yield return new WaitForSeconds(0.7f);
        for (int i = 0; i < 5; i++)
        {
            _animator.SetTrigger("Spit");
            yield return new WaitForSeconds(0.2f);
            Spit();
            Drop();
            yield return new WaitForSeconds(1.5f);
        }
        _currentState = Phase3Attack;
        yield return new WaitForSeconds(3f);
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
        Debug.Log("Got hit");
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
