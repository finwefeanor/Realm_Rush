using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    List<CubeWaypoint> path;

    void Start () 
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        path = pathfinder.PathSize();
        StartCoroutine(PrintAllWayPoints()); // don't forget the reference if encounter a problem "path"
        print("I am at start"); // third execution
    }

    // don't forget the reference if encounter a problem "List<CubeWaypoint> path"
    IEnumerator PrintAllWayPoints() // to add coroutine change return type IEnumerator
    {
        print("Starting Patrol..."); //firstly executed
        foreach (CubeWaypoint waypoint in path)
        {
            //print(element.name);
            transform.position = waypoint.transform.position;
            //print("Visiting block: " + waypoint); //second execution
            yield return new WaitForSeconds(1.5f);
        }
        print("Ending Patrol...");
    }

}
