using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class helloPanel : MonoBehaviour
{
    public GameObject thisPanel;
    public Text button_message;
    bool state = true;

    public static bool startofFirstRound = false;
    public static bool endofFirstRound = false;

    bool firstloop = true;
    bool secondloop = true;

    void Update()
    {
        //Start of Round 1
        if(Input.GetKeyDown(KeyCode.Space) && firstloop && ComfirmGems.demo_complete)
        {
            thisPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            PlayerMovement.playerCheck = true;
            ComfirmGems.roundinsession = true;
            firstloop = false;
            startofFirstRound = true;
            print("loop  1 ");
        }

        //Start of Round 2
        if (Input.GetKeyDown(KeyCode.Space) && secondloop && ComfirmGems.demo_complete)
        {
            thisPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            PlayerMovement.playerCheck = true;
            ComfirmGems.roundinsession = true;
            secondloop = false;
            endofFirstRound = true;
            print("loop  2 ");
        }

    }


    public void showPanel()
    {  
        state = !state;
        thisPanel.SetActive(state);

        if(state)
        {
            button_message.text = "Conditions";
        }
        else
        {
            button_message.text = "Login Sreen";
        }
       
    }
}
