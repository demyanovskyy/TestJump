using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HudWeapon : MonoBehaviour
{
    public Text PistolBullet;
    public Text ShotGunBullet;
    public Text RifleBullet;

    public static HudWeapon instance;

    private void Awake()
    {
        if (this != null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
