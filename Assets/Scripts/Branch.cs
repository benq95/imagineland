using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.instance.healths--;
            print("playerhealths: " + GameManager.instance.healths);
            Destroy(gameObject);
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(50.0f, 50.0f), ForceMode2D.Impulse);
        }

        if (collision.gameObject.tag == "Terrainn") //TODO change to Terrain
        {
            Destroy(gameObject);
        }
    }
}
