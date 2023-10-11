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

    private int moveDir;
    private int moveDirVertical;
    private bool jumpedState = false;

    [System.NonSerialized]
    public bool alive = true;
    public float deathImpulse = 5f;

    public Transform gameCamera;

    private Animator marioAnimator;

    private AudioSource marioAudio;
    public AudioSource marioDeathAudio;

    public bool isFlying = false;
    private bool onGroundState = false;
    private Vector2 currentVelocity;
    private Rigidbody2D marioRb;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    private Vector3 originalPos;


    int collisionLayerMask = (1 << 6) | (1 << 7) | (1 << 8);

    private void Awake()
    {
        //GameManager.instance.gameStart.AddListener(GameStart);
        GameManager.instance.shmupGameStart.AddListener(ShmupGameStart);
        //GameManager.instance.gameRestart.AddListener(GameRestart);
        GameManager.instance.shmupGameRestart.AddListener(ShmupGameRestart);
        //GameManager.instance.gameOver.AddListener(GameOver);

    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;

        originalPos = transform.position;
        marioRb = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAudio = GetComponent<AudioSource>();
        marioAnimator = GetComponent<Animator>();

        marioAnimator.SetBool("onGround", onGroundState);

        if (isFlying)
        {
            marioRb.gravityScale = 0.0f;
        }
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
            marioDeathAudio.PlayOneShot(marioDeathAudio.clip);
            alive = false;
        }
    }

    void FixedUpdate()
    {
        if (!alive) return;

        AltMove(moveDir);
        //HorizontalMovement();
        //VerticalMovement();
        if (isFlying)
            MoveVertical(moveDirVertical);

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

    void MoveVertical(int value)
    {
        if (Mathf.Abs(value) > 0)
        {
            marioRb.velocity = new Vector2(marioRb.velocity.x, Mathf.Lerp(value * horizontalSpeed, marioRb.velocity.y, smoothVal));
        }
        else
        {
            marioRb.velocity = new Vector2(marioRb.velocity.x, Mathf.SmoothDamp(marioRb.velocity.y, Vector2.zero.y, ref currentVelocity.y, smoothVal));
        }
    }

    public void AltMoveCheck(int value)
    {
        moveDir = value;
    }

    public void MoveVerticalCheck(int value)
    {
        moveDirVertical = value;
    }

    public void Jump()
    {
        if (!alive || !onGroundState || isFlying) return;

        marioRb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        onGroundState = false;
        jumpedState = true;

        marioAnimator.SetBool("onGround", onGroundState);
    }

    public void JumpHold()
    {
        if (!alive || (!jumpedState && !onGroundState) || isFlying) return;

        marioRb.AddForce(Vector2.up * jumpSpeed * holdJumpSpeed, ForceMode2D.Force);
        onGroundState = false;
        jumpedState = false;
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

    public void ShmupGameStart()
    {
        
    }

    public void ShmupGameRestart()
    {
        // reset position
        transform.position = originalPos;

        // reset sprite direction
        faceRightState = true;
        marioSprite.flipX = false;

        //  reset animation
        marioAnimator.SetTrigger("gameRestart");
        alive = true;

        gameObject.transform.GetChild(0).GetChild(0).GetComponent<WeaponClass>().bulletsPerShot = 1;
        gameObject.transform.GetChild(0).GetChild(0).GetComponent<WeaponClass>().fireRate = 0.1f;
    }

    public void ShmupGameOver()
    {
        
    }
}
