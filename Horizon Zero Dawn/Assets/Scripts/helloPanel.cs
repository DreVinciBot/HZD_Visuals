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
    public static bool startofSecondRound = false;
    public static bool endofSecondRound = false;

    static bool firstloop = true;
    static bool secondloop = true;
    static bool thirdloop = true;
    static bool finalloop = true; 

    void Update()
    {
        //End of Demp
        if(Input.GetKeyDown(KeyCode.Space) && firstloop && ComfirmGems.demo_complete)
        {
            thisPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            PlayerMovement.playerCheck = true;
            ComfirmGems.roundinsession = true;
            firstloop = false;
            startofFirstRound = true;
            Timer.timerCheck = true;
            print("2");
        }

        //start of Round 1
        if (Input.GetKeyDown(KeyCode.Space) && secondloop && startofFirstRound && ComfirmGems.round1)
        {
            thisPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            PlayerMovement.playerCheck = true;
            ComfirmGems.roundinsession = true;
            secondloop = false;

            endofFirstRound = true;
            //startofSecondRound = true;
            Timer.timerCheck = true;
            print("3");
        }

        //Start of Round 2
        if (Input.GetKeyDown(KeyCode.Space) && thirdloop && endofFirstRound && ComfirmGems.round2)
        {
            thisPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            PlayerMovement.playerCheck = true;
            ComfirmGems.roundinsession = true;
            thirdloop = false;

            startofSecondRound = true;

            endofSecondRound = true;
            Timer.timerCheck = true;
            print("7");
        }


        //End of Round 2
        if (Input.GetKeyDown(KeyCode.Space) && finalloop && endofSecondRound)
        {
            thisPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            PlayerMovement.playerCheck = true;
            ComfirmGems.roundinsession = true;
            finalloop = false;
        
            endofSecondRound = true;
            Timer.timerCheck = true;
            print("8");
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
