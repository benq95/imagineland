using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    public float fieldForce = 50.0f;
    Rigidbody2D rigidbody2d;

    // Start is called before the first frame update
    void Awake()
    {
        
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(ProtectingForceField());
    }
    private IEnumerator ProtectingForceField()
    {
        bool IsSearching = true;
        // wait one frame because some explosions instantiate debris which should then
        // be pushed by physics force
        yield return new WaitForSeconds(2.0f);
        while (IsSearching)
        {

            float r = 5.0f;

            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, r);


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
                rb.AddExplosionForce(fieldForce, transform.position, r, 0, ForceMode2D.Impulse);
            }

            yield return new WaitForSeconds(0.05f);
        }

    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }
}
