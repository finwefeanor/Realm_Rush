using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : MonoBehaviour {

    public float health = 50f;

    public void KillDamage (float amount) 
    {
        health -= amount; // or health = health - amount;

        if (health <= 0f)
        {
            Death();
        }
    }

    private void Death() 
    {
        Destroy(gameObject);
    }
}
