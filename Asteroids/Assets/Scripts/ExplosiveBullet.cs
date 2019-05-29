using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : MonoBehaviour
{
    public GameObject exprosium, explosao;
    void Start()
    {
        Invoke ("IExplosao", 2f);
    }

    private void OnTriggerEnter2D(Collider2D col) 
    {
      if(col.CompareTag("Asteroid") || col.CompareTag("Enemy"))
        {
            IExplosao();
        }  
    }
    
    void IExplosao()
    {
       explosao = Instantiate (exprosium, transform.position, Quaternion.identity);
       this.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
       Invoke("DestroyBullet", 2f);
    }

    void DestroyBullet()
    {
        Destroy(explosao);
        Destroy(gameObject);
    }
}
