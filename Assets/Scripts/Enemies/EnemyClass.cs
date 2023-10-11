using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    public GameManager gameManager;
    public HUDManager hudManager;

    public float maxHealth;
    public float moveSpeed;

    public float currentHealth;

    public float currentHealthNormalized;

    public bool isLockedOn = false;

    public bool isDead = false;

    public GameObject deathParticles;

    public Vector3 startPosition;

    public Rigidbody2D rb;

    public bool hasEnteredPlayArea = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
        NormalizeHealth();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void NormalizeHealth()
    {
        currentHealthNormalized = currentHealth / maxHealth;
    }

    public void TakeDamage(float damageValue)
    {
        currentHealth -= damageValue;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Death();
        }

        NormalizeHealth();
    }

    public virtual void Move()
    {
        
    }

    public virtual void Attack()
    { 

    }

    public virtual void Death()
    {
        gameManager.IncreaseScore(10);

        GameObject deathParticleClone = Instantiate(deathParticles, transform.position, transform.rotation);
        Destroy(deathParticleClone, 0.5f);

        Camera.main.GetComponent<CameraBehavior>().ShakeCamera(0.1f, 0.5f);

        isDead = true;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (hasEnteredPlayArea) return;

        if (col.gameObject.layer == 13)
        {
            hasEnteredPlayArea = true;
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (!hasEnteredPlayArea) return;

        if (col.gameObject.layer == 13)
            Destroy(gameObject);
    }

}
