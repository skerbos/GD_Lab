using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
    public float horizontalSpeed;
    public float maxSpeed;
    public float smoothVal;
    public float gravityScale;
    public float fallingGravityScale;
    public float jumpSpeed;
    public float holdJumpSpeed;
    public float jumpButtonTime;
    public float jumpTime;

    [System.NonSerialized]
    public bool alive;
    public float deathImpulse;

    public bool isFlying;
}
