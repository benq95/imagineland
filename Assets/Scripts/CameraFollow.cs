using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform player;
    public float cameraDistance = 30.0f;

	// Use this for initialization
	void Awake () {
        GetComponent<Camera>().orthographicSize = (Screen.height / 2) / cameraDistance;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
	}
}
