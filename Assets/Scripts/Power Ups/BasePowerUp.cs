using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePowerUp : MonoBehaviour, IPowerUp
{
    public PowerUpType type;
    public bool spawned = false;
    protected bool consumed = false;
    protected bool goRight = true;
    protected Rigidbody2D rigidBody;

    // base methods
    protected virtual void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // interface methods
    // 1. concrete methods
    public PowerUpType powerUpType
    {
        get // getter
        {
            return type;
        }
    }

    public bool hasSpawned
    {
        get // getter
        {
            return spawned;
        }
    }

    public void DestroyPowerUp()
    {
        Destroy(this.gameObject);
    }

    // 2. abstract methods, must be implemented by derived classes
    public abstract void SpawnPowerUp();
    public abstract void ApplyPowerUp(MonoBehaviour i);
}
