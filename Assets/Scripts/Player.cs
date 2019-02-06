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
    public Vector3 swordPosition;
    private string controller;
    bool isRed, isBlue;
    public GameObject Sword;
    public GameObject sword1;
    public bool isSword;
    public float attackTimer;
    public float speed = 5.0f;
    private float rot = 0.0f;
    private float rot1 = 0.0f;
    Vector3 forward;
    public float swordDistance = 1.0f;

    Vector3 input;

    // Use this for initialization
    void Start () {

        isRed = false;
        isBlue = false;
        isSword = false;
        attackTimer = 0;

        forward = new Vector3(0, -1);
        input = new Vector2(0,0);

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
        if (isSword == false)
        {
            Attack();
        }
        else
        {
            Sword.transform.position = new Vector3(transform.position.x + (input.x * swordDistance),transform.position.y + (input.y * swordDistance),0.0f);

            Sword.transform.rotation = transform.rotation * Quaternion.Euler(new Vector3(0, 0, 90.0f));
            attackTimer += Time.deltaTime;
            if (attackTimer >= 1.5f)
            {
                destroySword();
                input = new Vector2(0, 0);
            }
        }
        
    }


    void destroySword()
    {
        Destroy(Sword);
        attackTimer = 0.0f;
        isSword = false;
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

        float angle = Mathf.Atan2(Input.GetAxis(controller + "Horizontal"), -Input.GetAxis(controller + "Vertical")) * Mathf.Rad2Deg;

        //Gamepad Input
        if (Input.GetAxis(controller + "Horizontal") >= 0.1 || Input.GetAxis(controller + "Horizontal") <= -0.1)
        {
            //Debug.Log(controller + " " + Input.GetAxis(controller + "Horizontal") + " Horizontal");
            position.x += Input.GetAxis(controller + "Horizontal") / 10;

            if (!isSword)
            {
                input.x = Input.GetAxis(controller + "Horizontal") / 10;
            }

            //transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle));

            forward = new Vector3(Mathf.Cos(forward.x), Mathf.Sin(forward.x), 0.0f);
        }
        if (Input.GetAxis(controller + "Vertical") >= 0.1 || Input.GetAxis(controller + "Vertical") <= -0.1)
        {
            //Debug.Log(controller + " " + Input.GetAxis(controller + "Vertical") + " Vertical");
            position.y += -Input.GetAxis(controller + "Vertical") / 10;

            if (!isSword)
            {
                input.y = -Input.GetAxis(controller + "Vertical") / 10;
            }

            //transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle));
        }


        input.Normalize();

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

            Sword=Instantiate(sword1, new Vector2(transform.position.x,transform.position.y),transform.rotation);
            Sword.name = controller;
            isSword = true;
            swordAnim();
        }


    }

    void swordAnim()
    {
        
    }

    public void resetPlayer()
    {
        position = Vector3.zero;
        transform.position = position;
    }

    //Press B to dodge
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

            if (Input.GetAxis(controller + "Horizontal") >= 0.1 || Input.GetAxis(controller + "Horizontal") <= -0.1)
            {
                //Debug.Log(controller + " " + Input.GetAxis(controller + "Horizontal") + " Horizontal");
                position.x += Input.GetAxis(controller + "Horizontal")*2;

            }
            if (Input.GetAxis(controller + "Vertical") >= 0.1 || Input.GetAxis(controller + "Vertical") <= -0.1)
            {
                //Debug.Log(controller + " " + Input.GetAxis(controller + "Vertical") + " Vertical");
                position.y += -Input.GetAxis(controller + "Vertical")*2;
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
