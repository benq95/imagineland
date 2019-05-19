using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExplosionForce : MonoBehaviour
{
    public float explosionForce = 4;


    private void Start()
    {
        StartCoroutine(Explode());
        //Explode();
    }
    private IEnumerator Explode()
    {
        bool IsSearching = true;
        // wait one frame because some explosions instantiate debris which should then
        // be pushed by physics force
        yield return new WaitForSeconds(2.0f);
        while (IsSearching)
        {
            //explosionForce = Random.Range(-5, 5);
            //float multiplier = GetComponent<ParticleMultiplier>().multiplier;
            float r = 10.0f;
            //var cols = Physics2D.OverlapCircle(transform.position, r);
            //List<Collider2D> cols = new List<Collider2D>();
            //ContactFilter2D filter = new ContactFilter2D().NoFilter();
            //int colsNum = Physics2D.OverlapCircle(transform.position, r, filter, cols); //ni dziala

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

                rb.AddForce(new Vector3(explosionForce, 0, 0), ForceMode2D.Impulse);
                //rb.AddExplosionForce(explosionForce * 5.0f, transform.position + new Vector3(explosionForce, explosionForce, 0), r, 1 * 5.0f, ForceMode2D.Impulse);

            }

            yield return new WaitForSeconds(0.5f);
        }

    }
}
