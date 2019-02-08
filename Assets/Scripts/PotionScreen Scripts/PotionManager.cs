using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PotionManager : MonoBehaviour {
    [HideInInspector]
    public List<int> p1_potions, p2_potions, p3_potions, p4_potions;

    int[] playerEffects;

    GameObject p1_text;
    GameObject p2_text;
    GameObject currentPlayerText;

    public List<GameObject> ingredientButtons;

    int currentPlayer = 1;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        ingredientButtons = new List<GameObject>();
    }

    private void Start()
    {
        p1_potions = new List<int>();
        p2_potions = new List<int>();
        p3_potions = new List<int>();
        p4_potions = new List<int>();

        p1_text = GameObject.Find("p1_potionText");
        p2_text = GameObject.Find("p2_potionText");
        currentPlayerText = GameObject.Find("CurrentPlayerText");

    }



    void ParseIngredientList(List<int> iList, int playerNum, GameObject player_text)
    {
        string ingredients = "Player " + playerNum + " Ingredients: ";
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

        if(player_text != null) { player_text.GetComponent<Text>().text = ingredients; }
    }

    //string for debugging
    string p1_ingredients;
    string p2_ingredients;

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

        //player potions, player number, player text
        ParseIngredientList(p1_potions, 1, p1_text);
        ParseIngredientList(p2_potions, 2, p2_text);

        if (currentPlayerText != null) { currentPlayerText.GetComponent<Text>().text = "Selecting for Player: " + currentPlayer; }
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
    //1 - 3 Reds = Run Fast - Implemented
    //2 - 3 Blue = Bigger and Stronger - Implemented
    //3 - 3 Yellow = Throw Farther
    //4 - 2 Red 1 Blue = Hat
    //5 - 2 Red 1 Yellow = More Health
    //6 - 2 Blue 1 Red = Dodge - Implemented
    //7 - 2 Blue 1 Yellow = Peg Leg
    //8 - 2 Yellow 1 Red = Smaller and Weaker - Implemented
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

    public void Modify(GameObject gPlayer, int effectNum)
    {
        Player pc = gPlayer.GetComponent<Player>();
        switch (effectNum)
        {
            //increase speed
            case 1:
                pc.speed *= 1.5f;
                break;
            //decrease speed, increase size
            case 2:
                pc.speed *= 0.5f;
                pc.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                break;
            //throw farther
            case 3:
                break;
            //hat
            case 4:
                break;
            //increase health
            case 5:
                break;
            //Dodge Farther
            case 6:
                pc.dodgeFactor *= 1.5f;
                break;
            //Peg Leg
            case 7:
                break;
            //Smaller and Weaker
            case 8:
                pc.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                break;
            //50% to be hit and to hit
            case 9:
                break;
            //Invisible sometimes
            case 10:
                break;
        }
    }

    public void SwitchPlayer()
    {
        switch (currentPlayer)
        {
            case 1:
                currentPlayer = 2;
                foreach (GameObject iB in ingredientButtons)
                {
                    iB.GetComponent<PotionInfo>().currentPlayer = 2;
                }
                break;
            case 2:
                currentPlayer = 1;
                foreach (GameObject iB in ingredientButtons)
                {
                    iB.GetComponent<PotionInfo>().currentPlayer = 1;
                }
                break;
        }
    }

    public void Reset()
    {
        p1_potions.Clear();
        p2_potions.Clear();
    }
}
