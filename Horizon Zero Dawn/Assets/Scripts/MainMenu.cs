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
        public GameObject DirectionChoices;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("menu_scene");
                Cursor.lockState = CursorLockMode.None;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                robot.GetComponent<PathFollower>().startFollow();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                visualizations.GetComponent<PathPlacer>().showVisuals();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                robot.GetComponent<PathFollower>().startFollow();
                DirectionChoices.GetComponent<SelectionManager>().showArrows();
                Cursor.lockState = CursorLockMode.None;
            }
        }

        public void no_visuals_simple_Scene()
        {
            SceneManager.LoadScene("Simple Path A");
        }

        public void Menu_Scene()
        {
            SceneManager.LoadScene("menu_scene");
        }

        public void QuitGame()
        {
            Debug.Log("Quit Game!");
            Application.Quit();
        }
    }

}