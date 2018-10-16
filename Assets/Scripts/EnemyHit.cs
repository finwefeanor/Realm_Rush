using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour 
{
    [SerializeField] ParticleSystem hitParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] int scorePerHit = 12;
    [SerializeField] int takenHits = 8;
    ScoreHits scoreHits;
    BoxCollider boxCollider;

    void Start () {
        //AddBoxCollider();
        scoreHits = FindObjectOfType<ScoreHits>();
	}
	
	private void AddBoxCollider() {
        boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.size = new Vector3(7f, 6f, 6f);
        boxCollider.center = new Vector3(0f, 6f, 0f);
        boxCollider.isTrigger = true;
    }

    void OnParticleCollision(GameObject other) 
    {
        ProcessHit();
        if (takenHits <= 0)
        {
            KillEnemy();
            Debug.Log("Target Terminated");
        }      
    }
    
    private void ProcessHit() 
    {
        scoreHits.EnemyScore(scorePerHit);
        takenHits = takenHits - 1;
        hitParticles.Play();
    }

    private void KillEnemy() {
        DestroyEffect();
        Destroy(gameObject);        
    }

    private void DestroyEffect() {
        ParticleSystem fx = Instantiate(deathParticles, transform.position, Quaternion.identity);
        fx.Play();
        Destroy(fx, 1.4f);
    }
}
