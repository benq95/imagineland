using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUnderwaterFollow : MonoBehaviour
{

    public Transform player;
    public float cameraDistance = 30.0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(player.position.x, 0, player.position.z-10);
    }
}
