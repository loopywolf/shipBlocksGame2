using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //The purpose of this class is to manage the game state. It is housed in the main camera class
    int currentMission = 0;
    [SerializeField] GameObject mainUiCanvasObject;
    [SerializeField] GameObject missionTextObject;
    private TextMeshProUGUI tmpugText;

    // Start is called before the first frame update
    void Start()
    {
        //Canvas c = mainUiCanvas.GetComponent<Canvas>();
        //if (c == null) Debug.Log("Not Found!");

        tmpugText = missionTextObject.GetComponent<TextMeshProUGUI>();
        //Debug.Log("tmp " + tmp);
        //tmp.SetText( MissionText() ); //this works
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DisplayMissionUi()
    {
        int i = currentMission + 1;
        tmpugText.SetText("Mission "+i+": "+MissionText() );
    }//F

    public string MissionText()
    {
        switch( currentMission)
        {
            case 0: return "You've gotten your hands on a very basic rocketship.. A cockpit stuck on a rocket motor.  She isn't very maneuverable or fast, but it's just enough to get from A to B. " +
                            "Your contract is to deliver cargo to planet 4 of this system.  Your equipment is crude, so just follow a heading of 381.  Good Luck!";
            default: return "No mission found";
        }//switch
    }//F

    public void hideMissionText()
    {
        mainUiCanvasObject.SetActive(false);
    }//F

}//class
