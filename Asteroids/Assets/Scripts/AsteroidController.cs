using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    float directxRandom, directyRandom;
    public float asteroidVel;
    float scaleRandom;
    public float asteroidLife;
    
    void Start()
    {
        directxRandom = Random.Range(-1.0f, 1f);
        directyRandom = Random.Range(-1.0f,1.0f);
        scaleRandom = Random.Range(0.3f, 1.5f);
        GetComponent<Transform>().localScale = new Vector3 (scaleRandom, scaleRandom, 1);
        asteroidLife = 100* scaleRandom;
        GetComponent<Rigidbody2D>().velocity = new Vector3(directxRandom, directyRandom, 0)*asteroidVel;
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.CompareTag("Player"))
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.CompareTag("NormalBullet"))
        {
            asteroidLife-=10;
        }else if(col.gameObject.CompareTag("ExplosiveBullet"))
        {
            asteroidLife=0;
        }
    }
    
    void Update()
    {
        if(asteroidLife <= 0)
        {
            DestroyAsteroid();
        }
    }

    void DestroyAsteroid()
    {

        Destroy(gameObject);
    }
}
