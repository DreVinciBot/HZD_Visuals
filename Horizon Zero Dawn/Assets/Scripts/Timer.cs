using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    public static bool timerCheck = false;
    private static float timeRemaining = 30; //seconds
    private static float ElaspedTime = 0;
    public static string Time1;
    public TMP_Text time_text;
    bool finaltime = true;

    void Update()
    {
        if (timerCheck)
        {
            finaltime = true;
            if (timeRemaining >= 0)
            {
                timeRemaining -= Time.deltaTime;
                ElaspedTime += Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                DisplayTime(timeRemaining);
                timerCheck = false;
            }
        }
        else
        {
            if (finaltime)
            {
                //rint("Time Left: " + DisplayTime(timeRemaining));
                //print("Elapsed Time: " + DisplayTime(ElaspedTime));

                //Call aws server to record time
                Time1 = DisplayTime(ElaspedTime);
                finaltime = false;
            }
        }
    }

    public void RecordTime1()
    {
        print("Elapsed Time: " + DisplayTime(ElaspedTime));
        Time1 = DisplayTime(ElaspedTime);
        StartCoroutine(Wait());
        StartCoroutine(Main.Instance.Web.RegisterUserTime1(Time1));
    }

    public string DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        time_text.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
    }
}
