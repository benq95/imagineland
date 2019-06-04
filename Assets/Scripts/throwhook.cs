using UnityEngine;
using System.Collections;

public class throwhook : MonoBehaviour {


	public GameObject hook;
    public Vector2 position;

    public AudioClip throwhookSound;
    public AudioClip releasehookSound;
    private AudioSource source;

    public bool ropeActive;
    public bool canHook;

	GameObject curHook;

	// Use this for initialization
	void Start () {
        canHook = false;
        source = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	


		if (Input.GetKeyDown("space")&&this.canHook&& ropeActive == false) {

            source.PlayOneShot(throwhookSound);

            //Vector2 destiny = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            Vector2 destiny = this.position;


                curHook = (GameObject)Instantiate (hook, transform.position, Quaternion.identity);

				curHook.GetComponent<RopeScript> ().destiny = destiny;


				ropeActive = true;
			} else if(Input.GetKeyDown("space") && ropeActive == true)
        {

            source.PlayOneShot(releasehookSound);
                //delete rope

            Destroy (curHook);


				ropeActive = false;

			}
		}


	}

