using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour 
{
    private int numTowers;
    [SerializeField] GameObject towerGO;
    [SerializeField] int towerLimit = 2;

    public void AddTower(CubeWaypoint cubeWaypointBase) 
    {
        var towers = FindObjectsOfType<Tower>();
        numTowers = towers.Length;

        if (numTowers < towerLimit)
        {
            Instantiate(towerGO, cubeWaypointBase.transform.position, Quaternion.identity);
            cubeWaypointBase.isPlaceable = false;
        }
        else
        {
            print("You exceeded tower limit");
        }
    }
}
