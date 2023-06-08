using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BloxE : MonoBehaviour
{
    //Engine Block
    GameObject ThrustParticles;

    // Start is called before the first frame update
    void Start()
    {
        Transform tp = transform.Find("ThrustParticles");
        Debug.Log(gameObject + " tp=" + tp);
        if (tp != null)
            ThrustParticles = tp.gameObject;
        Assert.IsNotNull(ThrustParticles);
        //Debug.Log("ThrustParticles = " + ThrustParticles);
        ThrustParticles.SetActive(false);   //they start turned off
    }//Start

    // Update is called once per frame
    void Update()
    {
        
    }//Update

    public void ShowThrust(bool on)
    {
        //Debug.Log("see thrust?" + on);
        ThrustParticles.SetActive(on);
        //throw new NotImplementedException();
    }//F
}//class
