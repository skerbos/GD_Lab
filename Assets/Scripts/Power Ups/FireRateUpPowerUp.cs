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
        //if (col.)

        // Collision with player
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
