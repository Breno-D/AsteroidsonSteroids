using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossIA : MonoBehaviour
{
    public Transform playerTrans;
    // normalshoot

    public bool shootAllowed;
	public GameObject bulletSpawned;
	public GameObject bullet;
	public Transform shootPoints;
	public float bulletSpeed;

    // homing shoot
    public bool homingAllowed;
	public GameObject homingSpawned;
	public GameObject homingMissile;
	public Transform homingPoints;
	int randomHomingPoint;

    //"IA" variables
    public float mTempoEspera;
	float nextAction;
	float mTime;
    //win condition variables
    float bossLife;
    public Text lifeText;
    void Start()
    {
        shootAllowed = true;
        homingAllowed = true;
		nextAction = 0;
		mTime = mTempoEspera;
        bossLife=100;
        lifeText.text = bossLife+"%";
    }

  
    void Update()
    {
        nextAction = Random.Range(0f, 2f);
        lifeText.text = bossLife+"%";
        if (mTime <= 0)
		{
			ControlActions(nextAction);
			mTime = mTempoEspera;
		} else mTime -= Time.deltaTime;

        if(bossLife <= 0)
        {
            WinGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("NormalBullet"))
        {
            bossLife -= 2;
        }
        if (other.CompareTag("ExplosiveBullet"))
        {
            bossLife -=7;
        }
    }
    void ShootDatBitch()
	{
        if(shootAllowed)
        {
        Vector3 targetDir = playerTrans.position - shootPoints.position;
        float step = 250f * Time.deltaTime;
        shootPoints.up = Vector3.RotateTowards(shootPoints.forward, targetDir, step, 0.0f);
		bulletSpawned = Instantiate(bullet, shootPoints.position, Quaternion.identity);
		bulletSpawned.GetComponent<Rigidbody2D>().velocity = shootPoints.up * bulletSpeed * Time.deltaTime;
        }
	}

    void HomingShootDatBitch()
    {
        if(homingAllowed)
        {
        homingSpawned = Instantiate (homingMissile, homingPoints.position, Quaternion.identity);
        }
    }

    void ControlActions(float nextA)
	{
		if (nextA <= 1f)
		{
			Invoke("ShootDatBitch", 0.0f);
		}
		else if (nextA > 1f && nextA <= 2f)
		{
			Invoke("HomingShootDatBitch", Random.Range(0f, 4f));
		}
	}

    void WinGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }
}
