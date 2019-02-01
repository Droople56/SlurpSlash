using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;
    public List<GameObject> players;
	// Use this for initialization
	void Start () {
        players.Add(player1);
        players.Add(player2);
	}
	
	// Update is called once per frame
	void Update () {
        swordPlayerCollisions();
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
                            if (players[i].GetComponent<CircleCollider2D>().bounds.Intersects(players[j].GetComponent<Player>().Sword.GetComponent<BoxCollider2D>().bounds))
                            {
                                //will reduce health later, for now just resets player position
                                players[i].GetComponent<Player>().resetPlayer();

                            }
                        }
                    }
                }
            }
        }

        //if (Sword.GetComponent<BoxCollider2D>().bounds.Intersects(this.GetComponent<CircleCollider2D>().bounds) && Sword.name != controller)
        //{
        //    Debug.Log("OYOYOY");
        //    destroySword();
        //    position = Vector3.zero;
        //    transform.position = position;
        //}
    }
}
