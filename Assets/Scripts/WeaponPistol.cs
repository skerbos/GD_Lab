using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPistol : WeaponClass
{
    // Start is called before the first frame update
    void Start()
    {
        weaponName = "pistol";
        damage = 10f;
        fireRate = 0.3f;
        spread = 2f;
        barrel = transform.Find("barrel").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        fire();
    }
}
