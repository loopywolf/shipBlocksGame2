using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationUI : MonoBehaviour
{
    Transform target = null;
    [SerializeField] GameObject playerShip;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);   //starts invisible
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return; //can't do nothing

        //find angle to target
        float course = Mathf.Atan2(
            target.position.y - playerShip.transform.position.y,
            target.position.x - playerShip.transform.position.x) * Mathf.Rad2Deg; //YUP!
        //Debug.Log("player ship x=" + playerShip.transform.position.x + " y=" + playerShip.transform.position.y + " Target x=" + target.position.x + " y=" + target.position.y);
        //float course = 90f + Vector3.Angle(playerShip.transform.position, target.position);
        //float course = 0f;
        //transform.Rotate(0, 0, course);
        transform.rotation = Quaternion.Euler(0f, 0f, course);
        //Debug.Log("course = " + course);
    }

    internal void SetTarget(Transform nav)
    {
        target = nav;
        //throw new NotImplementedException();
    }

}//class
