using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    public GameObject marked;
    public GameObject normal;
    public GameObject disabled;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseOver()
    {
        if (!disabled.GetComponent<Renderer>().enabled)
        {
            normal.GetComponent<Renderer>().enabled = false;
            marked.GetComponent<Renderer>().enabled = true;
        }

    }

    void OnMouseExit()
    {
        if (!disabled.GetComponent<Renderer>().enabled)
        {
            normal.GetComponent<Renderer>().enabled = true;
            marked.GetComponent<Renderer>().enabled = false;
        }
    }
}
