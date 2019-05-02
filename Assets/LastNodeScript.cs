using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastNodeScript : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D player1;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            this.GetComponent<HingeJoint2D>().connectedBody = null;
            //player1 = null;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.tag == "Player")
        {
            this.GetComponent<HingeJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
            player1 = player.GetComponent<Rigidbody2D>();
        }
    }
}
