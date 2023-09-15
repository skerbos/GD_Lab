using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public float bulletLifeTime = 5f;
    private GameObject attachedWeapon;

    private Rigidbody2D bulletRb;

    // Start is called before the first frame update
    void Start()
    {
        bulletRb = GetComponent<Rigidbody2D>();
        attachedWeapon = GameObject.Find("pistol");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        move();
        destroyAfterSeconds();
    }

    void move()
    {
        bulletRb.velocity = transform.right * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Enemy") || col.transform.CompareTag("Ground"))
        {
            Debug.Log("HIT " + col.transform.name);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("Enemy") || col.transform.CompareTag("Ground"))
        {
            Debug.Log("HIT " + col.transform.name);
            Destroy(gameObject);
        }
    }

    private void destroyAfterSeconds()
    {
        Destroy(gameObject, bulletLifeTime);
    }
}
