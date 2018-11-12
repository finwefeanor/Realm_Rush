using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

    [SerializeField] Waypoint startWaypoint, endWaypoint;

<<<<<<< HEAD
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    bool isRunning = true;
    Waypoint searchCenter;
    List<Waypoint> path = new List<Waypoint>();
=======
    Dictionary<Vector2Int, CubeWaypoint> grid = new Dictionary<Vector2Int, CubeWaypoint>();
    SortedDictionary<int, CubeWaypoint> sortedQueue = new SortedDictionary<int, CubeWaypoint>();
    int counter = 0;
    Queue<CubeWaypoint> queue = new Queue<CubeWaypoint>(); // we constructed this like Dictionary
    public bool isRunning = true;
    CubeWaypoint searchStart; // the current search center. 
    Tower tower;

    List<CubeWaypoint> path = new List<CubeWaypoint>();
>>>>>>> parent of 226283c... Dijkstra_implementation_1

    Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

<<<<<<< HEAD
    public List<Waypoint> GetPath()
    {
        LoadBlocks();
        ColorStartAndEnd();
        BreadthFirstSearch();
        CreatePath();
        return path;
    }

    private void CreatePath()
    {
        path.Add(endWaypoint);

        Waypoint previous = endWaypoint.exploredFrom;
        while (previous != startWaypoint)
        {
            path.Add(previous);
            previous = previous.exploredFrom;
        }

        path.Add(startWaypoint);
        path.Reverse();
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startWaypoint);

        while(queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            HaltIfEndFound();
            ExploreNeighbours();
            searchCenter.isExplored = true;
=======
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
>>>>>>> parent of 226283c... Dijkstra_implementation_1
        }
    }

<<<<<<< HEAD
    private void HaltIfEndFound()
    {
        if (searchCenter == endWaypoint)
=======
    private void PauseIfEndHasFound() 
    {
        if (searchStart == endingPoint) //searchcenter is dynamic, not bound to startpoint
>>>>>>> parent of 226283c... Dijkstra_implementation_1
        {
            isRunning = false;
        }
    }

    private void ExploreNeighbours()
    {
        if (!isRunning) { return; }

        foreach (Vector2Int direction in directions)
<<<<<<< HEAD
        {
            Vector2Int neighbourCoordinates = searchCenter.GetGridPos() + direction;
            if (grid.ContainsKey(neighbourCoordinates))
=======
        {           
            var ExploreGridPos = searchStart.GetGridPos() + direction;
            //print("Exploring block " + ExploreGridPos);
            //
            if (grid.ContainsKey(ExploreGridPos)) // or use try-catch
>>>>>>> parent of 226283c... Dijkstra_implementation_1
            {
                QueueNewNeighbours(neighbourCoordinates);
            }
        }
    }
<<<<<<< HEAD

    private void QueueNewNeighbours(Vector2Int neighbourCoordinates)
    {
        Waypoint neighbour = grid[neighbourCoordinates];
        if (neighbour.isExplored || queue.Contains(neighbour))
        {
            // do nothing
=======
   
    private void QueueNewNeighbours(Vector2Int ExploreGridPos) 
    {
        CubeWaypoint neighbour = grid[ExploreGridPos];
        if(neighbour.isExplored || queue.Contains(neighbour))
        {
            // do nothing   
>>>>>>> parent of 226283c... Dijkstra_implementation_1
        }
        else
        {
            queue.Enqueue(neighbour);
<<<<<<< HEAD
            neighbour.exploredFrom = searchCenter;
        }
    }

    private void ColorStartAndEnd()
    {
        // todo consdier moving out
        startWaypoint.SetTopColor(Color.green);
        endWaypoint.SetTopColor(Color.red);
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPos();
            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Skipping overlapping block " + waypoint);
            }
            else
            {
                grid.Add(gridPos, waypoint);
            }
        }
    }
=======
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
>>>>>>> parent of 226283c... Dijkstra_implementation_1
}
