using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHits : MonoBehaviour {

    int scoreEnemy;
    Text scoreText;
 
	void Start () 
    {
        scoreText = GetComponent<Text>();
        scoreText.text = scoreEnemy.ToString();
    }
	
	public void EnemyScore(int scoreIncrease) {
        scoreEnemy = scoreEnemy + scoreIncrease;
        scoreText.text = scoreEnemy.ToString();
    }

}
