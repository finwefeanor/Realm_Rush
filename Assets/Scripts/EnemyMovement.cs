using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    //List<CubeWaypoint> path;
    [SerializeField] float movingSpeed = 15f;

    void Start () 
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        var path = pathfinder.PathSize();
        StartCoroutine(PrintAllWayPoints(path)); // don't forget the reference if encounter a problem "path"
        //print("I am at start"); // third execution
    }

    // don't forget the reference if encounter a problem "List<CubeWaypoint> path"
    IEnumerator PrintAllWayPoints(List<CubeWaypoint> path) // to add coroutine change return type IEnumerator
    {
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
            yield return new WaitForSeconds(1.3f);
        }
        //print("Ending Patrol...");
    }

}
