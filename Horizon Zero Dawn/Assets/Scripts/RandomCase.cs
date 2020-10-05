using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class RandomCase : MonoBehaviour
{

    public int num;

    public void RandomCase_Selected()
    {        
        num = Random.Range(1, 5);
        print("FirstRound");
        StartCoroutine(Wait(num));
        StartCoroutine(Main.Instance.Web.RegisterUserLevel(num));
    }


    public static void SecondRound()
    {
        if(SceneManager.GetActiveScene().name == "all_visuals_complex_scene")
        {
            SceneManager.LoadScene("all_visuals_simple_scene");
        }

        if (SceneManager.GetActiveScene().name == "all_visuals_simple_scene")
        {
            SceneManager.LoadScene("all_visuals_complex_scene");
        }

        if (SceneManager.GetActiveScene().name == "no_visuals_simple_scene")
        {
            SceneManager.LoadScene("no_visuals_complex_scene");
        }

        if (SceneManager.GetActiveScene().name == "no_visuals_complex_scene")
        {
            SceneManager.LoadScene("no_visuals_simple_scene");
        }

        if (SceneManager.GetActiveScene().name == "fixed_visuals_complex_scene")
        {
            SceneManager.LoadScene("fixed_visuals_simple_scene");
        }

        if (SceneManager.GetActiveScene().name == "fixed_visuals_simple_scene")
        {
            SceneManager.LoadScene("fixed_visuals_complex_scene");
        }

        if (SceneManager.GetActiveScene().name == "log_visuals_complex_scene")
        {
            SceneManager.LoadScene("log_visuals_simple_scene");
        }

        if (SceneManager.GetActiveScene().name == "log_visuals_simple_scene")
        {
            SceneManager.LoadScene("log_visuals_complex_scene");
        }

    } 

    IEnumerator Wait(int num)
    {
       yield return new WaitForSeconds(2);

       switch (num)
       {
           case 4:
               int A = Random.Range(0, 2);

               if (A == 0)
               {
                    print("NS"); 
                   //SceneManager.LoadScene("all_visuals_complex_scene");
                   SceneManager.LoadScene("no_visuals_simple_scene");
               }
               else
               {
                    print("NC");
                    //SceneManager.LoadScene("all_visuals_complex_scene");
                   SceneManager.LoadScene("no_visuals_complex_scene");
               }
               break;
           case 3:
               int B = Random.Range(0, 2);

               if (B == 0)
               {
                    print("AC");
                    SceneManager.LoadScene("all_visuals_complex_scene");
                   //SceneManager.LoadScene("all_visuals_complex_scene");
               }
               else
               {
                    print("AS");
                    //SceneManager.LoadScene("all_visuals_complex_scene");
                    SceneManager.LoadScene("all_visuals_simple_scene");
               }
               break;
           case 2:
               int C = Random.Range(0, 2);

               if (C == 0)
               {
                    print("FC");
                    SceneManager.LoadScene("fixed_visuals_complex_scene");
                   //SceneManager.LoadScene("no_visuals_simple_scene");
               }
               else
               {
                    print("FS");
                    SceneManager.LoadScene("fixed_visuals_simple_scene");
                   //SceneManager.LoadScene("no_visuals_complex_scene");
               }
               break;
           case 1:
               int D = Random.Range(0, 2);

               if (D == 0)
               {
                    print("YC");
                    SceneManager.LoadScene("log_visuals_complex_scene");
                   //SceneManager.LoadScene("all_visuals_complex_scene");
               }
               else
               {
                    print("YS");
                    SceneManager.LoadScene("log_visuals_simple_scene");
                   // SceneManager.LoadScene("all_visuals_simple_scene");
               }
               break;
           case 0:
                int E = Random.Range(0, 2);

                if (E == 0)
                {
                    print("LC");
                    SceneManager.LoadScene("all_visuals_complex_scene");
                    //SceneManager.LoadScene("all_visuals_complex_scene");
                }
                else
                {
                    print("LS");
                    SceneManager.LoadScene("all_visuals_complex_scene");
                    // SceneManager.LoadScene("all_visuals_simple_scene");
                }
                break;       
           default:
                print("Fail to select Level"); 
               break;
       }    
   


    }
}
