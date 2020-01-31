using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Kinematic : MonoBehaviour
{
    //position from gameobject's transform component
    //rotation from gameobject's transform component
    public Vector3 linearVelocity;
    public float angularVelocity; //In degrees
    public GameObject myTarget;
    public GameObject faceTarget;
    public Dropdown dropdown;
    public Dropdown uiDropdown;
    bool lookWhereGoing = true;

    // Update is called once per frame
    void Update()
    {
        StandardBehavior();
    }

    public void UpdatedLookUI()
    {
        switch (uiDropdown.value)
        {
            case 0:
                lookWhereGoing = true;
                break;
            case 1:
                lookWhereGoing = false;
                faceTarget = GameObject.Find("Guide1");
                break;
            case 2:
                lookWhereGoing = false;
                faceTarget = GameObject.Find("Guide2");
                break;
            case 3:
                lookWhereGoing = false;
                faceTarget = GameObject.Find("Guide3");
                break;
        }
    }

    public void UpdatedArriveUI()
    {
        switch (dropdown.value)
        {
            case 0:
                myTarget = this.gameObject;
                break;
            case 1:
                myTarget = GameObject.Find("Guide1");
                break;
            case 2:
                myTarget = GameObject.Find("Guide2");
                break;
            case 3:
                myTarget = GameObject.Find("Guide3");
                break;
        }
    }

    void UpdatePositionRotation()
    {
        //Update position and rotation
        transform.position += linearVelocity * Time.deltaTime;
        Vector3 angularIncrement = new Vector3(0, angularVelocity * Time.deltaTime, 0);
        transform.eulerAngles += angularIncrement;
    }

    void StandardBehavior()
    {
        UpdatePositionRotation();
        if (myTarget != null)
        {
            Arrive arrive = new Arrive();
            arrive.character = this;
            arrive.target = myTarget;
            SteeringOutput arriveSteering = arrive.getSteering();
            if (arriveSteering != null)
            {
                linearVelocity += arriveSteering.linear * Time.deltaTime;
                //angularVelocity += arriveSteering.angular * Time.deltaTime;
            }
        }

        if (lookWhereGoing == true)
        {
            LookWhereGoing face = new LookWhereGoing();
            face.character = this;
            face.target = faceTarget;
            SteeringOutput lookSteering = face.getSteering();
            if (lookSteering != null)
            {
                //linearVelocity += lookSteering.linear * Time.deltaTime;
                angularVelocity += lookSteering.angular * Time.deltaTime;
            }
            return;
        }

        if (faceTarget != null)
        {
            Face face = new Face();
            face.character = this;
            face.target = faceTarget;
            SteeringOutput lookSteering = face.getSteering();
            if (lookSteering != null)
            {
                //linearVelocity += lookSteering.linear * Time.deltaTime;
                angularVelocity += lookSteering.angular * Time.deltaTime;
            }
        }
    }
}
