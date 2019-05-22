using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    public Transform Player;
    public GameObject ForceFieldPrefab;
    public float DistanceToPlayer = 20.0f;

    float CurrentHealth = 100.0f;
    Rigidbody2D rigidbody2d;
    //Action CurrentState = null;
    bool CoroutineFinished = true;
    // Start is called before the first frame update
    void Start()
    {
        //CurrentState = Casting;
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (IsPlayerCloseEnough()) CurrentState?.Invoke();

        if (IsPlayerCloseEnough() && CoroutineFinished)
        {
            StartCoroutine(CastForceBalls());
            CoroutineFinished = false;
        }
    }

    //void Casting() {
    //    if (CoroutineFinished)
    //    {
    //        StartCoroutine(CastForceBalls());
    //    }
    //}
    bool IsPlayerCloseEnough()
    {
        if(Vector3.Distance(transform.position, Player.position) < DistanceToPlayer)
        {
            return true;
        }    
        return false;
    }

    public IEnumerator CastForceBalls()
    {
        bool CastBalls = true;

        while (CastBalls)
        {
            CastForceBall();

            yield return new WaitForSeconds(1.0f);
        }


    }
    void CastForceBall()
    {
        GameObject ForceFieldObject = Instantiate(ForceFieldPrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        print("casting force field");
        ForceField forceField = ForceFieldObject.GetComponent<ForceField>();
        forceField.Launch(Vector2.left, 3000);
    }
    
    Vector3 FindDirectionTowardsPlayer()
    {
        Vector3 dir = transform.forward;


        
        return dir;
    }


}
