using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Handle player controls and player collision
public class Player : MonoBehaviour {

    //Attributes
    public int playerNum = 0;
    public int dir = 0;
    public GameObject sprite;
    public Vector3 position;
    private string controller;

	// Use this for initialization
	void Start () {
        //position = new Vector3(playerNum, 0, 0);
        switch (playerNum)
        {
            case 1:
                controller = "j1";
                break;
            case 2:
                controller = "j2";
                break;
            case 3:
                controller = "j3";
                break;
            case 4:
                controller = "j4";
                break;
            default:
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
        Move();
        this.transform.position = position;
	}

    //Movement
    //For the time being there will be keyboard controls, ultimately aiming for 4 Controllers
    void Move()
    {
        //Up
        if (Input.GetKey(KeyCode.W))
        {
            position.y += 0.1f;
        }
        //Down
        if (Input.GetKey(KeyCode.S))
        {
            position.y -= 0.1f;
        }
        //Left
        if (Input.GetKey(KeyCode.A))
        {
            position.x -= 0.1f;
        }
        //Right
        if (Input.GetKey(KeyCode.D))
        {
            position.x += 0.1f;
        }

        //Gamepad Input
        if (Input.GetAxis(controller + "Horizontal") >= 0.1 || Input.GetAxis(controller + "Horizontal") <= -0.1)
        {
            Debug.Log(controller + " " + Input.GetAxis(controller + "Horizontal") + " Horizontal");
            position.x += Input.GetAxis(controller + "Horizontal") / 10;
        }
        if (Input.GetAxis(controller + "Vertical") >= 0.1 || Input.GetAxis(controller + "Vertical") <= -0.1)
        {
            Debug.Log(controller + " " + Input.GetAxis(controller + "Vertical") + " Vertical");
            position.y += -Input.GetAxis(controller + "Vertical") / 10;
        }
    }

    //Attack
    void Attack()
    {

    }

    //Dodge
    void Dodge()
    {

    }
}
