using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pathfinder : MonoBehaviour {
    public CubeWaypoint startWaypoint, endWaypoint;
    Dictionary<Vector2Int, CubeWaypoint> grid = new Dictionary<Vector2Int, CubeWaypoint>();
    SortedDictionary<int, CubeWaypoint> sortedQueue = new SortedDictionary<int, CubeWaypoint>(); // We use a SortedDictionary instead of a Queue
    int counter = 0; // this is for sorting purposes (to make sure we don't have duplicate keys in our SortedDictionary
    bool isRunning = true;
    CubeWaypoint searchCenter;
    List<CubeWaypoint> path = new List<CubeWaypoint>();
    Vector2Int[] directions ={
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };
    public List<CubeWaypoint> PathSize(CubeWaypoint start, CubeWaypoint end)  // I added the start and end waypoint here so you can send them from another script
    {
        startWaypoint = start; // this might have been unnessacery <-- spelled incorrect ;) 
        endWaypoint = end;
        ResetAllInfo();
        LoadBlocks();
        DijkstraSearch();  // Had to rename to Dijkstra ofcourse!
        StopIfEndIsSearchCenter();
        CreatePath();
        return path;
    }
    private void ResetAllInfo() // here we make sure we reset everything so we start clean everytime we create a path
    {
        path.Clear();
        grid.Clear();
        sortedQueue.Clear();
        counter = 0;
        isRunning = true;
    }
    private void LoadBlocks() {
        var waypoints = FindObjectsOfType<CubeWaypoint>();
        foreach (CubeWaypoint waypoint in waypoints)
        {
            waypoint.isExplored = false; // I think I added this.. not sure..
            waypoint.exploredFrom = null;
            var gridPos = waypoint.GetGridPos();
            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Skipping Overlapping block" + waypoint);
            }
            else
            {
                grid.Add(gridPos, waypoint);
            }
        }
        print("Loaded" + grid.Count + "Blocks");
    }

    private void DijkstraSearch() {
        sortedQueue.Add(0, startWaypoint); // we add the starting point to the SortedDictionary and we give it a Key 0 as it will not cost anything to get there.
        while (sortedQueue.Count > 0 && isRunning)
        {
            var first = sortedQueue.First(); // here we get the first value from the dictionary. As it is sorted, it will be the waypoint where we can get with the lowest cost that has not yet been explored
            int key = first.Key; // we also need the key so we can remove it from the dictionary
            searchCenter = first.Value; // guess I could have done this 2 rows up.. :o
            if (searchCenter == endWaypoint) // check if the waypoint we are exploring is the end waypoint
            {
                print("Found the end");
                isRunning = false;
            }

            sortedQueue.Remove(key); // here we remove the waypoint from the dictionary
            searchCenter.isExplored = true;
            ExploreNeighbours();
        }
    }
    private void StopIfEndIsSearchCenter() {
        if (searchCenter == endWaypoint)
        {
            print("Found the end");
            isRunning = false;
        }
    }
    private void ExploreNeighbours() {
        if (!isRunning) { return; }
        foreach (Vector2Int direction in directions)
        {
            Vector2Int NeighbourCoordinates = direction + searchCenter.GetGridPos();
            if (grid.ContainsKey(NeighbourCoordinates))
            {
                QueueNewNeighbours(NeighbourCoordinates);
            }
        }
    }
    private void QueueNewNeighbours(Vector2Int NeighbourCoordinates) {
        CubeWaypoint neighbour = grid[NeighbourCoordinates];
        if (neighbour.isExplored != startWaypoint)
        {
            if ((neighbour.costForPath > neighbour.travelCost + searchCenter.costForPath || neighbour.exploredFrom == null) && neighbour.walkable)
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
            else
            {
            }
        }
    }
    private void CreatePath() {
        path.Add(endWaypoint);
        CubeWaypoint previous = endWaypoint.exploredFrom;
        while (previous != startWaypoint)
        {
            path.Add(previous);
            previous = previous.exploredFrom;
        }
        path.Add(startWaypoint);
        path.Reverse();
    }
}