using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public GameObject pistol;
    public GameObject smg;

    private GameObject currentWeapon;

    private bool firing = false;
    private bool altFiring = false;
    private Vector2 altFiringPoint;

    private float weaponOffset = 0.5f;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = Instantiate(pistol, transform);
        currentWeapon.transform.localPosition += new Vector3(weaponOffset, 0, 0);
        player = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        UseCurrentWeaponFire();
    }

    public void AimLook(Vector2 value)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(value);
        Vector2 direction = new Vector2((mousePos.x - transform.position.x), (mousePos.y - transform.position.y));
        transform.right = direction;


        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (rotZ< 89 && rotZ > -89)
        {
            currentWeapon.transform.localRotation = Quaternion.Euler(0, 0, 0);
            player.transform.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            currentWeapon.transform.localRotation = Quaternion.Euler(180, 0, 0);
            player.transform.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void FiringCheck(bool mouseDown)
    {
        firing = mouseDown;
        Debug.Log("FIRE STATE: " + firing.ToString());
    }

    public void AltFiringCheck(bool mouseDown, Vector2 point)
    {
        altFiring = mouseDown;
        altFiringPoint = point;
    }

    void UseCurrentWeaponFire()
    {
        currentWeapon.GetComponent<WeaponClass>().Fire(firing);
    }

    void UseCurrentWeaponAltFire()
    {
        currentWeapon.GetComponent<WeaponClass>().AltFire(altFiring, altFiringPoint);
    }

    void SwitchWeapons()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && (currentWeapon.name != pistol.GetComponent<WeaponClass>().name))
        {
            Destroy(currentWeapon);
            currentWeapon = Instantiate(pistol, transform);
            currentWeapon.transform.localPosition += new Vector3(weaponOffset, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && (currentWeapon.name != smg.GetComponent<WeaponClass>().name))
        {
            Destroy(currentWeapon);
            currentWeapon = Instantiate(smg, transform);
            currentWeapon.transform.localPosition += new Vector3(weaponOffset, 0, 0);
        }
    }


}
