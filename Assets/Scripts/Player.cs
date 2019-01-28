using System;
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
    bool isRed, isBlue;
    public GameObject Sword;
	// Use this for initialization
	void Start () {

        isRed = false;
        isBlue = false;

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
        Dodge();
        Attack();
        
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
            //Debug.Log(controller + " " + Input.GetAxis(controller + "Horizontal") + " Horizontal");
            position.x += Input.GetAxis(controller + "Horizontal") / 10;
        }
        if (Input.GetAxis(controller + "Vertical") >= 0.1 || Input.GetAxis(controller + "Vertical") <= -0.1)
        {
            //Debug.Log(controller + " " + Input.GetAxis(controller + "Vertical") + " Vertical");
            position.y += -Input.GetAxis(controller + "Vertical") / 10;
        }


        StayOnScreen();
        this.transform.position = position;
        //Debug.Log(position);
    }



    //Attack
    void Attack()
    {
        if(Input.GetButtonDown(controller + "XboxA"))
        {
            Debug.Log(isRed);
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            isRed = !isRed;
            isBlue = false;

            if (isRed)
            {
                sr.color = Color.red;
            }
            else
            {
                sr.color = Color.white;
            }

            Instantiate(Sword, this.transform);
            swordAnim();
        }
    }

    void swordAnim()
    {
        
    }

    //Dodge
    void Dodge()
    {
        if(Input.GetButtonDown(controller + "XboxB"))
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();

            isBlue = !isBlue;
            isRed = false;

            if (isBlue)
            {
                sr.color = Color.blue;
            }
            else
            {
                sr.color = Color.white;
            }
        }
    }

    void StayOnScreen()
    {
        if(position.x < -8.5f)
        {
            position.x = -8.5f;
        }
        else if(position.x > 8.5f)
        {
            position.x = 8.5f;
        }

        if(position.y < -4.5f)
        {
            position.y = -4.5f;
        }
        else if(position.y > 4.5f)
        {
            position.y = 4.5f;
        }
    }
}
