using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

namespace PathCreation.Examples
{

    public class RandomCase : MonoBehaviour
    {

        //public int num;
        public static string first_level;
        public static bool level_changed = false;
        public static string scene_1;
        public static string scene_2;
        public int temp_pass_id;

        public void RandomCase_Selected()
        {
            //num = Random.Range(4, 5);
            print("FirstRound");


            // Be sure to change the web url to server 
            //StartCoroutine(Main.Instance.Web.GetRequest("http://localhost/ARNavigationStudy2020/GetID.php"));

            print("chosen_id2: " + Web.pass_id);
            temp_pass_id = Web.pass_id;
            StartCoroutine(Wait(temp_pass_id));


        }

        public static void SecondRound()
        {
            if (SceneManager.GetActiveScene().name == "all_visuals_complex_scene")
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
            print("num: " + num);

            switch (num)
            {
                case 3:
                    int A = Random.Range(0, 1);

                    if (A == 0)
                    {
                        print("NS");
                        StartCoroutine(Main.Instance.Web.RegisterUserLevel("NS"));
                        SceneManager.LoadScene("no_visuals_simple_scene");
                        scene_1 = "no_visuals_simple_scene";
                        scene_2 = "no_visuals_complex_scene";
                    }
                    else
                    {
                        print("NC");
                        StartCoroutine(Main.Instance.Web.RegisterUserLevel("NC"));
                        SceneManager.LoadScene("no_visuals_complex_scene");
                        scene_1 = "no_visuals_complex_scene";
                        scene_2 = "no_visuals_simple_scene";
                    }
                    break;
                case 2:
                    int B = Random.Range(1, 2);

                    if (B == 0)
                    {
                        print("AC");
                        StartCoroutine(Main.Instance.Web.RegisterUserLevel("AC"));
                        SceneManager.LoadScene("all_visuals_complex_scene");
                        scene_1 = "all_visuals_complex_scene";
                        scene_2 = "all_visuals_simple_scene";
                    }
                    else
                    {
                        print("AS");
                        StartCoroutine(Main.Instance.Web.RegisterUserLevel("AS"));
                        SceneManager.LoadScene("all_visuals_simple_scene");
                        scene_1 = "all_visuals_simple_scene";
                        scene_2 = "all_visuals_complex_scene";
                    }
                    break;
                case 1:
                    int C = Random.Range(1, 2);

                    if (C == 0)
                    {
                        print("FC");
                        StartCoroutine(Main.Instance.Web.RegisterUserLevel("FC"));
                        SceneManager.LoadScene("fixed_visuals_complex_scene");
                        scene_1 = "fixed_visuals_complex_scene";
                        scene_2 = "fixed_visuals_simple_scene";
                    }
                    else
                    {
                        print("FS");
                        StartCoroutine(Main.Instance.Web.RegisterUserLevel("FS"));
                        SceneManager.LoadScene("fixed_visuals_simple_scene");
                        scene_1 = "fixed_visuals_simple_scene";
                        scene_2 = "fixed_visuals_complex_scene";
                    }
                    break;
                case 0:
                    int D = Random.Range(1, 2);

                    if (D == 0)
                    {
                        print("YC");
                        StartCoroutine(Main.Instance.Web.RegisterUserLevel("YC"));
                        SceneManager.LoadScene("log_visuals_complex_scene");
                        scene_1 = "log_visuals_complex_scene";
                        scene_2 = "log_visuals_simple_scene";
                    }
                    else
                    {
                        print("YS");
                        StartCoroutine(Main.Instance.Web.RegisterUserLevel("YS"));
                        SceneManager.LoadScene("log_visuals_simple_scene");
                        scene_1 = "log_visuals_simple_scene";
                        scene_2 = "log_visuals_complex_scene";
                    }
                    break;

                default:
                    print("Fail to select Level");
                    break;
            }



        }
    }
}
