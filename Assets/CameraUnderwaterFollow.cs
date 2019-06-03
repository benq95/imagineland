using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUnderwaterFollow : MonoBehaviour
{

    public Transform player;
    public Transform leftBound;
    public Transform rightBound;
    public float cameraDistance = 30.0f;
    private bool locked = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!locked)
        {
            if (player.position.x > rightBound.position.x)
            {
                transform.position = new Vector3(transform.position.x + (player.position.x- rightBound.position.x), 0, transform.position.z);
            } else if(player.position.x < leftBound.position.x)
            {
                transform.position = new Vector3(transform.position.x - (leftBound.position.x - player.position.x), 0, transform.position.z);
            }
            //transform.position = new Vector3(player.position.x, 0, player.position.z - 10);
        }
    }

    public void lockCamera()
    {
        locked = true;
    }
}
