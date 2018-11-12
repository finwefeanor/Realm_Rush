using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
<<<<<<< HEAD
=======
    [SerializeField] float movingSpeed = 15f;
    [SerializeField] float movingPeriod = 0.5f;
>>>>>>> parent of 226283c... Dijkstra_implementation_1

	// Use this for initialization
	void Start () {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
<<<<<<< HEAD
        var path = pathfinder.GetPath();
        StartCoroutine(FollowPath(path));
	}
=======
        var path = pathfinder.PathSize();
        StartCoroutine(PrintAllWayPoints(path)); // don't forget the reference if encounter a problem "path"
    }
>>>>>>> parent of 226283c... Dijkstra_implementation_1

    IEnumerator FollowPath(List<Waypoint> path)
    {
        print("Starting patrol..."); 
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(1f);
        }
        print("Ending patrol");
    }
}
