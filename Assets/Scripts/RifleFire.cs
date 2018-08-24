using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleFire : MonoBehaviour {

    public float fireDamage = 10f;
    public float rifleImpactForce;
    public float range = 100f;
    public float fireRate = 15f;

    public Camera fpsCam;
    Rigidbody rigidBody;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    void Update() {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    private void Shoot() 
    {
        muzzleFlash.Play();
        RaycastHit hit;

        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            print("Raycast detected object: " + hit.transform + hit.point);
            //rigidBody.AddForce(hit.normal) = hit.point;
            //rigidBody.AddForce(2f, 2f, 2f);

            EnemyTarget enemyTarget = hit.transform.GetComponent<EnemyTarget>();

            if (enemyTarget != null)
            {
                enemyTarget.KillDamage(fireDamage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * rifleImpactForce);
            }

            GameObject impactGameObject = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGameObject, 1.4f);
        }

    }
}
