using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastExample : MonoBehaviour 
{

    void Update() 
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            print("There is an object - distance " + hit.distance);
            Debug.DrawLine(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);

        }
        //if (Physics.Raycast(ray, out hit, 100))
        //{
        //    Debug.DrawLine(ray.origin, hit.point);
        //    print("Hit something!");
        //}

    }
}
