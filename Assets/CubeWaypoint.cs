using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeWaypoint : MonoBehaviour 
{
    [SerializeField] Color exploredColor;

    //this bool var is for not requeuing grids that already been explored
    public bool isExplored = false;
    public CubeWaypoint exploredFrom;

    const int gridSize = 10;
    Vector2Int gridPos;

    GameObject startWayPoint;

    void Update() 
    {
        SetColor(); 
    }

    public int GetGridSize() 
    {
        return gridSize;
    }

    public Vector2Int GetGridPos() 
    {
        return new Vector2Int
         (Mathf.RoundToInt(transform.position.x / 10f),
          Mathf.RoundToInt(transform.position.z / 10f)
         );   
    }

    public void SetColor() 
    {
        MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRenderer.material.color = exploredColor;

        if (isExplored && exploredFrom == null)
        {
            exploredColor = Color.cyan;
        }
        else if (isExplored)
        {
            exploredColor = Color.yellow;
        }
        else
        {
            exploredColor = Color.red;
        }
    }

}
