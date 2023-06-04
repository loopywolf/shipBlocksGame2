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
    [SerializeField] float WinkFade;

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
        if (winkCounter>WinkDelay || true)
        {
            //winkCounter = 0f;
                //bool currentState = gameObject.activeSelf;
                //gameObject.SetActive(!currentState);   //switches it on or off

            float currentAlpha = Mathf.Sin((winkCounter + WinkDelay) *WinkFade);
            //float currentAlpha = cr.GetAlpha();
            cr.SetAlpha(currentAlpha);
            //cr.SetAlpha(1.0f - currentAlpha);   //switches on or off
            //if (currentAlpha == 0f) winksSoFar++; // HowManyWinks--;
        }
    }//Update

    public void WinkAgain()
    {
        winksSoFar = 0;
        winkCounter = 0f;
    }

}//class
