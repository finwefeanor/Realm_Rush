using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tower : MonoBehaviour {

    [SerializeField] Transform targetEnemy;
    Vector3 targetEnemyPos;
    [SerializeField] GameObject[] guns;

    public float towerRange;
    public string distanceInfo;
    float enemyDistance;

    void Update () 
    {           
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
