using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public float bulletLifeTime = 5f;
    public float bulletDamage = 10f;
    public TextMeshProUGUI damageNumberText;
    private Vector3 damageNumberTextOffset;

    private Rigidbody2D bulletRb;

    // Start is called before the first frame update
    void Start()
    {
        bulletRb = GetComponent<Rigidbody2D>();
        damageNumberTextOffset = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
        Destroy(gameObject, bulletLifeTime);
    }

    void Move()
    {
        bulletRb.velocity = transform.right * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Enemy") || col.transform.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("Enemy") || col.transform.CompareTag("Ground"))
        {
            TextMeshProUGUI damageNumberClone = Instantiate(damageNumberText, Camera.main.WorldToScreenPoint(col.transform.position) + damageNumberTextOffset, col.transform.rotation);
            damageNumberClone.GetComponent<DamageNumberBehavior>().damageValue = bulletDamage;

            Destroy(gameObject);
        }
    }
}