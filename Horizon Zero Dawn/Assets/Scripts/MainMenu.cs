using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PathCreation.Examples
{
    public class MainMenu : MonoBehaviour
    {
        public GameObject robot;
        public GameObject visualizations;
        bool demo_check = true;
        bool firstround_check = true;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {        
                Cursor.lockState = CursorLockMode.None;
            }

            // start the firstround 
            if (ComfirmGems.demo_complete && firstround_check && helloPanel.startofFirstRound)
            {
                //robot.GetComponent<PathFollower>().startFollow();
                visualizations.GetComponent<PathPlacer>().showVisuals();
                firstround_check = false;                
            }

            // Start Demo round after clicking the final next button
            if (PlayerMovement.playerCheck && SceneManager.GetActiveScene().name != "menu_scene" && demo_check)
            {               
                robot.GetComponent<PathFollower>().startFollow();
                demo_check = false;
            }


            if (Input.GetKeyDown(KeyCode.C))
            {
                //DirectionChoices.GetComponent<SelectionManager>().showArrows();
               // Cursor.lockState = CursorLockMode.None;
            }
        }

        public void no_visuals_simple_Scene()
        {
            SceneManager.LoadScene("no_visuals_simple_scene");
        }

        public void no_visuals_complex_Scene()
        {
            SceneManager.LoadScene("no_visuals_complex_scene");
        }

        public void all_visuals_complex_Scene()
        {
            SceneManager.LoadScene("all_visuals_complex_scene");
        }

        public void all_visuals_simple_Scene()
        {
            SceneManager.LoadScene("all_visuals_simple_scene");
        }

        public void Menu_Scene()
        {
            SceneManager.LoadScene("menu_scene");
        }

        public void Demo_Scene()
        {
            SceneManager.LoadScene("demo_scene");
        }

        public void QuitGame()
        {
            Debug.Log("Quit Game!");
            Application.Quit();
        }
    }

}