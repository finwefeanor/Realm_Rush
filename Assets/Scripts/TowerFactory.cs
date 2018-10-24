using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour 
{
    private int numTowers;
    [SerializeField] GameObject towerGO;
    [SerializeField] int towerLimit = 2;

    Queue<Tower> queue = new Queue<Tower>();
    //create an empty queue of towers

    public void AddTower(CubeWaypoint cubeWaypointBase) 
    {
        var towers = FindObjectsOfType<Tower>(); // change to queue size
        numTowers = towers.Length;

        if (numTowers < towerLimit)
        {
            Instantiate(towerGO, cubeWaypointBase.transform.position, Quaternion.identity);
            cubeWaypointBase.isPlaceable = false;

            // set the cubeWaypoitBase

            // put new tower on the queue
        }
        else
        {
            MoveExistingTower(cubeWaypointBase);
        }
    }

    private static void MoveExistingTower(CubeWaypoint cubeWaypointBase) 
    {
        // take bottom tower off queue
        // set the placeable flags
        // set the cubeWaypoitBase
        // put old tower on top of the queue 
        print("You exceeded tower limit");
        //todo actually move the tower!
    }
}
