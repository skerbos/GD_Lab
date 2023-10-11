using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingKoopa : EnemyClass
{

    public GameObject koopaBullet;
    public float fireRate = 0.5f;
    private float lastFiredTime;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        player = GameObject.FindWithTag("Player");

        hudManager = GameObject.Find("Canvas").GetComponent<HUDManager>();
        hudManager.CreateHealthBar(gameObject);

        lastFiredTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
    }

    public override void Move()
    {
        rb.velocity = new Vector2(-moveSpeed, 0);
    }

    public override void Attack()
    {
        // Simple density bullets target at player
        if (Time.time > lastFiredTime + fireRate)
        {
            Vector2 dir = (Vector2)(player.transform.position - transform.position);
            float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            Instantiate(koopaBullet, transform.position + new Vector3(-1, 0, 0), Quaternion.Euler(0, 0, rotZ));
            lastFiredTime = Time.time;
        }

    }
}
