﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExplosionForce : MonoBehaviour
{
    public float explosionForce = 4;


    private void Start()
    {
        StartCoroutine(Explode());    //Explode after 5 seconds
    }

    private IEnumerator Explode()
    {
        bool IsSearching = true;
        // wait one frame because some explosions instantiate debris which should then
        // be pushed by physics force
        yield return new WaitForSeconds(5.0f);
        while (IsSearching)
        {
            //explosionForce = Random.Range(-5, 5);
            //float multiplier = GetComponent<ParticleMultiplier>().multiplier;
            float r = 5.0f;
        
            Collider2D [] cols = Physics2D.OverlapCircleAll(transform.position, r);


            var rigidbodies = new List<Rigidbody2D>();
            foreach (var col in cols)
            {
                print(col.gameObject.name);
                
                if (col.attachedRigidbody != null && !rigidbodies.Contains(col.attachedRigidbody))
                {
                    rigidbodies.Add(col.attachedRigidbody);
                }
            }
            foreach (var rb in rigidbodies)
            {
                rb.AddExplosionForce(explosionForce, transform.position, r, 0, ForceMode2D.Impulse);
            }
            yield return new WaitForSeconds(100.0f); //TODO stop it someday
        }

    }


}
