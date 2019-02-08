using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;
    public List<GameObject> players;
    public Canvas c1;
    public Button restartButton;
    public Button quitButton;
    public Text p1Score;
    public Text p2Score;
    public Text cdTimer;
    public Text winnerText;
    float countdownTimer;

    PotionManager pMngr;

	// Use this for initialization
	void Start () {
        players.Add(player1);
        players.Add(player2);
        countdownTimer = 120.0f;
        restartButton.enabled = false;
        restartButton.gameObject.SetActive(false);
        restartButton.onClick.AddListener(reloadMenu);
        quitButton.enabled = false;
        quitButton.gameObject.SetActive(false);
        quitButton.onClick.AddListener(quitGame);

        pMngr = GameObject.Find("PotionManager").GetComponent<PotionManager>();

        pMngr.Modify(player1, pMngr.GenerateEffects(pMngr.p1_potions));
        pMngr.Modify(player2, pMngr.GenerateEffects(pMngr.p2_potions));
	}
	
	// Update is called once per frame
	void Update () {
        if (countdownTimer <= 0)
            declareWinner();
        else
        {
            player1SwordCollision();
            player2SwordCollision();
            
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
                    //Debug.Log(i);
                    //Debug.Log("hey1");
                    if (players[0].GetComponent<Player>().Sword.GetComponent<BoxCollider2D>().bounds.Intersects(players[i].GetComponent<Player>().GetComponent<CircleCollider2D>().bounds))
                    {
                        Debug.Log("hey");
                        players[i].GetComponent<Player>().resetPlayer();
                        players[0].GetComponent<Player>().addScore();
                        declareWinner();
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
                        declareWinner();
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
        else if(players[1].GetComponent<Player>().score > players[0].GetComponent<Player>().score)
        {
            winnerText.text = "Player 2 wins!";

        }
        else
        {
            winnerText.text = "Draw!";
        }
        restartButton.enabled = true;
        restartButton.gameObject.SetActive(true);
        quitButton.enabled = true;
        quitButton.gameObject.SetActive(true);
        
    }
    void quitGame()
    {
        Application.Quit();
    }
    void reloadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
