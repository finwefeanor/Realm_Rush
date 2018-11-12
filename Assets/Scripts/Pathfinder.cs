using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pathfinder : MonoBehaviour {

    [SerializeField] CubeWaypoint startPoint, endingPoint;

    Dictionary<Vector2Int, CubeWaypoint> grid = new Dictionary<Vector2Int, CubeWaypoint>();
    SortedDictionary<int, CubeWaypoint> sortedQueue = new SortedDictionary<int, CubeWaypoint>();
    int counter = 0;
    public bool isRunning = true;
    CubeWaypoint searchCenter; // the current search center. 
    Tower tower;

    List<CubeWaypoint> path = new List<CubeWaypoint>();

    Vector2Int[] directions = {
        Vector2Int.up, 
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left,
    };

    public List<CubeWaypoint> PathSize(CubeWaypoint start, CubeWaypoint end) 
    {
        if (path.Count == 0)
        {
            startPoint = start;
            endingPoint = end;
            ResetAllInfo();
            LoadBlocks();
            DijkstraSearch(); // in fact Breadth First Search
            CreatePath();
        }
        else
        {
            return path;
        }
        return path;
    }

    private void ResetAllInfo() 
    {
        path.Clear();
        grid.Clear();
        sortedQueue.Clear();
        counter = 0;
        isRunning = true;
    }

    private void LoadBlocks() 
    {
        CubeWaypoint[] waypoints = FindObjectsOfType<CubeWaypoint>();
        foreach (CubeWaypoint cubeWaypoint in waypoints)
        {
            cubeWaypoint.isExplored = false;
            cubeWaypoint.exploredFrom = null;
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
        print("Loaded" + grid.Count + "Blocks");
    }
     

    

    private void DijkstraSearch() {
        sortedQueue.Add(0, startPoint); //We add the starting point to the
        //SortedDictionary and we give it a Key 0 as it will not cost anything to get there

        while (sortedQueue.Count > 0 && isRunning)
            
        {
            var first = sortedQueue.First(); // get the first value from the dictionary
            //As it is sorted, it will be the waypoint where we can get with the lowest cost that has not yet been explored
            int key = first.Key; // we also need the key so we can remove it from the dictionary
            searchCenter = first.Value;
            StopIfEndIsSearchCenter(); //it stops searching if end has found
            sortedQueue.Remove(key);
            ExploreNeighbors();
            searchCenter.isExplored = true;
        }
        //print("Is Pathfinding Finished ? ");
    }

    private void StopIfEndIsSearchCenter() 
    {
        if (searchCenter == endingPoint) //searchcenter is dynamic, not bound to startpoint
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
            var ExploreGridPos = searchCenter.GetGridPos() + direction;
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

        if (neighbour != startPoint)
        {
            if((neighbour.costForPath > neighbour.travelCost + searchCenter.costForPath
                || neighbour.exploredFrom == null) && neighbour.walkable)
            // below should only happen if costs are lower than costs already OR when exploredfrom is empty and the neighbour is walkable.
            // you should create a public travelCost and a costForPath integer on your waypoint. If you want to have non-walkable waypoint, also add a public bool walkable on there
            // the travelCost is the cost for getting on that waypoint (so a mountain might be 10, where grassland might be 2). You set this number on the waypoint!
            // the costForPath should be 0 for all waypoints and is set from here. It will be the lowest cost to get to the waypoint from the starting point.
            // the walkable bool can be used for oceans or whatever where the player can't go. I've added this so I could change it to true for when the player gets a ship
            // you should set the walkable bool to true on every waypoint you want to travel on.
            {
                neighbour.costForPath = neighbour.travelCost + searchCenter.costForPath; // 6. the cost for path on the neighbour will be the cost for path on the waypoint we are exploring from + the cost of the neighbour itself.
                neighbour.exploredFrom = searchCenter; // we set the neighbour from which we investigated as the exploredFrom in the neighbour waypoint.
                sortedQueue.Add(neighbour.costForPath * 1000 + counter, neighbour); // here we add the waypoint to the Dictionary by multiplying the costForPath by a large number and adding the counter
                // we need to multiply with a high number and add the counter so we will not get an error as we might end up trying to add a key that already exsist in the Dictionary.
                // we multiply by this high number so the counter won't affect our pathfinding score (costForPath)
                counter++; // we add 1 to the counter so we get unique keys.
            }       
        }

        else
        {
            
        }
    }  
    private void SetAvailablePath(CubeWaypoint waypoint) 
    {
        path.Add(waypoint);
        waypoint.isPlaceable = false;
    }
    private void CreatePath() 
    {
        path.Add(endingPoint);
        CubeWaypoint previous = endingPoint.exploredFrom;
        while (previous != startPoint)
        {
            path.Add(previous);
            previous = previous.exploredFrom;
        }

        path.Add(startPoint);
        path.Reverse();
        //reverse the list       
    }
    
       
}
