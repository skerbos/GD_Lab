using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponClass : MonoBehaviour
{
    public string weaponName;

    public float damage;
    public float fireRate;
    public float spread;
    public int bulletsPerShot = 1;

    public float altFireRate;
    public float altSpread;

    public GameObject bullet;
    public GameObject altBullet;

    public GameObject barrel;

    public GameObject lockOnCrosshair;

    public AudioSource gunAudio;
    public AudioClip gunFire;
    public AudioClip altGunFire;

    public GameObject mainCamera;

    private float lastShotTime;
    private float lastAltShotTime;
    private bool isPrimaryFiring = false;
    private bool isAltFiring = false;

    public List<GameObject> lockOnCrosshairs;
    public List<GameObject> lockOnTargets;

    private void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
    }
    private void Update()
    {

    }

    public void Fire(bool firing)
    {
        if (isAltFiring) return;

        isPrimaryFiring = firing;
        if (firing && (Time.time >= (lastShotTime + fireRate)))
        {
            for (int i = 0; i < bulletsPerShot; i++)
            {
                GameObject bulletClone = Instantiate(bullet, barrel.transform.position, barrel.transform.rotation);
                bulletClone.transform.Rotate(0, 0, Random.Range(-spread, spread));
            }
            lastShotTime = Time.time;

            gunAudio.PlayOneShot(gunFire);

            mainCamera.GetComponent<CameraBehavior>().ShakeCamera(0.1f, 0.2f);
        }

    }

    public void AltFire(bool altFiring, Vector2 altFiringPoint)
    {
        if (isPrimaryFiring) return;

        isAltFiring = altFiring;
        if (altFiring)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(altFiringPoint), Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject.CompareTag("Enemy") && !hit.collider.gameObject.GetComponent<EnemyClass>().isLockedOn)
            {
                GameObject lockOnClone = Instantiate(lockOnCrosshair, hit.collider.gameObject.transform);
                lockOnClone.GetComponent<LockOnCrosshairBehavior>().lockOnTarget = hit.collider.gameObject;
                lockOnCrosshairs.Add(lockOnClone);

                hit.collider.gameObject.GetComponent<EnemyClass>().isLockedOn = true;
                lockOnTargets.Add(hit.collider.gameObject);
            }
        }
        else 
        {
            if (lockOnTargets.Count != 0)
            {
                if (lockOnTargets[0] == null)
                {
                    lockOnTargets.Remove(lockOnTargets[0]);
                }
                if ((Time.time >= lastAltShotTime + altFireRate))
                {
                    GameObject altBulletClone = Instantiate(altBullet, barrel.transform.position, barrel.transform.rotation);
                    altBulletClone.transform.Rotate(0, 0, Random.Range(-altSpread, altSpread));
                    altBulletClone.GetComponent<BulletBehavior>().lockOnTarget = lockOnTargets[0];

                    Destroy(lockOnCrosshairs[0]);
                    lockOnCrosshairs.RemoveAt(0);

                    lockOnTargets[0].GetComponent<EnemyClass>().isLockedOn = false;
                    lockOnTargets.RemoveAt(0);

                    lastAltShotTime = Time.time;

                    gunAudio.PlayOneShot(altGunFire);

                    mainCamera.GetComponent<CameraBehavior>().ShakeCamera(0.2f, 0.3f);

                }
            }
        }
    }

}
