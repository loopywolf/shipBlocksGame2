using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FadeUI : MonoBehaviour
{
    private CanvasRenderer cr;
    [SerializeField] float FadeSpeed; //fadeout is negative, FadeIn is positive
    [SerializeField] float WaitBeforeFading;

    // Start is called before the first frame update
    void Start()
    {
        cr = GetComponent<CanvasRenderer>();
        Assert.IsNotNull(cr);
    }

    // Update is called once per frame
    void Update()
    {
        float alpha = cr.GetAlpha();
        if (WaitBeforeFading > 0f)
        {
            WaitBeforeFading = WaitBeforeFading - Time.deltaTime * 1.0f;
        }
        else
        {
            alpha = alpha + FadeSpeed * Time.deltaTime;
            if (alpha < 0f) alpha = 0f; //also, we're done
            if (alpha > 1.0f) alpha = 1.0f;
            cr.SetAlpha(alpha);
        }
    }//Update

}//class
