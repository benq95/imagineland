﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBranch : MonoBehaviour
{
    public GameObject Branch;
    public GameObject HealthMixture;
    public GameObject Bomb; 
    public float SpawnTime = 0.5f;

    float Width = 10.0f;
    float TimeToSpawnMixture;
    float TimeToSpawnBomb;
    float timer = 0;
    bool IsPlayerUnderBranches = false;
    Vector3 StartingPos = new Vector3(0,0,0);
    float StartRange, EndRange;
    

    // Start is called before the first frame update
    void Start()
    {
        int SpawnRandom = Random.Range(1, 5);
        TimeToSpawnMixture = SpawnTime * SpawnRandom;
        TimeToSpawnBomb = SpawnTime * (SpawnRandom + 1);

        StartingPos = transform.position - new Vector3(Width/2,0,0);
        float OffsetY = 1;
        StartingPos.y -= OffsetY;
        Width = transform.localScale.x;
        StartRange = transform.position.x - Width / 2;
        EndRange =   transform.position.x + Width / 2;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            IsPlayerUnderBranches = true;
            StartCoroutine(BranchSpawner());
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            IsPlayerUnderBranches = false;
            StopCoroutine(BranchSpawner());
        }
    }

    public IEnumerator BranchSpawner()
    {
        while (IsPlayerUnderBranches)
        {
            StartingPos = new Vector3(Random.Range(StartRange, EndRange), StartingPos.y, StartingPos.z);

            if (timer == TimeToSpawnMixture)
                Instantiate(HealthMixture, StartingPos, Quaternion.identity);
            else if (timer == TimeToSpawnBomb)
                Instantiate(Bomb, StartingPos, Quaternion.identity);
            else 
                Instantiate(Branch, StartingPos, Quaternion.identity);

            timer += SpawnTime;

            yield return new WaitForSeconds(SpawnTime);
        }
    }

}
