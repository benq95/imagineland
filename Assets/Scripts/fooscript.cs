using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fooscript : MonoBehaviour
{
    GameObject curHook;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //void OnTriggerEnter2D(Collision2D col)
    //{
    //    int i = 0;
    //    //if (col.gameObject.name == "Player")
    //    //{
    //    //    this.GetComponent<HingeJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
    //    //    player1 = player.GetComponent<Rigidbody2D>();
    //    //}
    //}

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("X");
        int i = 0;
        if (col.gameObject.tag == "Player")
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<throwhook>().position = new Vector2( this.transform.position.x, this.transform.position.y);
            player.GetComponent<throwhook>().canHook = true;
        }
            
        //if (col.gameObject.name == "Player")
        //{
        //    this.GetComponent<HingeJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
        //    player1 = player.GetComponent<Rigidbody2D>();
        //}
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<throwhook>().canHook = false;
        }
    }
}
