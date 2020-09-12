using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class helloPanel : MonoBehaviour
{
    public GameObject thisPanel;
    
    bool state = true;

    public void showPanel()
    {
        state = !state;
        thisPanel.SetActive(state);
    }

}
