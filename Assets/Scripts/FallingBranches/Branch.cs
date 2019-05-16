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
            print(GameManager.instance.healths);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Terrain")
        {
            Destroy(gameObject);
        }
    }
}
