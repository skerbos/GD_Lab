using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float horizontalSpeed = 10f;
    public float maxSpeed = 20f;
    public float upSpeed = 10;
    public float dampFallSpeed = 0.5f;
    private float currentFallDampSpeed;
    private bool onGroundState = true;
    Vector2 currentVelocity;
    float smoothVal = 0.5f;
    private Rigidbody2D marioRb;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        marioRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            onGroundState = true;
            //dampFallSpeed =
        }
    }

    void FixedUpdate()
    {
        horizontalMovement();
        jump();
    }

    void horizontalMovement()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(moveHorizontal) > 0)
        {
            Vector2 movement = new Vector2(moveHorizontal, 0);

            if (marioRb.velocity.magnitude < maxSpeed)
            {
                marioRb.AddForce(movement * horizontalSpeed);
            }
        }
        else
        {
            marioRb.velocity = Vector2.SmoothDamp(marioRb.velocity, Vector2.zero, ref currentVelocity, smoothVal);
        }
    }

    void jump()
    {
        if (Input.GetKeyDown("space") && onGroundState)
        {
            marioRb.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
        }

        if (Input.GetKeyDown("space") && !onGroundState)
        {

        }
    }
}
