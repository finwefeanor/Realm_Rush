using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour 
{
    [SerializeField] float spawnDelays;
    [SerializeField] EnemyMovement enemyGO;
    [SerializeField] Text spawnedEnemyCounter;
    [SerializeField] AudioClip spawnedEnemySFX;


    int enemyCounter = 0;
    //[SerializeField] Transform enemyParentsTransform;

    void Start () 
    {
        StartCoroutine(SpawnEnemies());
        spawnedEnemyCounter.text = enemyCounter.ToString();
        
    }
	
    IEnumerator SpawnEnemies() 
    {
        WaitForSeconds delay = new WaitForSeconds(spawnDelays);
        while (true)
        {
            var spawningEnemies = 
                Instantiate(enemyGO, gameObject.transform.position, Quaternion.identity, this.transform);
            //spawningEnemies.transform.parent = enemyParentsTransform;
            
            enemyCounter++; // todo if it matters move this above to "spawningEnemies"
            spawnedEnemyCounter.text = enemyCounter.ToString();
            GetComponent<AudioSource>().PlayOneShot(spawnedEnemySFX);
            print(enemyCounter);

            yield return delay;
        }

    }
}
