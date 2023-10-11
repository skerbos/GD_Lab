using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyBulletBehavior : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public float bulletLifeTime = 5f;
    public float bulletDamage = 10f;

    public bool isHoming = false;
    public float angleChangeSpeed = 20f;
    public GameObject lockOnTarget;

    public float hitCameraShakeDuration = 0.1f;
    public float hitCameraShakeIntensity = 0.1f;

    public GameObject explosionObject;
    public AudioSource bulletAudio;

    private Rigidbody2D enemyBulletRb;

    // Start is called before the first frame update
    void Start()
    {
        enemyBulletRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Move();
        HomingMove();
        Destroy(gameObject, bulletLifeTime);
    }

    void Move()
    {
        if (isHoming) return;
        enemyBulletRb.velocity = transform.right * bulletSpeed;
    }

    void HomingMove()
    {
        if (!isHoming) return;

        if (lockOnTarget == null)
        {
            enemyBulletRb.velocity = transform.right * bulletSpeed;
        }
        else
        {
            Vector2 direction = (Vector2)lockOnTarget.transform.position - enemyBulletRb.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, transform.right).z;
            enemyBulletRb.angularVelocity = -angleChangeSpeed * rotateAmount;
            enemyBulletRb.velocity = transform.right * bulletSpeed;
        }

    }

    void SpawnAltBulletExplosion()
    {
        Instantiate(explosionObject, transform.position, transform.rotation);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Obstacles") || col.transform.CompareTag("Ground") || col.gameObject.layer == 12)
        {
            SpawnAltBulletExplosion();

            Camera.main.transform.GetComponent<CameraBehavior>().ShakeCamera(hitCameraShakeDuration, hitCameraShakeDuration);

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            SpawnAltBulletExplosion();

            Camera.main.transform.GetComponent<CameraBehavior>().ShakeCamera(hitCameraShakeDuration, hitCameraShakeDuration);

            Destroy(gameObject);
        }
    }

}
