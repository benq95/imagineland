using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class big_floor_of_death : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("gameover");
            GameObject.Find("Hero").GetComponent<FightScript>().Die();
        }
    }
}
