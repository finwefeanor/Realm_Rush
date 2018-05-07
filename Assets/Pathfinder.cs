using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

    Dictionary<Vector2Int, CubeWaypoint> grid = new Dictionary<Vector2Int, CubeWaypoint>();
    Vector2Int[] directions = {
        Vector2Int.up, 
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left,
     /* new Vector2Int(1, 1),  // upright
        new Vector2Int(1,-1), // downright
        new Vector2Int(-1, 1),  // upleft
        new Vector2Int(-1, -1),  // downleft
     */
    };

    public CubeWaypoint startPoint , endPoint;
    
    // Use this for initialization
    void Start () 
    {
        LoadBlocks();
        ColorsStartandEnd();
        ExploreNeighbors();
    }

    private void ExploreNeighbors() {
        foreach (Vector2Int direction in directions)
        {           
            var ExploreGridPos = startPoint.GetGridPos() + direction;
            print("Exploring block " + ExploreGridPos);

            if (grid.ContainsKey(ExploreGridPos)) // or use try-catch
            {
                grid[ExploreGridPos].SetColor(Color.yellow);
            }
        }
    }

    private void ColorsStartandEnd() {
        startPoint.SetColor(Color.cyan);
        endPoint.SetColor(Color.red);
    }

    private void LoadBlocks() {
        CubeWaypoint[] waypoints = GetComponentsInChildren<CubeWaypoint>();
        foreach (CubeWaypoint cubeWaypoint in waypoints)
        {
            var gridPos = cubeWaypoint.GetGridPos();
            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Destroying overlapping block" + cubeWaypoint);
                Destroy(cubeWaypoint.gameObject);
            }
            else
            {
                grid.Add(gridPos, cubeWaypoint);
            }
            
        }
        print("Loaded " + grid.Count + " elements");
    }
    
    public void SetthisColor(Color color) 
    {
        //_gameObject = GameObject.Find("0,0");
        MeshRenderer topMeshRenderer = transform.Find("Cube 0,0/Top" + "Cube 4,2/Top").GetComponent<MeshRenderer>();
        
        topMeshRenderer.material.color = color;
    }

    // Update is called once per frame
    void Update () 
    {
		
	}
}
