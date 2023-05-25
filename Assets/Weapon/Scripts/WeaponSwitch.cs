using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public int weaponSwitch = 0;

    public int weaponOpend = 2;

    public bool riflePickeUp = false;

    public Pistol pistol;
    public ShotGun shotgun;
    public Rifle rifle;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
        //pistol.ammoCount.text = pistol.currentAmmo + " / " + pistol.allAmmo;
       // shotgun.ammoCount.text = shotgun.currentAmmo + " / " + shotgun.allAmmo;
        //rifle.ammoCount.text = rifle.currentAmmo + " / " + rifle.allAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        int currentWeapon = weaponSwitch;

        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (weaponSwitch >= transform.childCount- weaponOpend)
            {
                weaponSwitch = 0;
            }
            else
            {
                weaponSwitch++;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (weaponSwitch <= 0)
            {
                weaponSwitch = transform.childCount - weaponOpend;
            }
            else
            {
                weaponSwitch--;
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponSwitch = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)&& transform.childCount >=2)
        {
            weaponSwitch = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && riflePickeUp == true)
        {
            weaponSwitch = 2;
        }

        if (currentWeapon != weaponSwitch)
        {
            SelectWeapon();
        }

       // pistol.ammoCount.text = pistol.currentAmmo + " / " + pistol.allAmmo;
        //shotgun.ammoCount.text = shotgun.currentAmmo + " / " + shotgun.allAmmo;
        //rifle.ammoCount.text = rifle.currentAmmo + " / " + rifle.allAmmo;
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == weaponSwitch)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //подбор оружия

        if(collision.gameObject.tag == "Rifle" && !riflePickeUp)
        {
            weaponOpend -= 1;
            riflePickeUp = true;
            Destroy(collision.gameObject);
        }
        if (collision.GetComponent<PistolClip>())
        {
            pistol.allAmmo += pistol.magazinAmmo;
            Destroy(collision.gameObject);
        }
        if (collision.GetComponent<ShotGunShel>())
        {
            shotgun.allAmmo += shotgun.magazinAmmo;
            Destroy(collision.gameObject);
        }
        if (collision.GetComponent<RifleClip>())
        {
            rifle.allAmmo += rifle.magazinAmmo;
            Destroy(collision.gameObject);
        }
    }
}
