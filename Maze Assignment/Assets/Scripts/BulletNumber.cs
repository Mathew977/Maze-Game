using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletNumber : MonoBehaviour
{
    [SerializeField]
    private Text bulletCount;
    private float numberBullet;
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        numberBullet = GameObject.Find("BulletCreator").GetComponent<BcWeapon>().currentAmmo;
        bulletCount.text = "Bullets: " + numberBullet.ToString();
    }
}
