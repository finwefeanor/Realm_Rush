using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour 
{
    [SerializeField] GameObject towerGO;
    CubeWaypoint cubeWaypoint;

    void Start() {

    }

    public void SpawnTowers() 
    {
        var testVar = towerGO.gameObject;
        print("Spawnnin works");
        Instantiate(testVar, cubeWaypoint.transform.position, Quaternion.identity);
    }

    void OnMouseOver() {

        if (Input.GetMouseButtonDown(0))
        {
            SpawnTowers();
        }
    }
}
