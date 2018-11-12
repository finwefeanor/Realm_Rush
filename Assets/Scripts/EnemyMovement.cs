using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    [SerializeField] float movingSpeed = 15f;
    [SerializeField] float movingPeriod = 0.5f;
    public CubeWaypoint startWaypoint, endWaypoint;

    void Start () 
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        var path = pathfinder.PathSize(startWaypoint, endWaypoint);
        StartCoroutine(PrintAllWayPoints(path)); // don't forget the reference if encounter a problem "path"
    }

    // don't forget the reference if encounter a problem "List<CubeWaypoint> path"
    IEnumerator PrintAllWayPoints(List<CubeWaypoint> path) // to add coroutine change return type IEnumerator
    {
        WaitForSeconds delay = new WaitForSeconds(movingPeriod);
        //print("Starting Patrol..."); //firstly executed
        foreach (CubeWaypoint waypoint in path)
        {

            while (transform.position != waypoint.transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, waypoint.transform.position, movingSpeed * Time.deltaTime);
                yield return null;
            }
            ////print(element.name);
            //transform.position = waypoint.transform.position;
            ////print("Visiting block: " + waypoint); //second execution
            yield return delay;
        }
        //var fx = Instantiate(enemyHit.deathParticles, transform.position, Quaternion.identity);
        //fx.Play();

        //Destroy(gameObject);

        GetComponent<EnemyHit>().KillEnemy();
    }

}
