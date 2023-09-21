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

    [System.NonSerialized]
    public bool alive = true;
    public float deathImpulse = 5f;

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
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public Image translucentOverlay;
    private Vector3 originalScoreTextPos = new Vector3(-780, 480, 0);
    private Vector3 originalRestartButtonPos = new Vector3(850, 480, 0);
    private Vector3 gameOverScoreTextPos = new Vector3(0, 0, 0);
    private Vector3 gameOverRestartButtonPos = new Vector3(0, -135, 0);
    public GameObject pistolSelectUI;
    public GameObject smgSelectUI;

    public GameObject enemies;

    public JumpOverGoomba jumpOverGoomba;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        marioRb = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();

        marioAnimator.SetBool("onGround", onGroundState);

        Time.timeScale = 0f;

        scoreText.enabled = false;
        gameOverText.enabled = false;
        restartButton.enabled = false;

        pistolSelectUI.SetActive(false);
        smgSelectUI.SetActive(false);
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
        if (col.gameObject.CompareTag("Ground") && !onGroundState)
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
        HorizontalMovement();
        VerticalMovement();

        setAnimGroundBool();
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

    void FlipSprite()
    {
        if (Input.GetKeyDown("a") && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;

            if (marioRb.velocity.x > 0.1f)
                marioAnimator.SetTrigger("onSkid");
        }

        if (Input.GetKeyDown("d") && !faceRightState)
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
        ResetGame();
        Time.timeScale = 1.0f;
    }

    public void StartButtonCallback(int input)
    {
        StartGame();
        Time.timeScale = 1.0f;
    }

    private void StartGame()
    {
        scoreText.text = "Score: 0";
        scoreText.transform.localPosition = originalScoreTextPos;
        restartButton.transform.localPosition = originalRestartButtonPos;

        scoreText.enabled = true;
        restartButton.enabled = true;

        titleLogo.enabled = false;
        startButton.enabled = false;
        startButton.transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().enabled = false; // start button text
        translucentOverlay.enabled = false;

        pistolSelectUI.SetActive(true);
        smgSelectUI.SetActive(true);
        
    }

    private void ResetGame()
    {
        marioRb.velocity = new Vector3(0, 0, 0);
        marioRb.transform.position = new Vector3(-9.27f, 1.25f, 0f);
        faceRightState = true;
        marioSprite.flipX = false;

        scoreText.text = "Score: 0";
        scoreText.transform.localPosition = originalScoreTextPos;
        restartButton.transform.localPosition = originalRestartButtonPos;
        gameOverText.enabled = false;
        translucentOverlay.enabled = false;

        foreach (Transform eachChild in enemies.transform)
        {
            eachChild.transform.localPosition = eachChild.GetComponent<EnemyMovement>().startPosition;
        }

        jumpOverGoomba.score = 0;

        // Reset Animation
        marioAnimator.SetTrigger("gameRestart");
        alive = true;
    }

    private void GameOver()
    {
        translucentOverlay.enabled = true;
        gameOverText.enabled = true;
        scoreText.transform.localPosition = gameOverScoreTextPos;
        restartButton.transform.localPosition = gameOverRestartButtonPos;

        Time.timeScale = 0f;
    }
}
