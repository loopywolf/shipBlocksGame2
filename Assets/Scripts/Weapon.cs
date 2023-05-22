using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject BulletPrefab;
    public EventSystem eventSystem;

    // Update is called once per frame
    void Update()
    {
        if (eventSystem.IsPointerOverGameObject())
        {

        } else
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }//Update

    void Shoot()
    {
        Instantiate(BulletPrefab, firePoint.position, firePoint.rotation);
        SoundManagerScript.playSound("laser");
    }//F
}
