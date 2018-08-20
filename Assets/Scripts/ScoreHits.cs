using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHits : MonoBehaviour {

    int score;
    Text scoreText;
 
	void Start () 
    {
        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
	}
	
	public void EnemyScore(int scoreIncrease) {
        score = score + scoreIncrease;
        scoreText.text = score.ToString();
    }

}
