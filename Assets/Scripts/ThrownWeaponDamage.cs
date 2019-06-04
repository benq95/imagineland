using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownWeaponDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponentInParent<FightScript>().DealDamage();
            Destroy(this.gameObject);
        }
        else if(collision.tag == "Terrain")
        {
            Destroy(this.gameObject);
        }
    }
}
