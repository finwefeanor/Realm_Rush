using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class F_BaseHealth : MonoBehaviour {

    public int baseHealth = 100;
    [SerializeField] int takenhits = 10;
    [SerializeField] Text baseHealthText;

    ScoreHits scoreHits;
    void Start() {
        baseHealthText.text = baseHealth.ToString();
    }

    void OnTriggerEnter(Collider other) {
        print("base collider working");

        baseHealth = baseHealth - takenhits;
        baseHealthText.text = baseHealth.ToString();

        if (baseHealth <= 0)
        {
            DestroyBase();          
        }
    }

    private void DestroyBase() {
        Debug.Log("Base Terminated");
    }
}
