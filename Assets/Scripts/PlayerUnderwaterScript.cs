using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnderwaterScript : MonoBehaviour
{

    private Rigidbody2D rigidbody2D;
    private Vector2 directionVector;
    [SerializeField] private GameObject direction;

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
            this.transform.Rotate(0, 0, 2);
        } else if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Rotate(0, 0, -2);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            directionVector = direction.transform.position - this.transform.position;
            directionVector.Normalize();
            rigidbody2D.AddForce(directionVector);
        } else if (Input.GetKey(KeyCode.DownArrow))
        {
            directionVector = this.transform.position - direction.transform.position;
            directionVector.Normalize();
            rigidbody2D.AddForce(directionVector);
        }

    }
    

}
