using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateUpPowerUp : BasePowerUp
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        this.type = PowerUpType.FireRateUp;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // Collision with level walls

        // Collision with player
        if (col.gameObject.CompareTag("Player") && spawned)
        {
            // Effects & Info after player collects

            DestroyPowerUp();
        }
        else if (col.gameObject.layer == 12) // Boundary layer
        {
            Vector2 colDir = (col.transform.position - gameObject.transform.position).normalized;

            if (colDir.y > 0 || colDir.y < 0)
            {
                //hit top or bottom
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, -rigidBody.velocity.y);
            }

            if (colDir.x > 0 || colDir.x < 0)
            {
                //hit left or right
                rigidBody.velocity = new Vector2(-rigidBody.velocity.x, rigidBody.velocity.y);
            }

        }
    }

    public override void SpawnPowerUp()
    {
        spawned = true;

        // Set velocitty in a random direction
        rigidBody.velocity = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized * 5f;

    }

    public override void ApplyPowerUp(MonoBehaviour i)
    {
        
    }
}
