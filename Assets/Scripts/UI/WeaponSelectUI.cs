using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class WeaponSelectUI : MonoBehaviour
{

    public Image pistolSelectImage;
    public TextMeshProUGUI pistolSelectText;
    public Image smgSelectImage;
    public TextMeshProUGUI smgSelectText;

    private GameObject currentWeapon; 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HighlightCurrentWeapon();
    }

    void HighlightCurrentWeapon()
    {
        currentWeapon = transform.GetChild(0).gameObject;

        switch (currentWeapon.GetComponent<WeaponClass>().weaponName)
        {
            case "pistol":

                pistolSelectImage.color = new Color32(255, 255, 255, 255);
                pistolSelectText.color = new Color32(255, 255, 255, 255);

                smgSelectImage.color = new Color32(140, 140, 140, 255);
                smgSelectText.color = new Color32(140, 140, 140, 255);

                break;

            case "smg":

                smgSelectImage.color = new Color32(255, 255, 255, 255);
                smgSelectText.color = new Color32(255, 255, 255, 255);

                pistolSelectImage.color = new Color32(140, 140, 140, 255);
                pistolSelectText.color = new Color32(140, 140, 140, 255);

                break;
        }
    }
}
