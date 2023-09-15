using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponClass : MonoBehaviour
{
    public string weaponName;

    public float damage;
    public float fireRate;
    public float spread;

    public GameObject bullet;

    public GameObject barrel;

    private float lastShotTime;

    public void fire()
    {
        if (Input.GetButton("Fire1") && (Time.time >= (lastShotTime + fireRate)))
        {
            GameObject bulletClone = Instantiate(bullet, barrel.transform.position, barrel.transform.rotation);
            bulletClone.transform.Rotate(0, 0, Random.Range(-spread, spread));
            lastShotTime = Time.time;
        }

    }

}
