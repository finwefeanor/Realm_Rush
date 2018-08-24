using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour 
    {
    [SerializeField] float secondsBetweenSpawns;
    [SerializeField] EnemyMovement enemyGO;

    // Use this for initialization
    void Start () 
    {
        //Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        //var path = pathfinder.PathSize();
        StartCoroutine(SpawnEnemies());
	}
	
    IEnumerator SpawnEnemies() 
    {
        print(SpawnEnemies());
        yield return new WaitForSeconds(secondsBetweenSpawns);
        //foreach (CubeWaypoint waypoint in path)
        //{
        //    Instantiate(enemyGO, enemyGO.transform.position, Quaternion.LookRotation(enemyGO.transform.forward,
        //        enemyGO.transform.up));
        //    yield return new WaitForSeconds(secondsBetweenSpawns);
        //}
    }
}
