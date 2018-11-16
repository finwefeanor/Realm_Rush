using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour 
{
    [SerializeField] ParticleSystem hitParticles;
    [SerializeField]
    ParticleSystem deathParticles;
    [SerializeField] int scorePerHit = 12;
    [SerializeField] int takenHits = 8;
    ScoreHits scoreHits;
    BoxCollider boxCollider;
    [SerializeField] AudioClip enemyHitSFX;
    [SerializeField] AudioClip enemyDeathSFX;

    AudioSource myAudioSource;

    void Start () {
        //AddBoxCollider();
        scoreHits = FindObjectOfType<ScoreHits>();
        myAudioSource = GetComponent<AudioSource>();
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
        GetComponent<AudioSource>().PlayOneShot(enemyHitSFX);
    }

    public void KillEnemy() 
    {  
        DestroyEffect();
        //AudioSource.PlayClipAtPoint(enemyDeathSFX, Camera.main.transform.position);
        
        Destroy(gameObject); //the enemy
    }

    private void DestroyEffect() // the deathparticle
    {
        var fx = Instantiate(deathParticles, transform.position, Quaternion.identity);
        AudioSource src = fx.gameObject.AddComponent<AudioSource>();
        src.PlayOneShot(enemyDeathSFX);
        //ParticleSystem parts = fx.GetComponent<ParticleSystem>();
        fx.Play();

        var main = fx.main;
        main.stopAction = ParticleSystemStopAction.Destroy;  
        //Destroy(fx, 3.0f); // if use this and not destroyed, try fx.gameobject to destroy.
    }
}
