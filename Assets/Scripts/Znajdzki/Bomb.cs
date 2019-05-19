using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    float timer = 0;
    float ExplodeTime = 5.0f;
    bool IsPlaying = false;
    GameObject ChildGameObject;

    private void Awake()
    {
        ChildGameObject = gameObject.transform.GetChild(0).gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (!IsPlaying)
        {
            if (timer > ExplodeTime)
            {
                ChildGameObject.SetActive(true);
                GetComponentInChildren<ParticleSystem>().Play();
                IsPlaying = true;
            }

        }

    }
}
