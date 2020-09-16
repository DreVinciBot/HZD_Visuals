using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class helloPanel : MonoBehaviour
{
    public GameObject thisPanel;
    public Text button_message;
    bool state = true;

    public void showPanel()
    {  
        state = !state;
        thisPanel.SetActive(state);

        if(state)
        {
            button_message.text = "Conditions";
        }
        else
        {
            button_message.text = "Login Sreen";
        }
       
    }
}
