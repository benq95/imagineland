using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FightScript : MonoBehaviour

{
    private AudioSource source;
    public AudioClip attackSound;

    private List<GameObject> enemiesUnderHit = new List<GameObject>();

    private bool attackPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Attack") && (!this.attackPressed))
        {
            source.PlayOneShot(attackSound);
            this.attackPressed = true;
            Debug.Log("Attack");
            var enemiesToHit = enemiesUnderHit.Distinct().ToList();
            Debug.Log(enemiesToHit.Count);
            for (int i = 0; i < enemiesToHit.Count; i++)
            {
                var enemy = enemiesUnderHit[i];
                DealDamageBase enemyComponent = enemy.GetComponentInParent<DealDamageBase>(); 
                if(enemyComponent == null)
                {
                    continue;
                }
                enemyComponent.Damage();
            }
        }
        else if(Input.GetButtonUp("Attack"))
        {
            this.attackPressed = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy enter fight collider.");
            enemiesUnderHit.Add(col.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy leave fight collider.");
            enemiesUnderHit.Remove(col.gameObject);
        }
    }
}
