using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


namespace PathCreation.Examples
{
    public class Timer : MonoBehaviour
    {
        public static bool timerCheck = false;
        public static float timeRemaining; //seconds
        private static float ElaspedTime = 0;
        public static string TimeRecord;
        public static bool timesup;
        public static bool zeroTime;

        public TMP_Text time_text;
        bool finaltime = true;

        void Start()
        {
            ElaspedTime = 0;
            timeRemaining = 120;  //120 seconds
            timesup = false;
            zeroTime = false;
            timerCheck = false;
            time_text.text = string.Format("{0:02}:{1:00}", 0, 0);
        }

        void Update()
        {
            if (timerCheck)
            {
                finaltime = true;
                if (timeRemaining >= 0)
                {
                    timeRemaining -= Time.deltaTime;
                    ElaspedTime += Time.deltaTime;
                    time_text.text = DisplayTime(timeRemaining);
                }
                else
                {
                    print("remaining time: " + timeRemaining);
                    Debug.Log("Time has run out!");
                    timeRemaining = 0;
                    time_text.text = DisplayTime(timeRemaining);
                    timerCheck = false;
                    timesup = true;
                    zeroTime = true;
                }
            }
            else
            {
                float minutes = 0;
                float seconds = 0;

                if (zeroTime)
                {
                    time_text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                }
                else
                {
                    //time_text.text = string.Format("{0:02}:{1:00}", minutes, seconds);
                }

                if (finaltime)
                {
                    //rint("Time Left: " + DisplayTime(timeRemaining));
                    //print("Elapsed Time: " + DisplayTime(ElaspedTime));             
                    TimeRecord = DisplayTime(ElaspedTime);
                    finaltime = false;
                }
            }
        }

        public void RecordTime1()
        {
            print("Elapsed Time1: " + DisplayTime(ElaspedTime));
            TimeRecord = DisplayTime(ElaspedTime);
            StartCoroutine(Wait());
            StartCoroutine(Main.Instance.Web.RegisterUserTime1(TimeRecord));
        }

        public void RecordTime2()
        {
            print("Elapsed Time2: " + DisplayTime(ElaspedTime));
            TimeRecord = DisplayTime(ElaspedTime);
            StartCoroutine(Wait());
            StartCoroutine(Main.Instance.Web.RegisterUserTime2(TimeRecord));
        }

        public string DisplayTime(float timeToDisplay)
        {
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            //time_text.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        public void RecordCollected1()
        {
            StartCoroutine(Wait());
            StartCoroutine(Main.Instance.Web.RegisterUserCollected1(CollectToken.total_collected));
        }

        public void RecordCollected2()
        {
            StartCoroutine(Wait());
            StartCoroutine(Main.Instance.Web.RegisterUserCollected2(CollectToken.total_collected));
        }

        public void RecordContacts1()
        {
            StartCoroutine(Wait());
            StartCoroutine(Main.Instance.Web.RegisterUserPointContact1(ComfirmGems.point_count_1));
        }

        public void RecordContacts2()
        {
            StartCoroutine(Wait());
            StartCoroutine(Main.Instance.Web.RegisterUserPointContact2(ComfirmGems.point_count_2));
        }

        public void RecordSequence1()
        {
            StartCoroutine(Wait());
            StartCoroutine(Main.Instance.Web.RegisterUserSequence1(ComfirmGems.pose_1));
        }

        public void RecordSequence2()
        {
            StartCoroutine(Wait());
            StartCoroutine(Main.Instance.Web.RegisterUserSequence2(ComfirmGems.pose_2));
        }

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(2);
        }
    }
}