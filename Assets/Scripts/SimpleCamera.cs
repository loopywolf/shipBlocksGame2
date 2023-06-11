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
    //zoom camera
    [SerializeField] float zoomFactor = 1.0f;
    [SerializeField] float zoomSpeed = 5.0f;
    private float originalSize = 0f;
    //private Camera thisCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        Assert.IsNotNull(mainCamera);
        originalSize = mainCamera.orthographicSize; //for zoom
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
    
    // Update is called once per frame
    void Update()
    {
        float targetSize = originalSize * zoomFactor;
        if (targetSize != mainCamera.orthographicSize)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize,targetSize, Time.deltaTime * zoomSpeed);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        { //player requests zoom
            if (zoomFactor == 1.0f)
                SetZoom(0.5f);
            else
                SetZoom(1.0f);
        }
    }//Update

    void LateUpdate()
    {
        transform.rotation = rotation;
    }//LateUpdate

    //adding zoom
    void SetZoom(float zoomFactor)
    {
        this.zoomFactor = zoomFactor;
    }

    public void CheckSensors()
    {
        gm.CheckItemSensors();
        //throw new NotImplementedException();
    }

}//class
