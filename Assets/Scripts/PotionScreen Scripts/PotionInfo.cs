using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionInfo : MonoBehaviour {

    PotionManager potionMngr;
    public int ingredient;
    public int currentPlayer;

    private void Start()
    {
        potionMngr = GameObject.Find("PotionManager").GetComponent<PotionManager>();
        potionMngr.ingredientButtons.Add(this.gameObject);
        currentPlayer = 1;
    }

    //handle clicks
    private void OnMouseOver()
    {
        //Left Click
        if (Input.GetMouseButtonDown(0))
        {
            if (currentPlayer == 1 && !potionMngr.p1Full())
            {
                //add the ingredient number to the potion list
                potionMngr.p1_potions.Add(ingredient);
            }
            else if(currentPlayer == 2 && !potionMngr.p2Full())
            {
                potionMngr.p2_potions.Add(ingredient);
            }

            Debug.Log("Left Click: " + currentPlayer);
        }

        //Right Click
        if (Input.GetMouseButtonDown(1))
        {
            if (currentPlayer == 1 && potionMngr.p1_potions.Contains(ingredient))
            {
                potionMngr.p1_potions.Remove(ingredient);
            }
            else if (currentPlayer == 2 && potionMngr.p2_potions.Contains(ingredient))
            {
                potionMngr.p2_potions.Remove(ingredient);
            }
        }
    }
}
