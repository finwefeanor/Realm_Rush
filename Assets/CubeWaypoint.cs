﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeWaypoint : MonoBehaviour {

    const int gridSize = 10;
    Vector2Int gridPos;

	
    public int GetGridSize() 
    {
        return gridSize;
    }

    public Vector2Int GetGridPos() 
    {
        return new Vector2Int(
        Mathf.RoundToInt(transform.position.x / 10f) * gridSize,
        Mathf.RoundToInt(transform.position.z / 10f) * gridSize
        );
    }

}
