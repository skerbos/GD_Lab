using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerUp
{

    void DestroyPowerUp();
    void SpawnPowerUp();
    void ApplyPowerUp(MonoBehaviour i);

    PowerUpType powerUpType
    {
        get;
    }

    bool hasSpawned
    {
        get;
    }
}

public interface IPowerUpApplicable
{
    public void RequestPowerUpEffect(IPowerUp i);
}

public enum PowerUpType
{
    FireRateUp = 0,
    BulletPerShotUp = 1,
}
