using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    public GameManager gameManager;

    public float maxHealth;
    public float moveSpeed;

    public float currentHealth;

    public float currentHealthNormalized;

    public bool isLockedOn = false;

    public bool isDead = false;

    public Vector3 startPosition;

    public Rigidbody2D rb;

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

    public void Death()
    {
        gameManager.IncreaseScore(1);

        isDead = true;
    }
}
