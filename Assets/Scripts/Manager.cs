using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;
    public List<GameObject> players;
    public Canvas c1;
    public Text p1Score;
    public Text p2Score;
    public Text cdTimer;
    public Text winnerText;
    float countdownTimer;
	// Use this for initialization
	void Start () {
        players.Add(player1);
        players.Add(player2);
        countdownTimer = 120.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (countdownTimer <= 0)
            declareWinner();
        else
        {
            player1SwordCollision();
            player2SwordCollision();
            //swordPlayerCollisions();
            gameTimer();
        }
        displayScore();
        displayGameTimer();
       
    }

    void player1SwordCollision()
    {
        if (players[0].GetComponent<Player>().isSword)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (i != 0)
                {
                    if (players[0].GetComponent<Player>().Sword.GetComponent<BoxCollider2D>().bounds.Intersects(players[i].GetComponent<Player>().GetComponent<CircleCollider2D>().bounds))
                    {
                        Debug.Log("hey");
                        players[i].GetComponent<Player>().resetPlayer();
                        players[0].GetComponent<Player>().addScore();
                    }
                }
            }
        }
    }
    void player2SwordCollision()
    {
        if (players[1].GetComponent<Player>().isSword)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (i != 1)
                {
                    if (players[1].GetComponent<Player>().Sword.GetComponent<BoxCollider2D>().bounds.Intersects(players[i].GetComponent<Player>().GetComponent<CircleCollider2D>().bounds))
                    {
                        Debug.Log("hey1");
                        players[i].GetComponent<Player>().resetPlayer();
                        players[1].GetComponent<Player>().addScore();
                    }
                }
            }
        }
    }

    void swordPlayerCollisions()
    {
        for (int i = 0; i < players.Count; i++)
        {
            for (int j = 0; j < players.Count; j++)
            {
                if(players[i] != null && players[j] != null)
                {
                    if (players[i] != players[j])
                    {
                        if(players[j].GetComponent<Player>().Sword != null)
                        {
                            Debug.Log("hey1");
                            if (players[i].GetComponent<CircleCollider2D>().bounds.Intersects(players[j].GetComponent<Player>().Sword.GetComponent<BoxCollider2D>().bounds))
                            {
                                Debug.Log("hey");
                                //will reduce health later, for now just resets player position
                                players[i].GetComponent<Player>().resetPlayer();
                                players[i].GetComponent<Player>().destroySword();
                                players[j].GetComponent<Player>().addScore();
                            }
                        }
                    }
                }
            }
        }


    }

    void displayScore()
    {
        p1Score.text = "Player 1 Score: " + players[0].GetComponent<Player>().score;
        p2Score.text = "Player 2 Score: " + players[1].GetComponent<Player>().score;
    }

    void gameTimer()
    {
        countdownTimer -= Time.deltaTime;
    }
    void displayGameTimer()
    {
        cdTimer.text = "" + (int)countdownTimer;
    }
    void declareWinner()
    {
        if (players[0].GetComponent<Player>().score > players[1].GetComponent<Player>().score)
        {
            winnerText.text = "Player 1 wins!";
        }
        else
        {
            winnerText.text = "Player 2 wins!";
        }
    }
}
