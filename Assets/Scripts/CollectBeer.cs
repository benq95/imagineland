using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBeer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Collectable")
        {
            Destroy(col.gameObject);
            GameManager.instance.points++;
        }
        
    }
}
