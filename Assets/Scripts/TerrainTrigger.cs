using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTrigger : MonoBehaviour
{
    public bool IsActive = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsActive = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        IsActive = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsActive = false;
    }
}
