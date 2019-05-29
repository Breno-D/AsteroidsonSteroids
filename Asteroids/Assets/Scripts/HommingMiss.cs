using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HommingMiss : MonoBehaviour
{
    public Transform target;
	public float speed = 50;
	public float rotateSpeed = 1000;
	public int tempoVivo=3;
	private Rigidbody2D rb;
	void Start () {
		target = GameObject.FindGameObjectWithTag("Player").transform;
		rb = GetComponent<Rigidbody2D>();
		Invoke("DestroyDaBomb", tempoVivo);
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = (Vector2)target.position - rb.position;
		direction.Normalize();

		float rotateAmount = Vector3.Cross(direction, transform.up).z;

		rb.angularVelocity = -rotateAmount * rotateSpeed;
		rb.velocity = transform.up * speed;
    }

    void DestroyDaBomb()
	{
		Destroy(gameObject);
	}
}
