using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    PotionManager potionMngr;


    public void StartGame()
    {
        potionMngr = GameObject.Find("PotionManager").GetComponent<PotionManager>();
        potionMngr.Reset();
        SceneManager.LoadScene("PotionCreation");
    }
}
