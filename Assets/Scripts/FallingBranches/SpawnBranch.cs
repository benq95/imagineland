using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBranch : MonoBehaviour
{
    public GameObject Branch;
    public GameObject HealthMixture;

    public float Width = 10.0f;
    public float SpawnTime = 0.5f;

    float TimeToSpawnMixture = 1.0f;
    float Time = 0;
    bool IsPlayerUnderBranches = false;
    Vector3 StartingPos = new Vector3(0,0,0);
    float StartRange, EndRange;
    

    // Start is called before the first frame update
    void Start()
    {
        Time = 0;
        StartingPos = transform.position - new Vector3(Width/2,0,0);
        StartRange = transform.position.x - Width / 2;
        EndRange =   transform.position.x + Width / 2;
        StartCoroutine(BranchSpawner());
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            IsPlayerUnderBranches = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            IsPlayerUnderBranches = false;
        }
    }

    public IEnumerator BranchSpawner()
    {
        bool spawn = true;

        while (spawn)
        {
            StartingPos = new Vector3(Random.Range(StartRange, EndRange), StartingPos.y, StartingPos.z);
            if (IsPlayerUnderBranches)
            {
                if(Time == TimeToSpawnMixture)
                    Instantiate(HealthMixture, StartingPos, Quaternion.identity);
                else
                    Instantiate(Branch, StartingPos, Quaternion.identity);

                Time += SpawnTime;
                yield return new WaitForSeconds(SpawnTime);
            }
        }
    }

}
