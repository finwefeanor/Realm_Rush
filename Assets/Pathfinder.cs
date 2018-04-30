using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

    Dictionary<Vector2Int, CubeWaypoint> grid = new Dictionary<Vector2Int, CubeWaypoint>();

    public CubeWaypoint startPoint , endPoint;
    // Use this for initialization
    void Start () 
    {
        LoadBlocks();
        ColorsStartandEnd();
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
