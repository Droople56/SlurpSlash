using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PotionManager : MonoBehaviour {
    [HideInInspector]
    public List<int> p1_potions, p2_potions, p3_potions, p4_potions;

    int[] playerEffects;

    GameObject text;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        p1_potions = new List<int>();
        p2_potions = new List<int>();
        p3_potions = new List<int>();
        p4_potions = new List<int>();

        text = GameObject.Find("PotionText");
    }



    void ParseIngredientList(List<int> iList)
    {
        string ingredients = "Player 1 Ingredients: ";
        int count = 0;

        //convert our list of numbers into words
        foreach(int i in iList)
        {
            switch (i)
            {
                case 1:
                    ingredients += "Red";
                    break;
                case 2:
                    ingredients += "Blue";
                    break;
                case 3:
                    ingredients += "Yellow";
                    break;
            }

            //add a parse inbetween non-last ingredients
            if(count < iList.Count - 1)
            {
                ingredients += ", ";
            }

            count++;
        }
        if (text != null) { text.GetComponent<Text>().text = ingredients; }
        
    }

    //string for debugging
    string p1_ingredients;

    //string to display player choices
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            p1_ingredients = "";

            foreach (int ingredient in p1_potions)
            {
                p1_ingredients += ingredient;
            }

            Debug.Log(p1_ingredients);
        }

        ParseIngredientList(p1_potions);
    }

    public bool p1Full()
    {
        if(p1_potions.Count == 3)
        {
            Debug.Log("P1 can't pick anymore ingredients");
            return true;
        }
        return false;
    }

    public bool p2Full()
    {
        if (p1_potions.Count == 3)
        {
            Debug.Log("P2 can't pick anymore ingredients");
            return true;
        }
        return false;
    }

    public bool p3Full()
    {
        if (p1_potions.Count == 3)
        {
            Debug.Log("P3 can't pick anymore ingredients");
            return true;
        }
        return false;
    }
    
    public bool p4Full()
    {
        if (p1_potions.Count == 3)
        {
            Debug.Log("P4 can't pick anymore ingredients");
            return true;
        }
        return false;
    }


    //Effects
    //1 - 3 Reds = Run Fast
    //2 - 3 Blue = Bigger and Stronger
    //3 - 3 Yellow = Throw Farther
    //4 - 2 Red 1 Blue = Hat
    //5 - 2 Red 1 Yellow = More Health
    //6 - 2 Blue 1 Red = Dodge
    //7 - 2 Blue 1 Yellow = Peg Leg
    //8 - 2 Yellow 1 Red = Smaller and Weaker
    //9 - 2 Yellow 1 Blue = 50% chance to hit and to be hit
    //10 - 1 Yellow 1 Red 1 Blue = Invisible

    public int GenerateEffects(List<int> player_potion)
    {
        int redCount = 0;
        int blueCount = 0;
        int yellowCount = 0;

        for(int i = 0; i < player_potion.Count; i++)
        {
            switch (player_potion[i])
            {
                case 1:
                    redCount++;
                    break;
                case 2:
                    blueCount++;
                    break;
                case 3:
                    yellowCount++;
                    break;
                default:
                    Debug.Log("Ingredient doesn't exist");
                    break;
            }
        }

        if(redCount == 3)
        {
            //Run fast
            return 1;
        }

        if(blueCount == 3)
        {
            //Bigger and Stronger
            return 2;
        }

        if(yellowCount == 3)
        {
            //throw farther
            return 3;
        }

        if(redCount == 2)
        {
            if(yellowCount == 1)
            {
                //More Health
                return 4;
            }
            else if (blueCount == 1)
            {
                //Hat
                return 5;
            }
        }

        if(blueCount == 2)
        {
            if(redCount == 1)
            {
                //dodge better
                return 6;
            }
            else if (yellowCount == 1)
            {
                //peg leg
                return 7;
            }
        }

        if(yellowCount == 2)
        {
            if(redCount == 1)
            {
                //Smaller and Weaker
                return 8;
            }
            else if(blueCount == 1)
            {
                //50% chance to hit andbe hit
                return 9;
            }
        }

        if (redCount == 1 && blueCount == 1 && yellowCount == 1)
        {
            //invisibility
            return 10;
        }

        return 0;
    }

    public void StartFightScene()
    {
        SceneManager.LoadScene("TestScene");
    }
}
