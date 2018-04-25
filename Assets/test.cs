using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach this script to a Camera
public class test : MonoBehaviour 
{
    public Mesh mesh;
    public Material mat;
    public Vector3 meshPos;

    public void OnPostRender() 
    {
        
        // set first shader pass of the material
        mat.SetPass(0);
        // draw mesh at the origin
        Graphics.DrawMeshNow(mesh, meshPos, Quaternion.identity);
    }
}

