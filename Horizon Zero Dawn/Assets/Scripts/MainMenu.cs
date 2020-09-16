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
        bool start_round = true;


        //public GameObject DirectionChoices;


        void Start()
        {     
            if (SceneManager.GetActiveScene().name == "demo_scene")
            {
                robot.GetComponent<PathFollower>().startFollow();

            }

            //.GetComponent<PathPlacer>().showVisuals();

        }

        void Update()
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //SceneManager.LoadScene("menu_scene");
                Cursor.lockState = CursorLockMode.None;
            }

            if (Input.GetKeyDown(KeyCode.Space) && start_round)
            {
                start_round = false;
                robot.GetComponent<PathFollower>().startFollow();
                visualizations.GetComponent<PathPlacer>().showVisuals();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                //visualizations.GetComponent<PathPlacer>().showVisuals();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                //robot.GetComponent<PathFollower>().startFollow();
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