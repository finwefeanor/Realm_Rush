using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour 
{
    [SerializeField] float spawnDelays;
    [SerializeField] EnemyMovement enemyGO;

    void Start () 
    {
        StartCoroutine(SpawnEnemies());
	}
	
    IEnumerator SpawnEnemies() 
    {
        while (true)
        {
            Instantiate(enemyGO, gameObject.transform.position, Quaternion.identity);
            print(SpawnEnemies());
            yield return new WaitForSeconds(spawnDelays);
        }

    }
}
