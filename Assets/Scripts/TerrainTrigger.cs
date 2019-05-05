﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTrigger : MonoBehaviour
{
    public bool IsActive = false;
    private int _enteredTerrains = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Terrain")
        {
            IsActive = true;
            _enteredTerrains++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Terrain")
            _enteredTerrains--;
        if (_enteredTerrains == 0)
            IsActive = false;
    }
}
