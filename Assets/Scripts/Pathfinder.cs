using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

    [SerializeField] CubeWaypoint startPoint, endingPoint;

    Dictionary<Vector2Int, CubeWaypoint> grid = new Dictionary<Vector2Int, CubeWaypoint>();
    Queue<CubeWaypoint> queue = new Queue<CubeWaypoint>(); // we constructed this like Dictionary
    public bool isRunning = true;
    CubeWaypoint searchStart; // the current search center. 
    Tower tower;

    List<CubeWaypoint> path = new List<CubeWaypoint>();

    Vector2Int[] directions = {
        Vector2Int.up, 
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left,
    };

    public List<CubeWaypoint> PathSize() 
    {
        if (path.Count == 0)
        {
            LoadBlocks();
            ScanSearch(); // in fact Breadth First Search
            CreatePath();
        }
        else
        {
            return path;
        }
        return path;
    }

    private void CreatePath() 
    {
        SetAvailablePath(endingPoint);

        CubeWaypoint previous = endingPoint.exploredFrom;
        while (previous != startPoint)
        {
            SetAvailablePath(previous);
            previous = previous.exploredFrom;
        }

        SetAvailablePath(startPoint);
        path.Reverse();
        //reverse the list       
    }

    private void SetAvailablePath(CubeWaypoint waypoint) {
        path.Add(waypoint);
        waypoint.isPlaceable = false;
    }

    private void ScanSearch() {
        queue.Enqueue(startPoint); //add the startpoint to the Queue- so assign the reference (queue) to Enqueue

        while (queue.Count > 0 && isRunning) // we confirm something in the queue
        {
            searchStart = queue.Dequeue(); // take it out the queue again
            PauseIfEndHasFound(); //it stops searching if end has found
            ExploreNeighbors();
            searchStart.isExplored = true;
        }
        //print("Is Pathfinding Finished ? ");
    }

    private void PauseIfEndHasFound() 
    {
        if (searchStart == endingPoint) //searchcenter is dynamic, not bound to startpoint
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
        //print("Loaded " + grid.Count + " elements");
    }  
}
