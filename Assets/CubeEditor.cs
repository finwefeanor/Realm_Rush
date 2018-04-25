using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(CubeWaypoint))]
public class CubeEditor : MonoBehaviour {
    //[SerializeField] [Range(1f, 20f)] int gridSize = 10;

    CubeWaypoint waypoint;

    private void Awake() {
        waypoint = GetComponent<CubeWaypoint>();
    }

    void Update() 
    {
        SnapToGrid();
        UpdateLabel();
    }

    private void SnapToGrid() 
    {
        //int gridSize = waypoint.GetGridSize();
        transform.position = new Vector3(
            waypoint.GetGridPos().x,
            0f, 
            waypoint.GetGridPos().y);
    }

    private void UpdateLabel() 
    {
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        int gridSize = waypoint.GetGridSize();
        string labelText = 
            waypoint.GetGridPos().x / gridSize + 
            "," +
            waypoint.GetGridPos().y / gridSize;
        textMesh.text = labelText;
        gameObject.name = "Cube " + labelText;
    }
}
