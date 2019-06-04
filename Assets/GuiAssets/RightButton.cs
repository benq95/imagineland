﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightButton : MonoBehaviour
{
    private GameObject guiScript;

    // Start is called before the first frame update
    void Start()
    {
        guiScript = GameObject.Find("Canvas");
    }
    void OnMouseDown()
    {
        guiScript.GetComponent<GuiScript>().nextLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
