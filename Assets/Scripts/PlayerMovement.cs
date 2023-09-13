using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
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
    private Vector2 currentVelocity;
    private Rigidbody2D marioRb;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public Image gameOverScreen;
    private Vector3 originalScoreTextPos = new Vector3(-780, 480, 0);
    private Vector3 originalRestartButtonPos = new Vector3(850, 480, 0);
    private Vector3 gameOverScoreTextPos = new Vector3(0, 0, 0);
    private Vector3 gameOverRestartButtonPos = new Vector3(0, -135, 0);
    public GameObject enemies;

    public JumpOverGoomba jumpOverGoomba;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        marioRb = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        gameOverText.enabled = false;
        gameOverScreen.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        flipSprite();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            onGroundState = true;
            marioRb.gravityScale = gravityScale;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with goomba!");
            gameOverScreen.enabled = true;
            gameOverText.enabled = true;
            scoreText.transform.localPosition = gameOverScoreTextPos;
            restartButton.transform.localPosition = gameOverRestartButtonPos;
            Time.timeScale = 0f;
        }
    }

    void FixedUpdate()
    {
        horizontalMovement();
        verticalMovement();
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
            marioRb.velocity = new Vector2(Mathf.SmoothDamp(marioRb.velocity.x, Vector2.zero.x, ref currentVelocity.x, smoothVal), marioRb.velocity.y);
        }
    }

    void verticalMovement()
    {
        // Initial tap jump
        if (Input.GetKeyDown("space") && onGroundState)
        {
            marioRb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            jumpTime = 0;
        }

        // Faster falling
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

        if (Input.GetKeyUp("space") && !onGroundState)
        {
            jumpTime = jumpButtonTime + 1f;
        }
    }

    void flipSprite()
    {
        if (Input.GetKeyDown("a") && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
        }

        if (Input.GetKeyDown("d") && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
        }
    }

    public void RestartButtonCallback(int input)
    {
        Debug.Log("Restart!");
        resetGame();
        Time.timeScale = 1.0f;
    }

    private void resetGame()
    {
        marioRb.velocity = new Vector3(0, 0, 0);
        marioRb.transform.position = new Vector3(-9.27f, 1.25f, 0f);
        faceRightState = true;
        marioSprite.flipX = false;

        scoreText.text = "Score: 0";
        scoreText.transform.localPosition = originalScoreTextPos;
        restartButton.transform.localPosition = originalRestartButtonPos;
        gameOverText.enabled = false;
        gameOverScreen.enabled = false;

        foreach (Transform eachChild in enemies.transform)
        {
            eachChild.transform.localPosition = eachChild.GetComponent<EnemyMovement>().startPosition;
        }

        jumpOverGoomba.score = 0;
    }
}
