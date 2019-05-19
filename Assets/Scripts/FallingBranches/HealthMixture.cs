using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMixture : MonoBehaviour
{
    float timer = 0;
    float destroyTime = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > destroyTime) Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.instance.healths++;
            print("playerhealths: "+ GameManager.instance.healths);
            Destroy(gameObject);
        }   
    }

}
