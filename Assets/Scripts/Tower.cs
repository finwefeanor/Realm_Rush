using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tower : MonoBehaviour {

    [SerializeField] Transform targetEnemy;
    Vector3 targetEnemyPos;
    [SerializeField] GameObject[] guns;

    RaycastHit hit;
    public float hitDistanceSet;
    public string distanceInfo;
    Ray ray;
    
    


    void Update () 
    {
        int layerMask = 1 << 8;
        //If we want to collide against everything except layer 8, The ~ operator does this, it inverts a bitmask.
        //layerMask = ~layerMask; 
        transform.LookAt(targetEnemy);
        //testvar = transform.TransformDirection(targetEnemyPos);
        //var testvar = targetEnemy.transform.position;
        //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))

        //hitDistance = hit.distance;
        distanceInfo = hit.distance.ToString();

        FireControl();

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            print("Object's - distance " + hit.distance);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);

            if (hit.distance <= hitDistanceSet)
            {
                FireAtWill(true);
            }
        }

    }

    private void FireControl() {

        if (Input.GetButton("Jump") || Input.GetButton("Fire1"))
        {
            FireAtWill(true);
        }
        else
        {
            FireAtWill(false);
        }
    }

    private void FireAtWill(bool isActive) {

        foreach (GameObject gun in guns)
        {
            ParticleSystem particle = gun.GetComponentInChildren<ParticleSystem>();
            ParticleSystem.EmissionModule em = particle.emission;
            em.enabled = isActive;
            
        }
    }
}
