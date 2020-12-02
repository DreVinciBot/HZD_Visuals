using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System.Linq;

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

        //int[] sequence = new int[2000];

        void Start()
        {
            
            /*
            // Block randomization based on 8 conditions
            int[] block_order = Enumerable.Range(0, 8).ToArray();

            for (int i = 0; i < sequence.Length; i++)
            {
                if (i % 8 == 0)
                {
                    for (int t = 0; t < block_order.Length; t++)
                    {
                        int tmp = block_order[t];
                        int r = Random.Range(t, block_order.Length);
                        block_order[t] = block_order[r];
                        block_order[r] = tmp;
                    }
                }

                sequence[i] = block_order[i % 8];
            }
            */
        }

        public void RandomCase_Selected()
        {
            //num = Random.Range(4, 5);
            print("FirstRound");
            int[] sequence = new int[] { 2, 5, 1, 4, 3, 7, 6, 0, 2, 1, 5, 3, 4, 0, 7, 6, 3, 0, 5, 1, 2, 7, 4, 6, 2, 4, 0, 7, 6, 5, 1, 3, 2, 1, 0, 4, 6, 3, 7, 5, 1, 2, 5, 4, 3, 7, 0, 6, 3, 4, 0, 6, 2, 1, 5, 7, 3, 5, 4, 7, 0, 1, 2, 6, 0, 2, 4, 5, 3, 6, 7, 1, 3, 6, 1, 5, 0, 7, 2, 4, 0, 2, 1, 6, 4, 7, 5, 3, 2, 3, 4, 0, 5, 1, 6, 7, 1, 0, 2, 3, 4, 7, 6, 5, 0, 1, 5, 2, 6, 7, 4, 3, 4, 7, 5, 2, 0, 6, 3, 1, 7, 3, 5, 1, 0, 2, 6, 4, 6, 4, 2, 7, 5, 3, 0, 1, 1, 0, 6, 5, 7, 3, 4, 2, 1, 0, 3, 6, 2, 4, 7, 5, 1, 7, 5, 0, 4, 6, 3, 2, 5, 7, 1, 0, 2, 3, 4, 6, 4, 0, 3, 7, 6, 2, 1, 5, 2, 0, 1, 7, 4, 5, 6, 3, 2, 1, 3, 7, 0, 5, 6, 4, 1, 0, 4, 3, 7, 5, 6, 2, 3, 1, 6, 7, 0, 4, 2, 5, 3, 4, 0, 2, 6, 1, 5, 7, 7, 0, 6, 1, 3, 2, 4, 5, 2, 7, 5, 1, 0, 3, 4, 6, 0, 5, 1, 2, 6, 7, 4, 3, 5, 7, 1, 6, 0, 4, 2, 3, 6, 2, 1, 0, 4, 3, 7, 5, 3, 5, 7, 6, 1, 4, 0, 2, 2, 7, 4, 1, 6, 5, 0, 3, 3, 1, 0, 7, 2, 6, 5, 4, 7, 3, 6, 2, 5, 0, 4, 1, 0, 6, 7, 5, 3, 1, 4, 2, 6, 7, 0, 4, 3, 2, 1, 5, 3, 6, 7, 0, 5, 4, 2, 1, 7, 0, 5, 6, 3, 4, 2, 1, 0, 7, 6, 5, 4, 3, 2, 1, 2, 7, 4, 3, 5, 6, 0, 1, 0, 3, 6, 1, 5, 4, 2, 7, 6, 7, 5, 1, 3, 4, 0, 2, 2, 7, 6, 5, 0, 4, 3, 1, 0, 1, 3, 7, 5, 6, 2, 4, 5, 4, 1, 0, 2, 7, 6, 3, 0, 3, 7, 6, 5, 1, 4, 2, 3, 0, 1, 2, 4, 7, 5, 6, 1, 2, 7, 5, 3, 0, 4, 6, 6, 0, 2, 1, 5, 7, 4, 3, 5, 0, 4, 1, 6, 7, 2, 3, 5, 0, 3, 7, 2, 6, 4, 1, 4, 6, 1, 2, 7, 0, 3, 5, 3, 4, 1, 5, 0, 6, 7, 2, 3, 1, 5, 4, 2, 0, 6, 7, 1, 7, 5, 4, 2, 3, 6, 0, 5, 4, 6, 3, 1, 0, 2, 7, 5, 1, 2, 4, 7, 0, 6, 3, 6, 7, 2, 5, 0, 4, 3, 1, 4, 5, 3, 0, 1, 2, 6, 7, 3, 4, 0, 2, 6, 7, 1, 5, 4, 2, 3, 7, 0, 1, 6, 5, 1, 7, 6, 2, 0, 5, 4, 3, 5, 0, 3, 1, 2, 7, 4, 6, 1, 5, 2, 7, 0, 3, 4, 6, 5, 6, 2, 0, 4, 7, 1, 3, 0, 4, 7, 6, 2, 3, 1, 5, 1, 6, 3, 5, 2, 0, 7, 4, 4, 5, 1, 3, 7, 2, 6, 0, 0, 5, 3, 7, 1, 4, 2, 6, 3, 1, 7, 5, 2, 4, 0, 6, 2, 0, 1, 3, 6, 7, 4, 5, 3, 6, 4, 0, 2, 5, 7, 1, 7, 3, 6, 0, 2, 5, 1, 4, 6, 1, 5, 0, 3, 2, 4, 7, 7, 5, 1, 0, 2, 3, 6, 4, 0, 1, 3, 4, 7, 2, 6, 5, 3, 2, 7, 4, 0, 6, 5, 1, 6, 5, 4, 3, 1, 0, 2, 7, 4, 1, 7, 0, 6, 2, 3, 5, 5, 7, 3, 0, 4, 6, 2, 1, 1, 7, 5, 4, 3, 0, 2, 6, 5, 6, 3, 1, 7, 0, 4, 2, 0, 4, 5, 2, 1, 6, 7, 3, 6, 3, 2, 1, 5, 0, 4, 7, 0, 1, 7, 4, 5, 6, 3, 2, 4, 7, 6, 2, 3, 5, 1, 0, 5, 4, 1, 2, 7, 3, 6, 0, 1, 6, 3, 5, 4, 2, 0, 7, 3, 6, 5, 2, 4, 7, 0, 1, 4, 5, 3, 0, 7, 6, 1, 2, 1, 6, 0, 4, 3, 2, 7, 5, 3, 0, 4, 7, 2, 5, 1, 6, 4, 2, 0, 5, 1, 6, 3, 7, 7, 6, 1, 4, 0, 2, 3, 5, 1, 4, 5, 2, 6, 3, 0, 7, 3, 2, 7, 0, 6, 5, 1, 4, 5, 2, 6, 4, 3, 1, 0, 7, 7, 2, 6, 4, 1, 5, 0, 3, 1, 2, 5, 6, 7, 0, 4, 3, 0, 6, 1, 3, 7, 2, 5, 4, 1, 5, 7, 6, 0, 4, 3, 2, 3, 0, 2, 5, 1, 4, 6, 7, 1, 7, 5, 3, 2, 4, 6, 0, 6, 7, 3, 1, 0, 2, 4, 5, 6, 1, 5, 7, 2, 4, 0, 3, 3, 1, 0, 4, 7, 5, 6, 2, 5, 6, 3, 2, 1, 0, 4, 7, 1, 5, 6, 2, 7, 0, 4, 3, 1, 4, 6, 2, 0, 5, 7, 3, 3, 0, 5, 1, 6, 4, 7, 2, 2, 7, 3, 0, 6, 1, 4, 5, 0, 3, 4, 2, 6, 7, 5, 1, 3, 1, 2, 0, 7, 5, 4, 6, 0, 4, 2, 5, 1, 6, 7, 3, 6, 3, 5, 0, 4, 2, 7, 1, 0, 3, 7, 2, 4, 1, 6, 5, 7, 0, 3, 2, 5, 6, 4, 1, 5, 4, 2, 6, 7, 3, 0, 1, 7, 5, 1, 6, 0, 4, 2, 3, 3, 7, 0, 5, 1, 6, 4, 2, 7, 6, 3, 5, 1, 4, 0, 2, 4, 0, 2, 7, 5, 6, 3, 1, 0, 4, 6, 1, 7, 3, 5, 2, 6, 1, 2, 4, 0, 7, 3, 5, 5, 4, 7, 2, 1, 3, 0, 6, 2, 1, 6, 4, 0, 5, 7, 3, 5, 1, 4, 7, 0, 3, 2, 6, 5, 7, 4, 6, 1, 2, 0, 3, 6, 0, 1, 5, 3, 7, 4, 2, 1, 7, 4, 6, 5, 0, 3, 2, 4, 5, 0, 3, 1, 6, 2, 7, 0, 6, 7, 1, 3, 5, 4, 2, 0, 2, 4, 3, 7, 1, 5, 6, 3, 2, 6, 0, 1, 5, 7, 4, 2, 0, 6, 7, 1, 3, 4, 5, 2, 6, 7, 4, 1, 3, 0, 5, 7, 3, 0, 4, 6, 1, 2, 5, 3, 6, 2, 5, 4, 0, 1, 7, 5, 1, 4, 7, 6, 2, 3, 0, 3, 5, 2, 4, 6, 1, 7, 0, 0, 2, 6, 4, 3, 1, 7, 5, 6, 0, 5, 4, 3, 1, 2, 7, 3, 7, 6, 1, 0, 5, 4, 2, 3, 0, 1, 4, 5, 7, 6, 2, 3, 7, 1, 5, 0, 6, 2, 4, 1, 4, 5, 2, 7, 0, 3, 6, 0, 1, 5, 7, 4, 3, 2, 6, 1, 7, 3, 2, 6, 0, 5, 4, 7, 5, 3, 1, 6, 2, 4, 0, 5, 3, 2, 6, 1, 4, 0, 7, 6, 7, 3, 4, 0, 1, 5, 2, 2, 3, 5, 6, 7, 1, 0, 4, 7, 4, 3, 0, 6, 2, 1, 5, 7, 5, 0, 3, 4, 1, 6, 2, 2, 5, 3, 1, 7, 0, 6, 4, 3, 6, 5, 7, 1, 2, 4, 0, 0, 5, 2, 4, 7, 6, 1, 3, 7, 1, 0, 2, 6, 4, 3, 5, 3, 7, 6, 2, 5, 1, 4, 0, 5, 0, 1, 7, 3, 2, 4, 6, 0, 3, 1, 6, 2, 7, 5, 4, 2, 5, 1, 3, 7, 0, 6, 4, 1, 6, 5, 0, 7, 4, 3, 2, 4, 0, 6, 3, 1, 7, 2, 5, 5, 0, 2, 6, 3, 1, 7, 4, 7, 1, 3, 5, 2, 4, 6, 0, 1, 3, 4, 7, 6, 2, 5, 0, 3, 2, 6, 0, 5, 7, 4, 1, 3, 0, 2, 7, 6, 4, 5, 1, 1, 2, 7, 0, 3, 4, 5, 6, 2, 6, 5, 7, 3, 1, 0, 4, 0, 1, 6, 7, 5, 4, 3, 2, 5, 2, 6, 7, 1, 3, 4, 0, 6, 0, 5, 4, 2, 7, 3, 1, 1, 6, 0, 4, 5, 2, 3, 7, 5, 1, 2, 3, 6, 0, 7, 4, 2, 7, 1, 4, 6, 0, 3, 5, 1, 3, 7, 0, 4, 5, 6, 2, 4, 1, 6, 0, 7, 3, 2, 5, 4, 5, 2, 7, 6, 0, 3, 1, 4, 6, 0, 3, 2, 5, 7, 1, 5, 7, 0, 2, 1, 6, 3, 4, 2, 6, 3, 5, 1, 7, 4, 0, 7, 0, 2, 3, 4, 5, 1, 6, 3, 1, 2, 4, 0, 6, 5, 7, 0, 6, 1, 2, 5, 3, 7, 4, 3, 7, 0, 5, 6, 4, 1, 2, 2, 5, 4, 1, 7, 3, 0, 6, 5, 1, 6, 0, 2, 4, 7, 3, 2, 1, 6, 4, 0, 5, 3, 7, 2, 1, 7, 3, 4, 5, 6, 0, 1, 2, 4, 0, 3, 6, 7, 5, 7, 4, 5, 1, 6, 3, 2, 0, 2, 7, 3, 4, 5, 1, 6, 0, 6, 0, 1, 3, 7, 4, 2, 5, 2, 3, 1, 6, 7, 0, 4, 5, 5, 4, 2, 3, 1, 7, 6, 0, 1, 0, 4, 7, 6, 2, 3, 5, 0, 7, 1, 4, 5, 2, 6, 3, 5, 2, 6, 3, 1, 4, 0, 7, 3, 4, 0, 1, 6, 7, 2, 5, 6, 7, 2, 1, 3, 5, 0, 4, 5, 0, 6, 2, 7, 3, 4, 1, 6, 0, 1, 2, 3, 4, 7, 5, 3, 0, 6, 2, 5, 1, 4, 7, 6, 3, 4, 1, 5, 2, 7, 0, 3, 2, 6, 5, 1, 4, 7, 0, 0, 4, 2, 7, 6, 5, 3, 1, 3, 7, 2, 4, 1, 6, 5, 0, 1, 6, 7, 2, 4, 0, 3, 5, 0, 7, 1, 3, 4, 6, 2, 5, 0, 5, 6, 1, 7, 2, 4, 3, 6, 4, 1, 7, 3, 0, 2, 5, 5, 2, 7, 0, 6, 1, 3, 4, 0, 1, 2, 7, 6, 3, 5, 4, 3, 4, 5, 2, 0, 1, 6, 7, 6, 4, 3, 1, 2, 5, 7, 0, 0, 1, 2, 6, 3, 4, 5, 7, 4, 3, 2, 6, 5, 1, 7, 0, 3, 0, 7, 5, 2, 6, 1, 4, 2, 1, 7, 4, 5, 6, 0, 3, 7, 6, 2, 3, 0, 4, 1, 5, 1, 5, 3, 7, 0, 6, 2, 4, 4, 5, 0, 3, 7, 2, 6, 1, 5, 6, 4, 1, 7, 3, 0, 2, 2, 0, 5, 7, 4, 6, 3, 1, 2, 6, 4, 3, 5, 0, 1, 7, 0, 2, 3, 5, 7, 1, 6, 4, 3, 2, 1, 0, 5, 7, 6, 4, 1, 6, 7, 5, 2, 3, 0, 4, 6, 5, 2, 3, 4, 0, 7, 1, 5, 4, 1, 6, 7, 0, 3, 2, 7, 5, 2, 3, 4, 6, 1, 0, 1, 6, 7, 5, 3, 0, 2, 4, 0, 7, 2, 6, 5, 1, 4, 3, 7, 5, 3, 2, 6, 1, 4, 0, 3, 2, 0, 7, 1, 6, 5, 4, 6, 7, 3, 4, 0, 1, 5, 2, 1, 5, 0, 6, 4, 7, 2, 3, 2, 6, 3, 0, 5, 1, 7, 4, 6, 3, 4, 1, 0, 5, 7, 2, 7, 3, 5, 6, 2, 4, 0, 1, 0, 7, 3, 2, 1, 4, 5, 6, 6, 5, 3, 0, 1, 2, 4, 7, 0, 2, 7, 1, 4, 6, 3, 5, 0, 6, 4, 7, 5, 3, 1, 2, 2, 7, 5, 0, 6, 3, 4, 1, 4, 0, 2, 3, 7, 1, 6, 5 };


            // Be sure to change the web url to server 
            //StartCoroutine(Main.Instance.Web.GetRequest("http://localhost/ARNavigationStudy2020/GetID.php"));

            var seq = Enumerable.Range(0, 10);


            print("chosen_id2: " + Web.pass_id);
            temp_pass_id = sequence[Web.pass_id];

            print("condition: " + temp_pass_id);

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
                case 7:

                    print("NS");
                    StartCoroutine(Main.Instance.Web.RegisterUserLevel("NS"));
                    SceneManager.LoadScene("no_visuals_simple_scene");
                    scene_1 = "no_visuals_simple_scene";
                    scene_2 = "no_visuals_complex_scene";

                    break;

                case 6:
                    print("NC");
                    StartCoroutine(Main.Instance.Web.RegisterUserLevel("NC"));
                    SceneManager.LoadScene("no_visuals_complex_scene");
                    scene_1 = "no_visuals_complex_scene";
                    scene_2 = "no_visuals_simple_scene";

                    break;

                case 5:
                    print("AC");
                    StartCoroutine(Main.Instance.Web.RegisterUserLevel("AC"));
                    SceneManager.LoadScene("all_visuals_complex_scene");
                    scene_1 = "all_visuals_complex_scene";
                    scene_2 = "all_visuals_simple_scene";

                    break;

                case 4:
                    print("AS");
                    StartCoroutine(Main.Instance.Web.RegisterUserLevel("AS"));
                    SceneManager.LoadScene("all_visuals_simple_scene");
                    scene_1 = "all_visuals_simple_scene";
                    scene_2 = "all_visuals_complex_scene";

                    break;

                case 3:
                    print("FC");
                    StartCoroutine(Main.Instance.Web.RegisterUserLevel("FC"));
                    SceneManager.LoadScene("fixed_visuals_complex_scene");
                    scene_1 = "fixed_visuals_complex_scene";
                    scene_2 = "fixed_visuals_simple_scene";

                    break;

                case 2:
                    print("FS");
                    StartCoroutine(Main.Instance.Web.RegisterUserLevel("FS"));
                    SceneManager.LoadScene("fixed_visuals_simple_scene");
                    scene_1 = "fixed_visuals_simple_scene";
                    scene_2 = "fixed_visuals_complex_scene";

                    break;

                case 1:
                    print("YC");
                    StartCoroutine(Main.Instance.Web.RegisterUserLevel("YC"));
                    SceneManager.LoadScene("log_visuals_complex_scene");
                    scene_1 = "log_visuals_complex_scene";
                    scene_2 = "log_visuals_simple_scene";

                    break;

                case 0:
                    print("YS");
                    StartCoroutine(Main.Instance.Web.RegisterUserLevel("YS"));
                    SceneManager.LoadScene("log_visuals_simple_scene");
                    scene_1 = "log_visuals_simple_scene";
                    scene_2 = "log_visuals_complex_scene";
                   
                    break;

                default:
                    print("Fail to select Level");
                    break;
            }



        }
    }
}
