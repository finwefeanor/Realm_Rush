using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tower : MonoBehaviour {

    // Parameters of each tower
    [SerializeField] GameObject[] guns;
    [SerializeField] float towerRange;

    // State of each tower
    Transform targetEnemy;


    float enemyDistance;
    float closestEnemyDistance;

    void Update () 
    {
        SetTargetEnemy();
        if (targetEnemy)
        {
            ShootEnemy();
        }
        else
        {
            FireControl(false);
        }

        //RaycastMethod();
    }

    private void SetTargetEnemy() {
        var sceneEnemies = FindObjectsOfType<EnemyHit>();
        if(sceneEnemies.Length == 0) { return; }

        Transform closestEnemy = sceneEnemies[0].transform;

        foreach (EnemyHit firstEnemy in sceneEnemies)
        {
            closestEnemy = GetClosestEnemy(closestEnemy, firstEnemy.transform);
        }
        targetEnemy = closestEnemy;
    }

    private Transform GetClosestEnemy(Transform transformA, Transform transformB) 
    {
        var distanceA = Vector3.Distance(transformA.transform.position, transform.position);
        var distanceB = Vector3.Distance(transformB.transform.position, transform.position);

        print("distance A: " + distanceA);
        print("distance B: " + distanceB);

        if (distanceA < distanceB)
        {
            return transformA;
        }

        return transformB;
    }

    private void ShootEnemy() 
    {
        enemyDistance = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);
        print("Distance between enemy and tower: " + enemyDistance);

        if (enemyDistance <= towerRange - 5)
        {
            transform.LookAt(targetEnemy);
            if (enemyDistance <= towerRange)
            {
                FireControl(true);
            }
        }
        
        else if (Input.GetButton("Jump") || Input.GetButton("Fire1"))
        {
            print("fire controls are working");
            FireControl(true);
        }
        else
        {
            FireControl(false);
        }
    }

    private void FireControl(bool isActive) 
    {

        foreach (GameObject gun in guns)
        {
            
            ParticleSystem particle = gun.GetComponentInChildren<ParticleSystem>();
            ParticleSystem.EmissionModule em = particle.emission;
            em.enabled = isActive;
        }
    }

    private void RaycastMethod() 
    {
        //---------------------------RAYCAST METHOD-----------------------------
        //Ray ray;
        //RaycastHit hit;
        //int layerMask = 1 << 8;
        //If we want to collide against everything except layer 8, The ~ operator does this, it inverts a bitmask.
        //layerMask = ~layerMask; 

        //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        //{
        //    print("Object's - distance " + hit.distance);

        //    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);

        //    if (targetEnemy != null && hit.distance <= hitDistanceSet)
        //    {
        //        FireAtWill(true);
        //    }
        //}
    }
}
