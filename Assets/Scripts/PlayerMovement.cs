using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float horizontalSpeed = 10f;
    public float maxSpeed = 20f;
    public float smoothVal = 0.2f;
    public float gravityScale = 1f;
    public float fallingGravityScale = 3f;
    public float jumpSpeed = 10f;
    public float holdJumpSpeed = 7f;
    public float jumpButtonTime = 0.3f;
    public float jumpTime;
    private bool onGroundState = true;
    Vector2 currentVelocity;
    
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
            marioRb.gravityScale = gravityScale;
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
            // Smooth stopping 
            marioRb.velocity = new Vector2 (Mathf.SmoothDamp(marioRb.velocity.x, Vector2.zero.x, ref currentVelocity.x, smoothVal), marioRb.velocity.y);
        }
    }

    void jump()
    {
        if (Input.GetKeyDown("space") && onGroundState)
        {
            marioRb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            jumpTime = 0;
        }

        if (!onGroundState && marioRb.velocity.y >= 0)
        {
            marioRb.gravityScale = gravityScale;
        }
        else if (!onGroundState && marioRb.velocity.y < 0)
        {
            marioRb.gravityScale = fallingGravityScale;
        }

        // Extended jump when jump key is held down, slightly higher jump and air time
        if (Input.GetKey("space") && !onGroundState && jumpTime <= jumpButtonTime)
        {
            marioRb.AddForce(Vector2.up * holdJumpSpeed, ForceMode2D.Force);
            jumpTime += Time.deltaTime;
        }
    }
}
