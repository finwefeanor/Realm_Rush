using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

    [SerializeField] CubeWaypoint startPoint, endPoint;

    Dictionary<Vector2Int, CubeWaypoint> grid = new Dictionary<Vector2Int, CubeWaypoint>();
    Queue<CubeWaypoint> queue = new Queue<CubeWaypoint>(); // we constructed this like Dictionary
    public bool isRunning = true;
    CubeWaypoint searchStart; // the current search center. 

    List<CubeWaypoint> path = new List<CubeWaypoint>();

    Vector2Int[] directions = {
        Vector2Int.up, 
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left,
    };

    public List<CubeWaypoint> PathSize() 
    {
        LoadBlocks();
        ColorsStartandEnd();
        ScanSearch(); // in fact Breadth First Search
        CreatePath();
        return path;  
    }
    

    private void CreatePath() {
        path.Add(endPoint);

        CubeWaypoint previous = endPoint.exploredFrom;
        while(previous != startPoint)
        {
            path.Add(previous);
            previous = previous.exploredFrom;
        }

        // add start waypoint
        path.Add(startPoint);
        path.Reverse();
        //reverse the list
        
    }

    private void ScanSearch() {
        queue.Enqueue(startPoint); //add the startpoint to the Queue- so assign the reference (queue) to Enqueue

        while (queue.Count > 0 && isRunning) // we confirm something in the queue
        {
            searchStart = queue.Dequeue(); // take it out the queue again
            //print("Searching from: " + searchStart); // todo remove log   
            PauseIfEndHasFound(); //it stops searching if end has found
            ExploreNeighbors();
            searchStart.isExplored = true;
        }

        //print("Is Pathfinding Finished ? ");
    }

    private void PauseIfEndHasFound() 
    {
        if (searchStart == endPoint) //searchcenter is dynamic, not bound to startpoint
        {
            //print("Start - End are the same paths!");
            isRunning = false;
        }
    }

    private void ExploreNeighbors() 
    {
        if(!isRunning) { return; } //this gets called even it is not running

        foreach (Vector2Int direction in directions)
        {           
            var ExploreGridPos = searchStart.GetGridPos() + direction;
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
        if(neighbour.isExplored || queue.Contains(neighbour))
        {
            // do nothing   
        }

        else
        {
            queue.Enqueue(neighbour);
            neighbour.exploredFrom = searchStart;
            //print("Queueing " + neighbour);           
        }
    }

    private void ColorsStartandEnd() 
    {
        //endPoint.SetColor(Color.red);
        //startPoint.SetColor(Color.cyan);
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
