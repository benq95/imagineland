using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBeer : MonoBehaviour {
    private AudioSource source;
    public AudioClip collectSound;
    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Collectable")
        {
            source.PlayOneShot(collectSound);
            Destroy(col.gameObject);
            GameManager.instance.points++;
        }
        
    }
}
