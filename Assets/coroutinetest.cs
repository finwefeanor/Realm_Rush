using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coroutinetest : MonoBehaviour {

    void Start() 
    {
        StartCoroutine(Example());
    }

    IEnumerator Example() 
    {
        print(Time.time);
        yield return new WaitForSeconds(5);
        print(Time.time);
    }

}
