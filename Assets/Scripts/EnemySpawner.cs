using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour 
{
    [SerializeField] float spawnDelays;
    [SerializeField] EnemyMovement enemyGO;
    //[SerializeField] Transform enemyParentsTransform;

    void Start () 
    {
        StartCoroutine(SpawnEnemies());
	}
	
    IEnumerator SpawnEnemies() 
    {
        while (true)
        {
            //var spawningEnemies = 
                Instantiate(enemyGO, gameObject.transform.position, Quaternion.identity, this.transform);
            //spawningEnemies.transform.parent = enemyParentsTransform;
            //print(SpawnEnemies());
            yield return new WaitForSeconds(spawnDelays);
        }

    }
}
