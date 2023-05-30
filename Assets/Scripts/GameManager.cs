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
    private WinkUI winkUI;

    private void Awake()
    {
        tmpugText = missionTextObject.GetComponent<TextMeshProUGUI>();
        //Debug.Log("tmp " + tmp);
        //tmp.SetText( MissionText() ); //this works
        Assert.IsNotNull(tmpugText);

        myShip = goMyShip.GetComponent<PlayerShip>();
        Assert.IsNotNull(myShip);

        winkUI = NavigationUiObject.GetComponent<WinkUI>();
        Assert.IsNotNull(winkUI);

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
            case 1: //mission 2: mining ship
                myShip.lookingFor = "asteroid0";
                myShip.missionCompleteAt = "STARBASE";
                break;
        }

        setNavTarget(myShip.lookingFor);
        //throw new NotImplementedException();
    }

    public string MissionText()
    {
        switch (currentMission)
        {
            case 0:
                return "You've gotten your hands on a very basic rocketship.. A cockpit stuck on a rocket motor.  She isn't very maneuverable or fast, but it's just enough to get from A to B. " +
                        "Your contract is to deliver cargo to planet Verda of this system.  Your equipment is crude, so just follow the navigational beacon.  Good Luck!";
            case 1:
                return "A mining company needs a pilot to fly this mining ship to the nearest asteroid belt and mine out some ore.  We fed the coordinates of the likeliest spot into your "+
                    "navigational console.  Fire your mining laser with the LMB, and when you collect ore, head on back.";
            default: return "No mission found";
        }//switch
    }//F

    // Update is called once per frame
    void Update()
    {

    }

    internal void CompleteMission()
    {
        missionCompleteObject.SetActive(true);  //shows "Mission Complete"
        setNavTarget(myShip.missionCompleteAt);
        winkUI.WinkAgain();
        //throw new NotImplementedException();
    }

    internal void NextMission()
    {
        //Selects and shows the next mission - different than start?
        Debug.Log("Now for next mission!");
        currentMission++;
        SetupMission();
        DisplayMissionUi();
        //throw new NotImplementedException();
    }

    private void setNavTarget(String t)
    {
        if (t == "") return;

        GameObject go = GameObject.Find(t);
        if (go == null) Debug.Log(t + " was not found!");
        NavigationUiObject.GetComponent<NavigationUI>().SetTarget(go.transform);
        Debug.Log("navitarget " + go);
    }//F

}//class
