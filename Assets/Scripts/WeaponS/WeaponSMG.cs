using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSMG : WeaponClass
{
    // Start is called before the first frame update
    void Start()
    {
        weaponName = "smg";
        damage = 5f;
        fireRate = 0.05f;
        spread = 7f;
        barrel = transform.Find("barrel").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }
}
