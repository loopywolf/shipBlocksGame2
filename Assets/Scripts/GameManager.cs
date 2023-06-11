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
    private WinkUI navWinkUI;
    [SerializeField] GameObject SensorUiObject;
    [SerializeField] GameObject[] premadeShips;

    private void Awake()
    {
        tmpugText = missionTextObject.GetComponent<TextMeshProUGUI>();
        //Debug.Log("tmp " + tmp);
        //tmp.SetText( MissionText() ); //this works
        Assert.IsNotNull(tmpugText);

        //myShip = goMyShip.GetComponent<PlayerShip>();
        //Assert.IsNotNull(myShip);

        navWinkUI = NavigationUiObject.GetComponent<WinkUI>();
        Assert.IsNotNull(navWinkUI);
        NavigationUiObject.SetActive(false);

        missionPanelObject.SetActive(false);
        missionCompleteObject.SetActive(false);

        //turn sensors ON //debug
        SensorUiObject.SetActive(false);
    }

    void Start()
    {
        //Canvas c = mainUiCanvas.GetComponent<Canvas>();
        //if (c == null) Debug.Log("Not Found!");
        //NavigationUiObject.GetComponent<NavigationUI>().setPlayerShip(myShip);

        ResetShips();
        EnableShip(0);//YourShip
        SetupMission();
        DisplayMissionUi();
        //debug
        CheckItemSensors();
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
        Debug.Log("SetupMission:");
        switch(currentMission){
            case 0: //mission1: fly ship to planetVerda
                EnableShip(0);//"YourShip"
                myShip.lookingFor = "planetVerda";
                myShip.missionCompleteAt = "STARBASE";
                //Debug.Log("Ship looking for = " + myShip.lookingFor);
                break;
            case 1: //mission 2: mining ship
                EnableShip(1);// "Ship2-MiningRig");
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
        navWinkUI.WinkAgain();
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

        Debug.Log("SetNavTarget:" + t);
        GameObject go = GameObject.Find(t);
        if (go == null) Debug.Log(t + " was not found!");
        NavigationUiObject.GetComponent<NavigationUI>().SetTarget(go.transform);
        Debug.Log("navitarget " + go);
    }//F

    private void CheckItemSensors()
    {
        //called when items are added to the scene, to see if we should have item sensors active
        GameObject[] anyItems = GameObject.FindGameObjectsWithTag("Item");
        if (anyItems.Length == 0) //none found 
        {
            //deactivate items sensors
            SensorUiObject.SetActive(false);
        } else {
            //activate
            SensorUiObject.SetActive(true);
            //must set the UI to point to the nearest object
            GameObject nearest = null;
            float closest = Mathf.Infinity;
            foreach(GameObject go in anyItems)
            {
                Vector3 directionToTarget = go.transform.position - myShip.transform.position;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closest)
                {
                    nearest = go;
                    closest = dSqrToTarget;
                }//if closer
            }//for list

            if (nearest != null)
            {
                Debug.Log("item go=" + nearest);
                //set the three sensor objects to the new target
                foreach (Transform child in SensorUiObject.transform)
                {
                    Debug.Log("activating " + child.gameObject);
                    //child is your child transform
                    //child.gameObject.SetActive(true);
                    NavigationUI nu = child.GetComponent<NavigationUI>();
                    nu.SetTarget(nearest.transform);
                    //nu.gameObject.SetActive(true);
                    //SensorUiObject.SetActive(true);
                }//for
            }//if

        }//if any items
        //throw new NotImplementedException();
    }//F

    private void SetItemDetector(GameObject closet)
    {

        //throw new NotImplementedException();
    }

    private void ResetShips()
    {
        //find all ships and reset them
        GameObject[] ships = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject go in ships)
            go.SetActive(false);
/*        {
            PlayerShip ps = go.GetComponent<PlayerShip>();
            ps.enabled = false;
        } */
        //throw new NotImplementedException();
    }

    private void EnableShip(int index)
    {
        Debug.Log("enable ship=" + index);
        //attack camera to ship
        SmoothFollow sf = Camera.main.GetComponent<SmoothFollow>();
        if (sf == null) return;

        if(myShip!=null)
            myShip.gameObject.SetActive(false);
        GameObject nextShip = premadeShips[index];// GameObject.Find(shipName); // "Ship2-MiningRig");
        if (nextShip == null) return;

        Debug.Log("found " + nextShip);

        nextShip.SetActive(true);
        //TODO take away the stuff below
        sf.target = nextShip.transform;
        if (myShip != null)
        {
            myShip.enabled = false;
            Debug.Log("disabled " + myShip.gameObject);
        }
        myShip = nextShip.GetComponent<PlayerShip>();
        myShip.enabled = true;
        //throw new NotImplementedException();
        Debug.Log("enabled " + myShip.gameObject);
    }//F

}//class
