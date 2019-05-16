using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FightScript : MonoBehaviour

{
    private List<GameObject> enemiesUnderHit = new List<GameObject>();

    private bool attackPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Attack") && (!this.attackPressed))
        {
            this.attackPressed = true;
            Debug.Log("Attack");
            var enemiesToDestroy = new List<GameObject>();
            var enemiesToHit = enemiesUnderHit.Distinct().ToList();
            for (int i = 0; i < enemiesToHit.Count; i++)
            {
                var enemy = enemiesUnderHit[i];
                EnemyBase enemyComponent = enemy.GetComponent(typeof(EnemyBase)) as EnemyBase;
                if(enemyComponent == null)
                {
                    var minotaurComponent = enemy.GetComponent(typeof(MinotaurBoss)) as MinotaurBoss;
                    if (minotaurComponent.Damage())
                    {
                        //enemiesToDestroy.Add(enemy);
                    }
                    continue;
                }
                if(enemyComponent.Damage())
                {
                    enemiesToDestroy.Add(enemy);
                }
            }
            for(int i = 0; i < enemiesToDestroy.Count; i++)
            {
                var toDestroy = enemiesToDestroy[i];
                Destroy(toDestroy);
                if(this.enemiesUnderHit.Contains(toDestroy))
                {
                    this.enemiesUnderHit.Remove(toDestroy);
                }
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
