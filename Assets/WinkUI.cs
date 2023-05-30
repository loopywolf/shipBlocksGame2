using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class WinkUI : MonoBehaviour
{
    private CanvasRenderer cr;
    [SerializeField] float WinkDelay; 
    [SerializeField] int HowManyWinks;
    private float winkCounter = 0f;
    private int winksSoFar;

    // Start is called before the first frame update
    void Start()
    {
        cr = GetComponent<CanvasRenderer>();
        Assert.IsNotNull(cr);
        //gameObject.SetActive(false);    //starts invisible
        cr.SetAlpha(0f);//starts invisible    
        winksSoFar = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (winksSoFar >= HowManyWinks) return;//do not update after this

        winkCounter += Time.deltaTime;
        //Debug.Log("winkCounter="+winkCounter);
        if (winkCounter>WinkDelay)
        {
            winkCounter = 0f;
            //bool currentState = gameObject.activeSelf;
            float currentAlpha = cr.GetAlpha();
            //gameObject.SetActive(!currentState);   //switches it on or off
            cr.SetAlpha(1.0f - currentAlpha);   //switches on or off
            if (currentAlpha == 0f) winksSoFar++; // HowManyWinks--;
            //Debug.Log("winksSoFar=" + winksSoFar);
            //Debug.Log("OnOrOff=" + currentState);
            //Debug.Log("OnOrOff=" + currentAlpha);
        }
    }//Update

    public void WinkAgain()
    {
        winksSoFar = 0;
        winkCounter = 0f;
    }

}//class
