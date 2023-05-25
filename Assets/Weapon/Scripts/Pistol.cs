using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pistol : MonoBehaviour
{
    
    public GameObject ammo;
    public Transform shotDir;

    private float timeShot;
    public float startShot;
    public int magazinAmmo = 15;
    public int currentAmmo = 15;
    public int allAmmo = 0;
    public int fullAmmo = 45;

    public float reloadTime = 2f;

    public Transform perent;
    public static Pistol instance;

    private void Awake()
    {
        if (this != null)
            instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

        HudWeapon.instance.PistolBullet.text = currentAmmo + " / " + allAmmo;
        perent = GetComponentInParent<Character>().transform;

       
        
    }

    // Update is called once per frame
    void Update()
    {
       // Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
       // float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);

        if (timeShot <= 0)
        {
            if (Input.GetMouseButton(0) && currentAmmo >0)
            {

                if (perent.transform.localScale.x > 0)
                {
                   GameObject b = Instantiate(ammo, shotDir.position, transform.rotation);
                    b.transform.position = shotDir.position;
                    b.transform.rotation = shotDir.rotation;
                }
                else
                {
                    GameObject b = Instantiate(ammo, shotDir.position, transform.rotation);
                    b.transform.position = shotDir.position;
                    b.transform.rotation = shotDir.rotation * new Quaternion(1, 1, shotDir.rotation.z + 180, 1);
                }
                timeShot = startShot;
                currentAmmo -= 1;
            }
        }
        else
        {
            timeShot -= Time.deltaTime;
        }

        HudWeapon.instance.PistolBullet.text = currentAmmo + " / " + allAmmo;
       

        if (Input.GetKeyDown(KeyCode.R) && allAmmo > 0)
        {
            Invoke("Reload", reloadTime);

        }

    }

 

    public void Reload()
    {
        int reason = magazinAmmo - currentAmmo;

        if(allAmmo>=reason)
        {
            allAmmo = allAmmo - reason;
            currentAmmo = magazinAmmo;
        }
        else
        {
            currentAmmo = currentAmmo + allAmmo;
            allAmmo = 0;
        }
    }
}
