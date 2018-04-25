using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

    Dictionary<Vector2Int, CubeWaypoint> grid = new Dictionary<Vector2Int, CubeWaypoint>();
	// Use this for initialization
	void Start () 
    {
        LoadBlocks();
	}

    private void LoadBlocks() {
        CubeWaypoint[] waypoints = GetComponentsInChildren<CubeWaypoint>();
        foreach (CubeWaypoint element in waypoints)
        {
            var gridPos = element.GetGridPos();
            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Destroying overlapping block" + element);
                Destroy(element.gameObject);
            }
            else
            {
                grid.Add(gridPos, element);
            }
            
        }
        print("Loaded " + grid.Count + " elements");
    }

    // Update is called once per frame
    void Update () 
    {
		
	}
}
