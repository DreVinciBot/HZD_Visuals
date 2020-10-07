using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PathCreation.Examples
{
    public class KeyConditions : MonoBehaviour
    {
        public void demoComplete()
        {
            //ComfirmGems.demo_complete = true;
            PlayerMovement.playerCheck = true;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("menu_to_selection_scene");
            }
        }
    }
}
