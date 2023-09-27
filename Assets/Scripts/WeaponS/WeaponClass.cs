using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponClass : MonoBehaviour
{
    public string weaponName;

    public float damage;
    public float fireRate;
    public float spread;

    public float altFireRate;

    public GameObject bullet;

    public GameObject barrel;

    public GameObject lockOnCrosshair;

    public AudioSource gunAudio;
    public AudioClip gunFire;

    public GameObject mainCamera;

    private float lastShotTime;

    Ray ray;
    RaycastHit2D hit;

    private void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
    }
    private void Update()
    {
        //Fire();
    }

    public void Fire(bool firing)
    {
        if (firing && (Time.time >= (lastShotTime + fireRate)))
        {
            GameObject bulletClone = Instantiate(bullet, barrel.transform.position, barrel.transform.rotation);
            bulletClone.transform.Rotate(0, 0, Random.Range(-spread, spread));
            lastShotTime = Time.time;

            gunAudio.PlayOneShot(gunFire);

            mainCamera.GetComponent<CameraBehavior>().ShakeCamera(0.1f, 0.2f);
        }

    }

    public void AltFire(bool altFiring, Vector2 altFiringPoint)
    {
        if (altFiring)
        {
            ray = Camera.main.ScreenPointToRay(altFiringPoint);
            hit = Physics2D.GetRayIntersection(ray, 1500f);

            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                GameObject lockOnClone = Instantiate(lockOnCrosshair, hit.collider.gameObject.transform);
                lockOnClone.GetComponent<LockOnCrosshairBehavior>().lockOnTarget = hit.collider.gameObject;
            }
        }
    }

}
