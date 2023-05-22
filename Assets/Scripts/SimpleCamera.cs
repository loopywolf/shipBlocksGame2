using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCamera : MonoBehaviour
{
    Quaternion rotation;
    public Camera mainCamera;
    [SerializeField] GameObject mainGameManager;
    private GameManager gm;

    private void Start()
    {
        mainCamera = Camera.main;
        gm = mainGameManager.GetComponent<GameManager>();
    }

    // Awake is called at the start
    void Awake()
    {
        rotation = transform.rotation;

        if(gm!=null) gm.DisplayMissionUi();
    }//awake

    void LateUpdate()
    {
        transform.rotation = rotation;
    }//LateUpdate

}//class
