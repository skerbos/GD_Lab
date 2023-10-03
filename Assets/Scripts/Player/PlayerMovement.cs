using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public GameManager gameManager;

    public float horizontalSpeed = 10f;
    public float maxSpeed = 20f;
    public float smoothVal = 0.2f;
    public float gravityScale = 1f;
    public float fallingGravityScale = 3f;
    public float jumpSpeed = 10f;
    public float holdJumpSpeed = 7f;
    public float jumpButtonTime = 0.3f;
    public float jumpTime;

    private bool moving = false;
    private int moveDir;
    private bool jumpedState = false;

    [System.NonSerialized]
    public bool alive = true;
    public float deathImpulse = 5f;

    public Transform gameCamera;

    public Animator marioAnimator;

    public AudioSource marioAudio;
    public AudioClip marioDeath;

    private bool onGroundState = true;
    private Vector2 currentVelocity;
    private Rigidbody2D marioRb;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;

    public Image titleLogo;
    public Button startButton;
    public GameObject pistolSelectUI;
    public GameObject smgSelectUI;

    public GameObject enemies;

    int collisionLayerMask = (1 << 6) | (1 << 7) | (1 << 8);

    public JumpOverGoomba jumpOverGoomba;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;

        marioRb = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();

        marioAnimator.SetBool("onGround", onGroundState);

        /*
        Time.timeScale = 0f;

        scoreText.enabled = false;
        gameOverText.enabled = false;
        restartButton.enabled = false;
        */
        //pistolSelectUI.SetActive(false);
        //smgSelectUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //FlipSprite();
        skidAnimTrigger();
        setAnimSpeed();

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if ((collisionLayerMask & (1 << col.transform.gameObject.layer)) > 0 & !onGroundState)
        {
            onGroundState = true;
            marioRb.gravityScale = gravityScale;

            marioAnimator.SetBool("onGround", onGroundState);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

            //Death Animation
            marioAnimator.Play("mario-die");
            marioAudio.PlayOneShot(marioDeath);
            alive = false;
        }
    }

    void FixedUpdate()
    {
        if (!alive) return;

        AltMove(moveDir);
        //HorizontalMovement();
        //VerticalMovement();

        setAnimGroundBool();
    }

    void Move(int value)
    {
        Vector2 movement = new Vector2(value, 0);
        if (Mathf.Abs(value) > 0)
        {
            if (marioRb.velocity.magnitude < maxSpeed)
                marioRb.AddForce(movement * horizontalSpeed);
        }
        else
        {
            marioRb.velocity = new Vector2(Mathf.SmoothDamp(marioRb.velocity.x, Vector2.zero.x, ref currentVelocity.x, smoothVal), marioRb.velocity.y);
        }
    }

    void AltMove(int value)
    {
        if (Mathf.Abs(value) > 0)
        {
            marioRb.velocity = new Vector2(Mathf.Lerp(value * horizontalSpeed, marioRb.velocity.x,  smoothVal), marioRb.velocity.y);
        }
        else
        {
            marioRb.velocity = new Vector2(Mathf.SmoothDamp(marioRb.velocity.x, Vector2.zero.x, ref currentVelocity.x, smoothVal), marioRb.velocity.y);
        }
    }

    public void MoveCheck(int value)
    {
        if (value == 0)
        {
            moving = false;
        }
        else
        {
            FlipSprite(value);
            moving = true;
            Move(value);
        }
    }

    public void AltMoveCheck(int value)
    {
        moveDir = value;
    }

    public void Jump()
    {
        if (!alive && !onGroundState) return;

        marioRb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        onGroundState = false;
        jumpedState = true;

        marioAnimator.SetBool("onGround", onGroundState);
    }

    public void JumpHold()
    {
        if (!alive && !jumpedState) return;

        marioRb.AddForce(Vector2.up * jumpSpeed * holdJumpSpeed, ForceMode2D.Force);
        jumpedState = false;
    }

    void HorizontalMovement()
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

    void VerticalMovement()
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

        // Stop extended jump after jump key is lifted
        if (Input.GetKeyUp("space") && !onGroundState)
        {
            jumpTime = jumpButtonTime + 1f;
        }
    }

    void PlayDeathImpluse()
    {
        marioRb.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
    }

    void GameOverScene()
    {
        Time.timeScale = 0.0f;

        GameOver();

    }

    void FlipSprite(int value)
    {
        if (value == -1 && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;

            if (marioRb.velocity.x > 0.1f)
                marioAnimator.SetTrigger("onSkid");
        }

        if (value == 1 && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;

            if (marioRb.velocity.x < -0.1f)
                marioAnimator.SetTrigger("onSkid");
        }
    }

    void skidAnimTrigger()
    {
        if (marioRb.velocity.x > 1f && Input.GetKeyDown("a"))
            marioAnimator.SetTrigger("onSkid");

        if (marioRb.velocity.x < 1f && Input.GetKeyDown("d"))
            marioAnimator.SetTrigger("onSkid");
    }

    void setAnimSpeed()
    {
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioRb.velocity.x));
    }

    void setAnimGroundBool()
    {
        marioAnimator.SetBool("onGround", onGroundState);
    }

    void PlayJumpSound()
    {
        marioAudio.PlayOneShot(marioAudio.clip);
    }

    public void RestartButtonCallback(int input)
    {
        //ResetGame();
        Time.timeScale = 1.0f;
    }

    public void StartButtonCallback(int input)
    {
        //StartGame();
        Time.timeScale = 1.0f;
    }

    public void GameStart()
    {
           
    }

    public void GameOver()
    {
        //gameManager.GameOver();
    }

    public void GameRestart()
    {
        // reset position
        marioRb.transform.position = new Vector3(-9.27f, 1.25f, 0f);

        // reset sprite direction
        faceRightState = true;
        marioSprite.flipX = false;

        //  reset animation
        marioAnimator.SetTrigger("gameRestart");
        alive = true;

        // reset camera position
        gameCamera.position = new Vector3(0, 4, -10);

    }
}
