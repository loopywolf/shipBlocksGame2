using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SimpleCamera : MonoBehaviour
{
    Quaternion rotation;
    public Camera mainCamera;
    [SerializeField] GameObject mainGameManager;
    private GameManager gm;

    private void Start()
    {
        mainCamera = Camera.main;
        Assert.IsNotNull(mainCamera);
        gm = mainGameManager.GetComponent<GameManager>();
        Assert.IsNotNull(gm);

        //Debug.Log("Camera Start "+gm);

        //gm.SetupMission();
        //gm.DisplayMissionUi();
    }

    // Awake is called at the start
    void Awake()
    {
        rotation = transform.rotation;

        //Debug.Log("Camera Awake");
    }//awake

    internal GameManager GetGameManager()
    {
        //throw new NotImplementedException();
        return gm;
    }

    void LateUpdate()
    {
        transform.rotation = rotation;
    }//LateUpdate

}//class
