using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
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

    public TextMeshProUGUI damageNumberText;
    private Vector3 damageNumberTextOffset;

    private Rigidbody2D bulletRb;

    // Start is called before the first frame update
    void Start()
    {
        bulletRb = GetComponent<Rigidbody2D>();
        damageNumberTextOffset = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f) + 100f, 0);
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
        bulletRb.velocity = transform.right * bulletSpeed;
    }

    void HomingMove()
    {
        if (!isHoming) return;

        if (lockOnTarget == null)
        {
            bulletRb.velocity = transform.right * bulletSpeed;
        }
        else
        {
            Vector2 direction = (Vector2)lockOnTarget.transform.position - bulletRb.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, transform.right).z;
            bulletRb.angularVelocity = -angleChangeSpeed * rotateAmount;
            bulletRb.velocity = transform.right * bulletSpeed;
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
        if (col.transform.CompareTag("Enemy"))
        {
            TextMeshProUGUI damageNumberClone = Instantiate(damageNumberText, Camera.main.WorldToScreenPoint(col.transform.position) + damageNumberTextOffset, col.transform.rotation);
            damageNumberClone.GetComponent<DamageNumberBehavior>().damageValue = bulletDamage;

            SpawnAltBulletExplosion();

            col.gameObject.GetComponent<EnemyClass>().TakeDamage(bulletDamage);

            Camera.main.transform.GetComponent<CameraBehavior>().ShakeCamera(hitCameraShakeDuration, hitCameraShakeDuration);

            Destroy(gameObject);
        }
    }

}
