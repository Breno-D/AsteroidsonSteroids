using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerConroller : MonoBehaviour
{
    public float bulletVel ,vel, rotationVel;
    public Transform playerTrans;
    int currentShoot;
    bool canEShoot, canStopShip;
    public GameObject normalShoot, explosiveShoot;
    public Transform shootingPoint;

    public float playerLife = 100;
    public Text lifeText;
    public Text cdText;
    public Text stopText;
    bool canTakeDmg;
    public Animator anim;

    bool canBarrel;

    public AudioSource barrelSound;
    void Start()
    {
        canBarrel=true;
        currentShoot=1;
        transform.position = new Vector3(0,0,0);
        canEShoot=true;
        canStopShip=true;
        canTakeDmg=true;
        cdText.text = "Available";
        stopText.text = "Available";
        stopText.color = UnityEngine.Color.green;
        lifeText.text = playerLife+ "%";
        cdText.color = UnityEngine.Color.green;
        lifeText.color = UnityEngine.Color.green;
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Asteroid"))
        {
            if(canTakeDmg)
            {
             playerLife -= 10;
             canTakeDmg=false;
             Invoke("ResetTakeDmg", 1f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col) 
    {
         if(col.gameObject.CompareTag("Enemy"))
        {
            if(canTakeDmg)
            {
             playerLife -= 20;
             canTakeDmg=false;
             Invoke("ResetTakeDmg", 1f);
            }
        }
    }

    void Update()
    {
        lifeText.text = playerLife + "%";
        if (playerLife <= 80 && playerLife > 60)
        {
            lifeText.color = UnityEngine.Color.yellow;
        }
        else if (playerLife <= 60 && playerLife > 40)
        {
            lifeText.color = new Color(1.0f, 0.64f, 0.0f);
        }
        else if (playerLife <= 40 && playerLife > 20)
        {
            lifeText.color = UnityEngine.Color.red;
        }
        
        if (playerLife <= 0)
        {
            EndGame();
        }


        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");


        if(h!= 0)
        {
            playerTrans.Rotate(0, 0, -h * rotationVel * Time.deltaTime, Space.Self);
        }
        if (v > 0)
        {
            GetComponent<Rigidbody2D>().velocity = playerTrans.up * vel * Time.deltaTime;
        }

        if(Input.GetButtonDown("Fire1"))
        {
            currentShoot=1;
        }else if (Input.GetButtonDown("Fire2"))
        {
            currentShoot=2;
        } else if (Input.GetButtonDown("Fire3"))
        {
            currentShoot=3;
        }

        if(Input.GetButtonDown("Fire"))
        {
            ShootDatBitch(currentShoot);
        }

        if(Input.GetButtonDown("Barrel"))
        {
            if(canBarrel)
            {
            canBarrel = false;
            barrelSound.Play();
            canTakeDmg=false;
            anim.SetBool("Barrelroll", true);
            Invoke("ResetBarrel", 0.5f);
            Invoke("ResetTakeDmg", 0.5f);
            Invoke("ResetCDBarrel", 2f);
            }
        }
    }

    public void ShootDatBitch(int i)
    {
        if(i==1)
        {
           GameObject tirimNormal = Instantiate (normalShoot, shootingPoint.position, Quaternion.identity);
           tirimNormal.GetComponent<Rigidbody2D>().velocity = playerTrans.up * bulletVel* Time.deltaTime;
        }
        else if (i==2)
        {
            if(canEShoot)
            {
            GameObject tirimqIsprode = Instantiate (explosiveShoot, shootingPoint.position, Quaternion.identity);
            tirimqIsprode.GetComponent<Rigidbody2D>().velocity = playerTrans.up * bulletVel/2 * Time.deltaTime;
            canEShoot=false;
            Invoke("ResetEShoot", 5f);
            cdText.text = "Recharging";
            cdText.color = UnityEngine.Color.red;
            }
        }
        else if (i==3)
        {
            if(canStopShip)
            {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            canStopShip = false;
            Invoke("ResetStopShip", 3f);
            stopText.text = "Recharging";
            stopText.color = UnityEngine.Color.red;
            }
        }
    }

    void ResetEShoot()
    {
        cdText.text = "Available";
        cdText.color = UnityEngine.Color.green;
        canEShoot = true;
    }
    void ResetStopShip()
    {
        stopText.text = "Available";
        stopText.color = UnityEngine.Color.green;
        canStopShip=true;
    }
    void ResetTakeDmg()
    {
        canTakeDmg = true;
    }
    void ResetBarrel()
    {
        anim.SetBool("Barrelroll", false);
        canTakeDmg=true;
    }
    void ResetCDBarrel()
    {
        canBarrel = true;
    }
    void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
}
