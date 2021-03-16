using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace PathCreation.Examples
{
    public class helloPanel : MonoBehaviour
    {
        public GameObject thisPanel;
        public Text button_message;
        bool state = true;

        public static bool startofFirstRound = false;
        public static bool endofFirstRound = false;
        public static bool startofSecondRound = false;
        public static bool endofSecondRound = false;
        public static string scenename;

        static bool firstloop = true;
        static bool secondloop = true;
        static bool thirdloop = true;
        static bool finalloop = true;

        void Start()
        {
            thisPanel.SetActive(true);
        }

        void Update()
        {
            //End of Demp
            if (Input.GetKeyDown(KeyCode.Space) && firstloop && ComfirmGems.demo_complete)
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

                PathPlacer.start_fixed_path = true;
                print("3");
            }

            //Start of Round 2
            else if (thirdloop && endofFirstRound && ComfirmGems.round2 && Timer.timeRemaining > 0 && SceneManager.GetActiveScene().name != RandomCase.scene_1)
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    //&& SceneManager.GetActiveScene().name == RandomCase.scene_2
                    thisPanel.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    PlayerMovement.playerCheck = true;

                    startofSecondRound = true;
                    ComfirmGems.roundinsession = true;
                    thirdloop = false;
       
                    Timer.timerCheck = true;

                    PathPlacer.start_fixed_path = true;
                    print("6");
                }
            }

            //End of Round 2
            else if (Input.GetKeyDown(KeyCode.Space) && finalloop && startofSecondRound && ComfirmGems.round3)
            {
                thisPanel.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                PlayerMovement.playerCheck = true;
                ComfirmGems.roundinsession = true;
                finalloop = false;

                endofSecondRound = true;
                //Timer.timerCheck = true;

                //call for second round of quesitions

                SceneManager.LoadScene("Survey_Questions1");

                //add the quit button here???
                print("8");
               // Debug.Log("Quit Game!");
                //Application.Quit();
            }
        }

        public void showPanel()
        {
            state = !state;
            thisPanel.SetActive(state);

            if (state)
            {
                button_message.text = "Conditions";
            }
            else
            {
                button_message.text = "Login Sreen";
            }

        }
    }
}
