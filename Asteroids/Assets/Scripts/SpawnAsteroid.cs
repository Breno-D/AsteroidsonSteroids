using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroid : MonoBehaviour
{
    public GameObject asteroidPF;
    public Transform[] spawnPoints;
    public Transform playerPos;
    float distPlayer;
    int posSpawn=0;
    bool spawnAllowed = true;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        posSpawn = Random.Range(0, spawnPoints.Length);
        distPlayer = Vector3.Distance(spawnPoints[posSpawn].position, playerPos.position);
        if(spawnAllowed && distPlayer <= 10f)
        {
         GameObject tirimNormal = Instantiate (asteroidPF, spawnPoints[posSpawn].position, Quaternion.identity);
         spawnAllowed = false;
         Invoke("ResetSpawn", 10f);
        }
    }

    void ResetSpawn()
    {
        spawnAllowed = true;
    }
}
