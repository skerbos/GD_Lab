using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float originalX;
    private float maxOffset = 5f;
    private float enemyPatrolTime = 2f;
    private int moveRight = -1;
    private Vector2 velocity;

    public Vector3 startPosition = new Vector3(9f, -0.5f, 0f);

    private Rigidbody2D enemyRb;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();

        originalX = transform.position.x;
        computeVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(enemyRb.position.x - originalX) < maxOffset)
        {
            moveGoomba();
        }
        else
        {
            // change direction
            moveRight *= -1;
            computeVelocity();
            moveGoomba();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
    }

    void computeVelocity()
    {
        velocity = new Vector2((moveRight) * maxOffset / enemyPatrolTime, 0);
    }

    void moveGoomba()
    {
        enemyRb.MovePosition(enemyRb.position + velocity * enemyPatrolTime * Time.fixedDeltaTime);
    }

}
