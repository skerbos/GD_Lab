using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishNormal : EnemyClass
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    public override void Move()
    {
        rb.velocity = new Vector2(-moveSpeed, Mathf.Sin(Time.fixedTime * 10));
    }

    public override void Attack()
    {
        
    }
}
