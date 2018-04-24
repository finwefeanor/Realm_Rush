using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditorSnap : MonoBehaviour {
    [SerializeField] [Range(1f, 20f)] int gridSize = 10;

    void Update() 
    {
        Vector3 snapPos;
        snapPos.x = Mathf.RoundToInt(transform.position.x / 10f) * gridSize;
        snapPos.z = Mathf.RoundToInt(transform.position.z / 10f) * gridSize;

        transform.position = new Vector3(snapPos.x, 0f, snapPos.z);
    }
}
