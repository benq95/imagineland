using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightScript : MonoBehaviour

{
    private List<GameObject> enemiesUnderHit = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Attack"))
        {
            Debug.Log("Attack");
            var enemiesToDestroy = new List<GameObject>();
            foreach(var enemy in enemiesUnderHit)
            {
                EnemyBase enemyComponent = enemy.GetComponent(typeof(EnemyBase)) as EnemyBase;
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
