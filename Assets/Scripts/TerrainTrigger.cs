using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTrigger : MonoBehaviour
{
    public bool IsActive = false;
    private int _enteredTerrains = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag != "Player") && (collision.tag != "Junk"))
        {
            IsActive = true;
            _enteredTerrains++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.tag != "Player") && (collision.tag != "Junk"))
            _enteredTerrains--;
        if (_enteredTerrains == 0)
            IsActive = false;
    }
}
