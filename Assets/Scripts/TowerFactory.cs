using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour 
{
    private int numTowers;
    Tower queueTower;
    [SerializeField] Tower towerGO;
    [SerializeField] Transform towerParentsTransform;
    [SerializeField] int towerLimit = 2;

    Queue<Tower> queue = new Queue<Tower>();
    private Tower newTower;

    //create an empty queue of towers

    public void AddTower(CubeWaypoint cubeWaypointBase) 
    {
        print(queue.Count);
        numTowers = queue.Count;

        if (numTowers < towerLimit)
        {
            newTower = Instantiate(towerGO, cubeWaypointBase.transform.position,  Quaternion.identity);
            newTower.transform.parent = towerParentsTransform;
            cubeWaypointBase.isPlaceable = false;

            newTower.standWaypoint = cubeWaypointBase;
            cubeWaypointBase.isPlaceable = false;

            queue.Enqueue(newTower);
        }
        else
        {
            MoveExistingTower(cubeWaypointBase);
        }
        
    }

    private void MoveExistingTower(CubeWaypoint newBaseWaypoint) 
    {
        var oldTower = queue.Dequeue();

        oldTower.standWaypoint.isPlaceable = true;
        newBaseWaypoint.isPlaceable = false;

        oldTower.standWaypoint = newBaseWaypoint;
        oldTower.transform.position = newBaseWaypoint.transform.position;

        queue.Enqueue(oldTower);

        print("You exceeded tower limit");
    }
}
