using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableBoss : MonoBehaviour
{
    public int armToDisable, shootsToDisable, shootsA;
    void Start()
    {
        shootsA = shootsToDisable;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("NormalBullet"))
        {
            shootsToDisable--;
        }
        if(other.CompareTag("ExplosiveBullet"))
        {
            shootsToDisable -=2;
        }
    }
    
    void Update()
    {
        if (shootsToDisable <= 0)
        {
            if (armToDisable==0)
            {
                FindObjectOfType<BossIA>().shootAllowed=false;
                GetComponent<SpriteRenderer>().color= UnityEngine.Color.red;
                Invoke("ResetArm", 5f);
            }
            if (armToDisable==1)
            {
                FindObjectOfType<BossIA>().homingAllowed=false;
                GetComponent<SpriteRenderer>().color= UnityEngine.Color.red;
                Invoke("ResetArm", 5f);
            }
            shootsToDisable = shootsA;
        }

    }

    void ResetArm()
    {
        if (armToDisable==0)
        {
            FindObjectOfType<BossIA>().shootAllowed=true;
            GetComponent<SpriteRenderer>().color= UnityEngine.Color.white;
        }
        if (armToDisable==1)
        {
            FindObjectOfType<BossIA>().homingAllowed=true;
            GetComponent<SpriteRenderer>().color= UnityEngine.Color.white;
        }
    }
}
