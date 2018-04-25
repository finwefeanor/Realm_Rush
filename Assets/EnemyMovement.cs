using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] List<CubeWaypoint> path;

	// Use this for initialization
	void Start () 
    {
        //StartCoroutine(PrintAllWayPoints());
        //print("I am at start"); // third execution
    }

    IEnumerator PrintAllWayPoints() // to add coroutine change return type IEnumerator
    {
        print("Starting Patrol..."); //firstly executed
        foreach (CubeWaypoint waypoint in path)
        {
            //print(element.name);
            transform.position = waypoint.transform.position;
            print("Visiting block: " + waypoint); //second execution
            yield return new WaitForSeconds(1.5f);
        }
        print("Ending Patrol...");
    }

    // Update is called once per frame
    void Update () 
    {
        
    }
}
