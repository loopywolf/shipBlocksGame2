using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    //The purpose of this class is to manage the game state. It is housed in the main camera class
    int currentMission = 0;
    [SerializeField] GameObject mainUiCanvasObject;
    [SerializeField] GameObject missionTextObject;
    [SerializeField] GameObject missionPanelObject;
    private TextMeshProUGUI tmpugText;
    [SerializeField] GameObject missionCompleteObject;
    //mission control
    [SerializeField] GameObject goMyShip;
    private PlayerShip myShip;
    //NavigationAid
    [SerializeField] GameObject NavigationUiObject;
    //internal Transform navigationTarget = null;

    private void Awake()
    {
        tmpugText = missionTextObject.GetComponent<TextMeshProUGUI>();
        //Debug.Log("tmp " + tmp);
        //tmp.SetText( MissionText() ); //this works
        Assert.IsNotNull(tmpugText);

        myShip = goMyShip.GetComponent<PlayerShip>();
        Assert.IsNotNull(myShip);

        missionPanelObject.SetActive(false);
        missionCompleteObject.SetActive(false);
    }

    void Start()
    {
        //Canvas c = mainUiCanvas.GetComponent<Canvas>();
        //if (c == null) Debug.Log("Not Found!");
        //NavigationUiObject.GetComponent<NavigationUI>().setPlayerShip(myShip);
    }

    public void DisplayMissionUi()
    {
        int i = currentMission + 1;
        tmpugText.SetText("Mission "+i+": "+MissionText() );
        missionPanelObject.SetActive(true);
    }//F

    public string MissionText()
    {
        switch(currentMission)
        {
            case 0: return "You've gotten your hands on a very basic rocketship.. A cockpit stuck on a rocket motor.  She isn't very maneuverable or fast, but it's just enough to get from A to B. " +
                            "Your contract is to deliver cargo to planet 4 of this system.  Your equipment is crude, so just follow a heading of 381.  Good Luck!";
            default: return "No mission found";
        }//switch
    }//F

    public void hideMissionText()
    {
        //mainUiCanvasObject.SetActive(false);
        missionPanelObject.SetActive(false);
        NavigationUiObject.SetActive(true);
    }//F

    internal void SetupMission()
    {
        switch(currentMission){
            case 0: //mission1: fly ship to planetVerda
                myShip.lookingFor = "planetVerda";
                myShip.missionCompleteAt = "STARBASE";
                //Debug.Log("Ship looking for = " + myShip.lookingFor);
                break;
        }

        if (myShip.lookingFor != "")
        {
            Transform nav = GameObject.Find(myShip.lookingFor).transform;
            NavigationUiObject.GetComponent<NavigationUI>().SetTarget(nav);
            Debug.Log("navitarget " + nav);
        }
        //throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void CompleteMission()
    {
        missionCompleteObject.SetActive(true);
        //throw new NotImplementedException();
    }

    internal void NextMission()
    {
        //Selects and shows the next mission - different than start?
        //throw new NotImplementedException();
    }
}//class
