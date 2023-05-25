using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleWeapon : Character
{
    public float rHandcorrectPosition = 0f;
    public Vector3 tempRHandpos;
    public Transform weaponHand;
    public Transform weaponRotation;
    public Transform offHandIK;
    public List<Weapon> weapons = new List<Weapon>();

    private Weapon[] currentWeapons;
    private GameObject currentWeapon;

    public int weaponSwitch = 0;

    public int weaponOpend = 2;

    public bool riflePickeUp = false;
    public bool shotGunPickeUp = false;
    public bool pistolPickeUp = false;
    /*
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        foreach(Weapon weapon in weapons)
        {
            Instantiate(weapon);
        }
        currentWeapons = FindObjectsOfType<Weapon>();
        foreach(Weapon weapon in currentWeapons)
        {
            weapon.gameObject.transform.SetParent(transform);
            weapon.gameObject.SetActive(false);
        }
        ChangeWeapon();
    }
    */
 
    protected override void Initializtion()
    {
        base.Initializtion();
        foreach (Weapon weapon in weapons)
        {
            Instantiate(weapon);
        }
        currentWeapons = FindObjectsOfType<Weapon>();
        foreach (Weapon weapon in currentWeapons)
        {
            weapon.gameObject.transform.SetParent(transform);
            weapon.gameObject.SetActive(false);
        }
        /* if (currentWeapon == null)
         {
             currentWeapons[0].gameObject.SetActive(true);
             currentWeapon = currentWeapons[0].gameObject;
             SetAnimator();
         }*/
        tempRHandpos = new Vector3(weaponHand.position.x, weaponHand.position.y, weaponHand.position.z);
        HudWeapon.instance.PistolBullet.text = Pistol.instance.currentAmmo + " / " + Pistol.instance.allAmmo;
        HudWeapon.instance.ShotGunBullet.text = ShotGun.instance.currentAmmo + " / " + ShotGun.instance.allAmmo;
        HudWeapon.instance.RifleBullet.text = Rifle.instance.currentAmmo + " / " + Rifle.instance.allAmmo;
    }


    private void Update()
    {
        ManageInput();
        tempRHandpos = new Vector3(weaponHand.position.x, weaponHand.position.y, weaponHand.position.z);

    }


    private void LateUpdate()
    {
        ManagePlacement();
    }

    private void ManageInput()
    {
        if(weaponOpend> currentWeapons.Length) return;
            int currentWeapon = weaponSwitch;

            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (weaponSwitch >= currentWeapons.Length - weaponOpend)
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
                    weaponSwitch = currentWeapons.Length - weaponOpend;
                }
                else
                {
                    weaponSwitch--;
                }
            }

        ChangeWeapon2(weaponSwitch);

      /*  if(Input.GetKeyDown(KeyCode.R))
        {
            ChangeWeapon();
        }*/

    }



    private void ChangeWeapon2(int w)
    {

        for (int i = 0; i < currentWeapons.Length; i++)
        {
            if(i!=w)    
            currentWeapons[i].gameObject.SetActive(false);
        }
        if (currentWeapons[w].gameObject != currentWeapon)
        {
            currentWeapons[w].gameObject.SetActive(true);
            currentWeapon = currentWeapons[w].gameObject;
            SetAnimator();
        }

    }


    private void ChangeWeapon()
    {
        for (int i = 0; i < currentWeapons.Length; i++)
        {
            if (currentWeapon == null)
            {
                currentWeapons[0].gameObject.SetActive(true);
                currentWeapon = currentWeapons[0].gameObject;
                SetAnimator();
                return;
            }
            else
            {
                currentWeapons[i].gameObject.SetActive(false);
                if (currentWeapons[i].gameObject == currentWeapon)
                {
                    i++;
                    if (i == currentWeapons.Length)
                    {
                        i = 0;
                    }
                    currentWeapons[i].gameObject.SetActive(true);
                    currentWeapon = currentWeapons[i].gameObject;
                    SetAnimator();
                }
            }
        }
    }

    private void ManagePlacement()
    {
        if (currentWeapon != null)
        {

            currentWeapon.transform.position = weaponHand.position;

            offHandIK.position = currentWeapon.GetComponent<Weapon>().offHandPlacement.position;
           
            currentWeapon.transform.rotation = weaponRotation.rotation;
        }
    }

    private void SetAnimator()
    {
        if (currentWeapon.GetComponent<Weapon>().weaponType == Weapon.TypeOfWeapon.Pistol)
        {
            anim.SetBool("Pistol", true);
            anim.SetBool("Rifle", false);
        }
        if (currentWeapon.GetComponent<Weapon>().weaponType == Weapon.TypeOfWeapon.Rifle)
        {
            anim.SetBool("Rifle", true);
            anim.SetBool("Pistol", false);
            weaponHand.position = new Vector3(weaponHand.position.x + rHandcorrectPosition, weaponHand.position.y, weaponHand.position.z);
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        //подбор оружия
        if (collision.gameObject.tag == "Rifle")
        {
            weaponOpend -= 1;
            riflePickeUp = true;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "ShotGun")
        {
            weaponOpend -= 1;
            shotGunPickeUp = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Pistol")
        {
            weaponOpend -= 1;
            pistolPickeUp = true;
            Destroy(collision.gameObject);
        }

        if (collision.GetComponent<PistolClip>())
        {
            Pistol.instance.allAmmo += Pistol.instance.magazinAmmo;
            HudWeapon.instance.PistolBullet.text = Pistol.instance.currentAmmo + " / " + Pistol.instance.allAmmo;
            Destroy(collision.gameObject);
        }
        if (collision.GetComponent<ShotGunShel>())
        {
            ShotGun.instance.allAmmo += ShotGun.instance.magazinAmmo;
            HudWeapon.instance.ShotGunBullet.text = ShotGun.instance.currentAmmo + " / " + ShotGun.instance.allAmmo;
            Destroy(collision.gameObject);
        }
        if (collision.GetComponent<RifleClip>())
        {
            Rifle.instance.allAmmo += Rifle.instance.magazinAmmo;
            HudWeapon.instance.RifleBullet.text = Rifle.instance.currentAmmo + " / " + Rifle.instance.allAmmo;
            Destroy(collision.gameObject);
        }
    }
   
        
        
}
