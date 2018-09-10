using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverExample : MonoBehaviour {

    void OnMouseOver() {
        Debug.Log("Mouse is over GameObject: " + gameObject.name);
    }

    void OnMouseExit() {
        Debug.Log("Mouse is no longer on GameObject: " + gameObject.name);
    }
}
