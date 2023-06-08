using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerShip : MonoBehaviour
{
    //Transform shipTransform;
    public float turnSpeed;
    public float moveSpeed;
    public float SlowDown;
    public float MaxSpeed;
    Rigidbody2D ShipRb2d;
    Vector3 CurrentSpeed;
    private GameManager gm;
    public string lookingFor = "";
    internal string missionCompleteAt = "";
    //private GameObject thrustParticles = null;
    private List<BloxE> EngineBlox = new List<BloxE>();

    // Start is called before the first frame update
    void Start()
    {
        //shipTransform = GetComponent<Transform>();
        ShipRb2d = GetComponent<Rigidbody2D>();
        CurrentSpeed = new Vector2(0f, 0f);
        gm = Camera.main.GetComponent<SimpleCamera>().GetGameManager(); //static const
        //thrusters display
        //Transform tp = gameObject.transform.Find("ThrustParticles");
        //Assert.IsNotNull(tp);
        //thrustParticles = tp.gameObject;
        //Assert.IsNotNull(thrustParticles);
        
        //Thrusters take 2
        foreach(Transform childTf in transform)
        {
            BloxE be = childTf.GetComponent<BloxE>();
            if (be != null)
                EngineBlox.Add(be);
        }
        Debug.Log(gameObject.name+ "..EngineBlox=" + EngineBlox);
        Debug.Log(EngineBlox.Count);
    }//Start

    // Update is called once per frame
    void Update()
    {
        float dx = Input.GetAxisRaw("Horizontal");
        float dy = Input.GetAxisRaw("Vertical");

        //Ship controls
        if (dx != 0)
            //shipTransform.eulerAngles = new Vector3(
              //  shipTransform.eulerAngles.x,
                //shipTransform.eulerAngles.y,
                //shipTransform.eulerAngles.z + turnSpeed * dx);
            ShipRb2d.MoveRotation(ShipRb2d.rotation - turnSpeed * dx);

        if (dy != 0)
        {
            /*Vector3 speed = new Vector3(
                Mathf.Cos(shipTransform.rotation.z * moveSpeed * dy),
                Mathf.Sin(shipTransform.rotation.z * moveSpeed * dy),
                0f);
            Debug.Log(speed);
            shipTransform.position = shipTransform.position + speed;*/
            //ShipRb2d.AddRelativeForce(Vector3.forward * moveSpeed * dy);
            //Debug.Log("thrusting " + ShipRb2d.transform.rotation.eulerAngles.z);
            //if( CurrentSpeed.magnitude < MaxSpeed )
            CurrentSpeed = new Vector2(
                CurrentSpeed.x + moveSpeed * dy * Mathf.Cos(ShipRb2d.transform.rotation.eulerAngles.z * Mathf.Deg2Rad),
                CurrentSpeed.y + moveSpeed * dy * Mathf.Sin(ShipRb2d.transform.rotation.eulerAngles.z * Mathf.Deg2Rad)
            );

            //display thrusters
            ShowThrust(dy>0);
        }
        else
        {
            ShowThrust(false);
        }//dy

        //Ship movement
        CurrentSpeed.x = CurrentSpeed.x * (1.0f - 1.0f/SlowDown);
        CurrentSpeed.y = CurrentSpeed.y * (1.0f - 1.0f/SlowDown);

        //was here
    }//Update

    void FixedUpdate()
    {
        Vector2 newPosition = new Vector2(
            ShipRb2d.position.x + CurrentSpeed.x,
            ShipRb2d.position.y + CurrentSpeed.y
        );
        ShipRb2d.MovePosition(newPosition); //don't need deltaTime - provided by engine
    }//FixedUpdate

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log("our ship hit " + hitInfo);
        //Debug.Log("its name is " + hitInfo.name); //planetVerda
        //Debug.Log("we are looking for " + lookingFor);
        if (hitInfo.name == lookingFor)
        {
            Debug.Log("You're done!");
            lookingFor = "";    //blank it out
            gm.CompleteMission();
        } else
        if (hitInfo.name == missionCompleteAt && lookingFor=="")
        {
//          Debug.Log("Next mission, please!");
            gm.NextMission();
        }
    }//OnTrigger

    private void ShowThrust(bool on)
    {
        //for each engine block, show thrusters yea or nay
        foreach(BloxE e in EngineBlox)
                e.ShowThrust(on);
    }//F

}//class

internal class ship2B2D
{
}