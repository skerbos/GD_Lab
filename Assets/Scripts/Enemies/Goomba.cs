using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : EnemyClass
{

    public float moveDuration = 4f;

    private float direction = 1f;
    private float lastMovedTime;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;

        startPosition = transform.position;

        lastMovedTime = moveDuration;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        direction *= -1;
    }

    public override void Move()
    {
        // Move side to side for a fixed distance
        if (lastMovedTime > 0)
        {
            rb.velocity = transform.right * direction * moveSpeed;
            lastMovedTime -= Time.deltaTime;
        }
        else
        {
            lastMovedTime = moveDuration;
            direction *= -1;
        }

        // If bump into another object, switch direction
    }

    public override void Attack()
    {
        // Shoot single slow moving bullets if player is in range

    }
}
