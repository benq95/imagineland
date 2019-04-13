using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurBoss : MonoBehaviour
{
    public Transform NeutralPosition;
    public Transform VulnerablePosition;

    public Transform AxePosition1;
    public Transform AxePosition2;
    public Transform AxePosition3;

    public GameObject Axe;
    public GameObject Player;

    public int HP = 3;
    public event Action Death;

    private Action _currentState = null;
    private float _timeCounter = 0.0f;
    private Coroutine _attackCoroutine = null;


    // Start is called before the first frame update
    void Start()
    {
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
        transform.position = Vector3.Lerp(transform.position, NeutralPosition.position, 0.05f * _timeCounter);
        if (transform.position == NeutralPosition.position)
        {
            _currentState = Phase1Attack;
            _timeCounter = 0.0f;
        }
    }

    private void Phase1Attack()
    {
        if(_attackCoroutine == null)
        {
            _attackCoroutine = StartCoroutine(AttackSequenceP1());
        }
    }

    private IEnumerator AttackSequenceP1()
    {
        _currentState = null;
        yield return new WaitForSeconds(1);
        ThrowAxeAtPlayer();
        yield return new WaitForSeconds(2);
        ThrowAxeAtPlayer();
        yield return new WaitForSeconds(2);
        ThrowAxeAtPlayer();
        yield return new WaitForSeconds(1);
        _currentState = Phase1Vulnerable;
        _timeCounter = 0.0f;
        _attackCoroutine = null;
    }

    private void ThrowAxeAtPlayer()
    {
        var axe = Instantiate(Axe);
        axe.transform.position = transform.position;
        axe.GetComponent<Rigidbody2D>().AddForce((Player.transform.position.ToVec2() - transform.position.ToVec2() + new Vector2(0.0f, 2.5f)) * 50);
    }

    private void Phase1Vulnerable()
    {
        transform.position = Vector3.Lerp(transform.position, VulnerablePosition.position, 0.05f * _timeCounter);
        if (_timeCounter > 5.0f)
        {
            _timeCounter = 0.0f;
            _currentState = Phase1;
        }
    }

    private void Phase2()
    {

    }

    private void Phase3()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "PlayerAttack")
        {
            if(_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
                _attackCoroutine = null;
            }
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
                    Death?.Invoke();
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
}
