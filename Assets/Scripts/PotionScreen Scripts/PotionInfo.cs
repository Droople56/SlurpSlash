using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionInfo : MonoBehaviour {

    PotionManager potionMngr;
    public int ingredient;

    private void Start()
    {
        potionMngr = GameObject.Find("PotionManager").GetComponent<PotionManager>();
    }

    //handle clicks
    private void OnMouseOver()
    {
        //Left Click
        if (Input.GetMouseButtonDown(0))
        {
            //add the ingredient number to the potion list
            if (!potionMngr.p1Full())
            {
                potionMngr.p1_potions.Add(ingredient);
            }

            Debug.Log("Left Click: " + ingredient);
        }

        //Right Click
        if (Input.GetMouseButtonDown(1))
        {
            if (potionMngr.p1_potions.Contains(ingredient))
            {
                potionMngr.p1_potions.Remove(ingredient);
            }
        }
    }
}
