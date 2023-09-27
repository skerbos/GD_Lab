using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : MonoBehaviour
{

    public float maxHealth;
    public float moveSpeed;

    private float currentHealth;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Move()
    {
        
    }

    public virtual void Attack()
    { 

    }
}
