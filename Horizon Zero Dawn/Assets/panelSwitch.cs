﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panelSwitch : MonoBehaviour
{

    public GameObject[] panels;
    private int pageNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (i == pageNumber)
            {
                panels[pageNumber].SetActive(true);
            }
            else
            {
                panels[i].SetActive(false);
            }
        }
    }

    public void previousButton()
    {
        if (pageNumber <= 0)
        {
            pageNumber = 1;
        }
        else
        {
            pageNumber -= 1;
        }
    }

    public void nextButton()
    {
        if (pageNumber >= panels.Length)
        {
            pageNumber = panels.Length;
        }
        else
        {
            pageNumber += 1;
        }
    }
}
