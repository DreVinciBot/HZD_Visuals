using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class helloPanel : MonoBehaviour
{
    public GameObject thisPanel;
    bool state = true;

    public void showPanel()
    {
        if(thisPanel.activeSelf)
        {
            state = !state;
            thisPanel.SetActive(state);
        }

        else if(thisPanel.activeSelf)
        {
            SceneManager.LoadScene("demo_scene");
        }

        else
        {
            int num = Random.Range(0, 5);
            switch (num)
            {
                case 4:
                    int A = Random.Range(0, 2);

                    if (A == 0)
                    {
                        SceneManager.LoadScene("no_visuals_simple_scene");
                    }
                    else
                    {
                        SceneManager.LoadScene("no_visuals_complex_scene");
                    }
                    break;
                case 3:
                    int B = Random.Range(0, 2);

                    if (B == 0)
                    {
                        SceneManager.LoadScene("all_visuals_complex_scene");
                    }
                    else
                    {
                        SceneManager.LoadScene("all_visuals_simple_scene");
                    }
                    break;
                case 2:
                    int C = Random.Range(0, 2);

                    if (C == 0)
                    {
                        SceneManager.LoadScene("no_visuals_simple_scene");
                    }
                    else
                    {
                        SceneManager.LoadScene("no_visuals_complex_scene");
                    }
                    break;
                case 1:
                    int D = Random.Range(0, 2);

                    if (D == 0)
                    {
                        SceneManager.LoadScene("all_visuals_complex_scene");
                    }
                    else
                    {
                        SceneManager.LoadScene("all_visuals_simple_scene");
                    }
                    break;
                case 0:
                    print("Ulg, glib, Pblblblblb");
                    break;
                default:
                    print("Incorrect intelligence level.");
                    break;
            }
        }
    }
}
