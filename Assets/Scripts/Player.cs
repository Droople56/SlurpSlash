using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Handle player controls and player collision
public class Player : MonoBehaviour {

    public PlayerAnimation m_playerAnimator;

    //Attributes
    public int playerNum = 0;
    public int dir = 0;
    public GameObject sprite;
    public Vector3 position;
    public Vector3 startPosition;
    public Vector3 swordPosition;
    private string controller;
    bool isRed, isBlue;
    public GameObject Sword;
    public GameObject sword1;
    public bool isSword;
    public float attackTimer;
    public float speed = 5.0f;
    public int score = 0;

    Vector3 forward;
    public float swordDistance = 1.0f;

    Vector3 input;

    public PlayerState m_playerState;
    public enum PlayerState
    {
        IDLE,
        WALK,
        ATTACK
    }
    
    public PlayerFacing m_facing;
    //public PlayerFacing Facing
    //{
    //    get
    //    {
    //        if (forward.x == 1)
    //        {
    //            m_facing = PlayerFacing.RIGHT;
    //            return m_facing;
    //        }
    //        else
    //        {
    //            m_facing = PlayerFacing.LEFT;
    //            return m_facing;
    //        }
    //    }
    //}
    public enum PlayerFacing
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    // Use this for initialization
    void Start () {

        isRed = false;
        isBlue = false;
        isSword = false;
        attackTimer = 0;
        startPosition = transform.position;
        forward = new Vector3(0, -1);
        input = new Vector2(0,0);
        Sword = Instantiate(sword1, new Vector2(transform.position.x, transform.position.y), transform.rotation);

        Sword.SetActive(false);
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

            attackTimer += Time.deltaTime;
            if (attackTimer >= 1.5f)
            {
                destroySword();
                input = new Vector2(0, 0);
            }
        }



    }

    public void addScore()
    {
        score++;
    }

    public void destroySword()
    {
        
        Sword.SetActive(false);
        attackTimer = 0.0f;
        isSword = false;
    }
    //Movement
    //For the time being there will be keyboard controls, ultimately aiming for 4 Controllers
    void Move()
    {
        if (m_playerState != PlayerState.ATTACK)
        m_playerState = PlayerState.IDLE;

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
        float horizontalInput = Input.GetAxis(controller + "Horizontal");
        float verticalInput = Input.GetAxis(controller + "Vertical");

        if (horizontalInput >= 0.1 || horizontalInput <= -0.1)
        {
            m_playerState = PlayerState.WALK;

            position.x += horizontalInput / 10;

            if (!isSword)
            {
                input.x = horizontalInput / 10;
            }

            if (horizontalInput <= -0.15)
            {
                m_facing = PlayerFacing.LEFT;
            }
            if (horizontalInput >= 0.15)
            {
                m_facing = PlayerFacing.RIGHT;
            }

            forward = new Vector3(Mathf.Cos(forward.x), Mathf.Sin(forward.x), 0.0f);
        }
        if (verticalInput >= 0.1 || verticalInput <= -0.1)
        {
            m_playerState = PlayerState.WALK;

            position.y += -verticalInput / 10;

            if (!isSword)
            {
                input.y = -verticalInput / 10;
            }

            if (-verticalInput <= -0.15)
            {
                m_facing = PlayerFacing.DOWN;
            }
            if (-verticalInput >= 0.15)
            {
                m_facing = PlayerFacing.UP;
            }
        }


        input.Normalize();

        StayOnScreen();
        
        this.transform.position = position;
        Sword.transform.position = new Vector3(transform.position.x + (input.x * swordDistance), transform.position.y + (input.y * swordDistance), 0.0f);

        Sword.transform.rotation = transform.rotation * Quaternion.Euler(new Vector3(0, 0, 90.0f));
    }



    //Attack
    void Attack()
    {
        if(Input.GetButtonDown(controller + "XboxA"))
        {
            isSword = true;
            Sword.SetActive(true);
            Sword.name = controller;
            
            m_playerState = PlayerState.ATTACK;
        }

        
    }



    public void resetPlayer()
    {
        position = startPosition;
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
                position.x += Input.GetAxis(controller + "Horizontal")*2;

            }
            if (Input.GetAxis(controller + "Vertical") >= 0.1 || Input.GetAxis(controller + "Vertical") <= -0.1)
            {
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
