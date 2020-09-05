using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("main_scene");
        }
    }

    public void no_visuals_simple_Scene()
    {
        SceneManager.LoadScene("Simple Path A");
    }

    public void Menu_Scene()
    {
        SceneManager.LoadScene("main_scene");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}
