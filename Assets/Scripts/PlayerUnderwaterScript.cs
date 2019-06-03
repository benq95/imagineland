using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnderwaterScript : MonoBehaviour
{

    private Rigidbody2D rigidbody2D;
    private Vector2 directionVector;
    [SerializeField] private GameObject direction;
    [SerializeField] private int speed;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidbody2D.AddForce(Vector2.left*speed);
        } else if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidbody2D.AddForce(Vector2.right * speed);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            rigidbody2D.AddForce(Vector2.up * speed);
        } else if (Input.GetKey(KeyCode.DownArrow))
        {
            rigidbody2D.AddForce(Vector2.down*speed);
        }

    }
    

}
