using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

    Dictionary<Vector2Int, CubeWaypoint> grid = new Dictionary<Vector2Int, CubeWaypoint>();
    Queue<CubeWaypoint> queue = new Queue<CubeWaypoint>(); // we constructed this like Dictionary

    Vector2Int[] directions = {
        Vector2Int.up, 
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left,
    };

    public CubeWaypoint startPoint , endPoint;

    public bool isRunning = true;

    // Use this for initialization
    void Start () 
    {
        LoadBlocks();
        ColorsStartandEnd();
        Pathfind();
    }

    private void Pathfind() {
        queue.Enqueue(startPoint); //add the startpoint to the Queue- so assign the reference (queue) to Enqueue

        while (queue.Count > 0 && isRunning) // we confirm something in the queue
        {
            var searchStart = queue.Dequeue(); // take it out the queue again
            print("Searching from: " + searchStart); // todo remove log   
            PauseIfEndHasFound(searchStart);
            ExploreNeighbors(searchStart);
            searchStart.isExplored = true;
        }

        print("Finished pathfinding");
    }

    private void PauseIfEndHasFound(CubeWaypoint searchStart) 
    {
        if (searchStart == endPoint)
        {
            print("same paths!");
            isRunning = false;
        }
    }

    private void ExploreNeighbors(CubeWaypoint from) 
    {
        if(!isRunning) { return; } //this gets called even it is not running

        foreach (Vector2Int direction in directions)
        {           
            var ExploreGridPos = from.GetGridPos() + direction;
            //print("Exploring block " + ExploreGridPos);
            //
            if (grid.ContainsKey(ExploreGridPos)) // or use try-catch
            {
                QueueNewNeighbours(ExploreGridPos);
            }
        }
    }

    private void QueueNewNeighbours(Vector2Int ExploreGridPos) 
    {
        CubeWaypoint neighbour = grid[ExploreGridPos];
        if(neighbour.isExplored)
        {
            // do nothing
        }
        else
        {
            neighbour.SetColor(Color.yellow);
            queue.Enqueue(neighbour);
            print("Queueing " + neighbour);
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
