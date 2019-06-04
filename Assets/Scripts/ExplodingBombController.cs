using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBombController : MonoBehaviour
{
    public float DamageDistance = 3.0f;
    public float SelfExplodeTimer = 5.0f;
    public float BombExplosionTime = 1.0f;

    private Animator _animator = null;
    private Rigidbody2D _rigidbody = null;
    private BoxCollider2D _collider = null;
    private GameObject _player = null;
    private float _time;
    private Coroutine _explosionCoroutine = null;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        if (_time > SelfExplodeTimer)
        {
            if (_explosionCoroutine == null)
            {
                _explosionCoroutine = StartCoroutine(Explode());
            }
        }            
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (_explosionCoroutine == null)
            {
                _explosionCoroutine = StartCoroutine(Explode());
            }
        }
    }

    private IEnumerator Explode()
    {
        _animator.SetTrigger("Explode");
        Destroy(_collider);
        Destroy(_rigidbody);
        if(_player != null)
        {
            if(Vector3.Distance(transform.position, _player.transform.position) < DamageDistance)
            {
                _player.GetComponentInParent<FightScript>().DealDamage();
            }
        }
        yield return new WaitForSeconds(BombExplosionTime);
        _explosionCoroutine = null;
        Destroy(this.gameObject);
    }
}
