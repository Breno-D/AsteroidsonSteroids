using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    public GameObject normalShoot;
    public Transform shootingPoint, enemyTrans;
    public Transform playerTrans;
    public float eBulletVel, turningSpeed;
    bool canShoot=true;
    int enemyLife;
    void Start()
    {
        playerTrans = FindObjectOfType<PlayerConroller>().playerTrans;
        enemyLife=3;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDir = playerTrans.position - transform.position;
        float step = turningSpeed * Time.deltaTime;
        enemyTrans.up = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);

        float dist = Vector3.Distance(playerTrans.position, transform.position);

      
        if (dist <= 10f && dist > 5)
        {
         transform.position = Vector3.MoveTowards(transform.position, playerTrans.position, 0.05f);
        }

        if (dist <= 10)
        {
            ShootPlayer();
        }
        if(enemyLife <= 0)
        {
            Destroy(gameObject);
        }

       
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.CompareTag("NormalBullet"))
        {
            enemyLife--;
        }
    }
    void ShootPlayer()
    {
        if(canShoot){
        GameObject tirimNormal = Instantiate (normalShoot, shootingPoint.position, Quaternion.identity);
        tirimNormal.GetComponent<Rigidbody2D>().velocity = enemyTrans.up * eBulletVel* Time.deltaTime;
        canShoot=false;
        Invoke("ResetCanShoot", 1f);
        }
    }

    void ResetCanShoot()
    {
        canShoot = true;
    }
}


