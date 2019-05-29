using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControllervsBoss : MonoBehaviour
{
    public float vel,bulletVel;
    public Transform shootingPoint, playerTrans;
    bool canEShoot;
    public float playerLife = 100;
    public GameObject normalShoot, explosiveShoot;
    int currentShoot;
    public Text lifeText;
    public Text cdText;
    bool canTakeDmg;
    public Animator anim;
    public AudioSource barrelSound;
    bool canBarrel;
    void Start()
    {
        canBarrel=true;
        canTakeDmg=true;
        playerTrans = FindObjectOfType<PlayerControllervsBoss>().transform;
        currentShoot=1;
        transform.position = new Vector3(-7.34f,-0.65f,0.0f);
        canEShoot=true;
        cdText.text = "Available";
        lifeText.text = playerLife+ "%";
        cdText.color = UnityEngine.Color.green;
        lifeText.color = UnityEngine.Color.green;
    }

    private void OnTriggerEnter2D(Collider2D col) 
    {
         if(col.gameObject.CompareTag("BossBullet"))
        {
            if(canTakeDmg)
            {
                canTakeDmg=false;
                playerLife -= 15;
                Invoke("ResetTakeDmg", 1f);
            }
        }
    }
    // Update is called once per frame
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

        if (v!=0 || h!=0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(h,v) * vel * Time.deltaTime;
        } else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0) * vel * Time.deltaTime;
        }



         if(Input.GetButtonDown("Fire1"))
        {
            currentShoot=1;
        }else if (Input.GetButtonDown("Fire2"))
        {
            currentShoot=2;
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
            Invoke("ResetBarrel", 0.7f);
            Invoke("ResetTakeDmg", 0.7f);
            Invoke("ResetCDBarrel", 2f);
            }
        }
    }

    void ShootDatBitch(int i)
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
    }

    void ResetBarrel()
    {
        anim.SetBool("Barrelroll", false);
    }
    void ResetTakeDmg()
    {
        canTakeDmg=true;
    }
    void ResetEShoot()
    {
        canEShoot = true;
        cdText.text = "Available";
        cdText.color = UnityEngine.Color.green;
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
