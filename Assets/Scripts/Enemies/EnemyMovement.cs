using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : EnemyClass
{
    private float originalX;
    private float maxOffset = 5f;
    private float enemyPatrolTime = 2f;
    private int moveRight = -1;
    private Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        originalX = transform.position.x;
        ComputeVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(rb.position.x - originalX) < maxOffset)
        {
            Move();
        }
        else
        {
            // change direction
            moveRight *= -1;
            ComputeVelocity();
            Move();
        }
    }

    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight * moveSpeed) * maxOffset / enemyPatrolTime, 0);
    }

    public override void Move()
    {
        rb.MovePosition(rb.position + velocity * enemyPatrolTime * Time.fixedDeltaTime);
    }

    public void GameRestart()
    {
        transform.localPosition = startPosition;
        originalX = transform.position.x;
        moveRight = -1;
        ComputeVelocity();

        currentHealth = maxHealth;
    }

}
