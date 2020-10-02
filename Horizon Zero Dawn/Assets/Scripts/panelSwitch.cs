using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PathCreation.Examples
{
    public class panelSwitch : MonoBehaviour
    {

        public GameObject[] panels;
        public GameObject NextButton;
        public GameObject PreviousButton;
        private int pageNumber = 0;
        bool initial_run = true;

        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < panels.Length; i++)
            {
                if (i == pageNumber)
                {
                    panels[pageNumber].SetActive(true);
                }
                else
                {
                    panels[i].SetActive(false);
                }

                if (pageNumber == panels.Length && initial_run)
                {

                    NextButton.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    PlayerMovement.playerCheck = true;
                    PathPlacer.initial_call = true;
                    initial_run = false;

                    PathPlacer.revolve = true;
                    PathPlacer.start_fixed_path = true;
                    PathPlacer.start_log_path = true;



                }
                else
                {
                    //NextButton.SetActive(true);
                }

                if (pageNumber == 0 || pageNumber == panels.Length)
                {
                    PreviousButton.SetActive(false);
                }
                else
                {
                    PreviousButton.SetActive(true);
                }
            }
        }

        public void previousButton()
        {
            if (pageNumber <= 0)
            {
                pageNumber = 1;
            }
            else
            {
                pageNumber -= 1;
            }
        }

        public void nextButton()
        {
            if (pageNumber >= panels.Length)
            {
                pageNumber = panels.Length;
            }
            else
            {
                pageNumber += 1;
            }
        }
    }
}