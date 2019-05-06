using UnityEngine;
using System.Collections;

public class throwhook : MonoBehaviour {


	public GameObject hook;
    public Vector2 position;

	public bool ropeActive;
    public bool canHook;

	GameObject curHook;

	// Use this for initialization
	void Start () {
        canHook = false;
	}
	
	// Update is called once per frame
	void Update () {
	


		if (Input.GetKeyDown("space")&&this.canHook&& ropeActive == false) {


			
                //Vector2 destiny = Camera.main.ScreenToWorldPoint (Input.mousePosition);
                Vector2 destiny = this.position;


                curHook = (GameObject)Instantiate (hook, transform.position, Quaternion.identity);

				curHook.GetComponent<RopeScript> ().destiny = destiny;


				ropeActive = true;
			} else if(Input.GetKeyDown("space") && ropeActive == true)
        {

				//delete rope

				Destroy (curHook);


				ropeActive = false;

			}
		}


	}

