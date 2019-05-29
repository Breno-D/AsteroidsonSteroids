using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : MonoBehaviour
{
    public bool isBossBullet;
    int i=5;
    void Start()
    {
        Invoke("DestroyBullet", 5f);
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.CompareTag("Asteroid") || col.CompareTag("Enemy")|| col.CompareTag("NormalBullet")||col.CompareTag("BossBullet"))
        {
            if (isBossBullet)
            {
                i--;
                if(i==0) DestroyBullet();
            }else DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
