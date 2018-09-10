using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeWaypoint : MonoBehaviour 
{
    [SerializeField] Color exploredColor;

    //this bool var is for not requeuing grids that already been explored
    public bool isExplored = false;
    public bool isPlaceable = true;
    public CubeWaypoint exploredFrom;
    TowerSpawner towerSpawner;

    const int gridSize = 10;
    Vector2Int gridPos;

    GameObject startWayPoint;

    void Update() 
    {
        SetColor(); 
    }

    void OnMouseOver() 
    {
        
        if (isPlaceable && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Cube clicked. You can place a tower here: " + gameObject.name);
        }
        else if (!isPlaceable && Input.GetMouseButtonDown(0))
        {
            print("This is the path, You can't place here !!");
        }       
    }

    private void TestMethod() 
    {
        
    }

    void OnMouseExit() {

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
