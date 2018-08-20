using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach this script to a Camera
public class test : MonoBehaviour 
{
    public Mesh mesh;
    public Material mat;
    public Vector3 meshPos;
    //public Transform scale;

    public void OnPostRender() 
    {
        
        // set first shader pass of the material
        mat.SetPass(0);
        // draws a mesh at the origin or given position
        
        Graphics.DrawMeshNow(mesh, meshPos, Quaternion.identity);
        mesh.bounds.size.Set(7f, 6f, 6f);
    }
}

