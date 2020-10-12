using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PathCreation.Examples
{
    public class ComfirmGems : MonoBehaviour
    {
        private string humanAgent = "HumanAgent";
        public GameObject robotAlert;
        public GameObject Delivered_Obj;
        public GameObject Collected_text;
        public GameObject Remaining_text;
        public GameObject Delivered_text;
        public GameObject gemHolder;
        public GameObject Completed_text;
        public GameObject Continue_Panel;
        public GameObject Finished_Panel;
        public GameObject Random_scene;
        public GameObject Countdown;
        public GameObject TimesUp_text;
        public static int robot_counter = 0;

        public static bool demo_complete = false;
        public static bool round1 = false;
        public static bool round2 = false;
        public static bool round3 = false;
        public static bool roundinsession = true;
        public static string point_count_1;
        public static string point_count_2;
        public static string pose_1;
        public static string pose_2;


        static bool firstround = false;
        static bool secondround = false;
        static bool exitround = false;
        static bool finalround = false;
        static bool lastmessage = false;
        static bool finalmessage = false;

        static bool timerecord1 = true;
        static bool timerecord2 = true;

        public Transform[] robot_sides;

        public static int AF;
        public static int AL;
        public static int AR;
        public static int PB;
        public static int PL;
        public static int PR;

        // Start is called before the first frame update
        void Start()
        {
            pose_1 = "";
            pose_2 = "";
            Completed_text.SetActive(false);
            TimesUp_text.SetActive(false);
            Continue_Panel.SetActive(false);
            Delivered_Obj.SetActive(false);
            Finished_Panel.SetActive(false);
            robot_counter = 0;

            PlayerMovement.playerCheck = false;

            AF = 0;
            AL = 0;
            AR = 0;
            PB = 0;
            PL = 0;
            PR = 0;
        }

        void Update()
        {

            //called in DEMO to select condition
            if (Input.GetKeyDown(KeyCode.Space) && demo_complete && !firstround)
            {
                Random_scene.GetComponent<RandomCase>().RandomCase_Selected();
                PlayerMovement.playerCheck = false;
                firstround = true;
                round1 = true;
                print("1");
            }

            //Start the second round
            if (Input.GetKeyDown(KeyCode.Space) && demo_complete && helloPanel.endofFirstRound && secondround && !exitround)
            {
                print("5");
                Timer.timeRemaining = 120;
                StartCoroutine(Delay_secondround());
                round2 = true;
                secondround = false;
                exitround = true;

            }

            //Final message
            if (Input.GetKeyDown(KeyCode.Space) && helloPanel.endofSecondRound && !finalmessage && finalround)
            {
                finalmessage = true;
                Debug.Log("Quit Game!");
                Application.Quit();

            }

            if (Timer.timesup && roundinsession)
            {

                Timer.timesup = false;
                PlayerMovement.playerCheck = false;
                robotAlert.SetActive(false);
                Collected_text.SetActive(false);
                Remaining_text.SetActive(false);
                Delivered_text.SetActive(false);
                TimesUp_text.SetActive(true);

                if (helloPanel.endofFirstRound && !lastmessage)
                {
                    lastmessage = true;
                    Continue_Panel.SetActive(true);
                }
                else if (helloPanel.startofFirstRound)
                {
                    Finished_Panel.SetActive(true);
                }

                Timer.timerCheck = false;
                roundinsession = false;

                if (helloPanel.startofFirstRound && timerecord1)
                {
                    Countdown.GetComponent<Timer>().RecordTime1();
                    Countdown.GetComponent<Timer>().RecordCollected1();
                    timerecord1 = false;
                    print("4");
                    secondround = true;

                    point_count_1 = "AF " + AF.ToString() + " AL: " + AL.ToString() + " AR: " + AR.ToString() + " PB: " + PB.ToString() + " PL: " + PL.ToString() + " PR: " + PR.ToString();

                    Countdown.GetComponent<Timer>().RecordContacts1();
                    Countdown.GetComponent<Timer>().RecordSequence1();

                }

                if (helloPanel.startofSecondRound && timerecord2)
                {
                    print("7");
                    timerecord2 = false;
                    Countdown.GetComponent<Timer>().RecordTime2();
                    Countdown.GetComponent<Timer>().RecordCollected2();
                    finalround = true;
                    round3 = true;

                    point_count_2 = "AF " + AF.ToString() + " AL: " + AL.ToString() + " AR: " + AR.ToString() + " PB: " + PB.ToString() + " PL: " + PL.ToString() + " PR: " + PR.ToString();

                    Countdown.GetComponent<Timer>().RecordContacts2();
                    Countdown.GetComponent<Timer>().RecordSequence2();
                }

                if (helloPanel.endofFirstRound)
                {
                    secondround = true;
                }

                if (firstround)
                {
                    //secondround = true;
                }

                print("gems collected");
            }

            if (gemHolder.transform.childCount == 0 && roundinsession)
            {
                PlayerMovement.playerCheck = false;
                robotAlert.SetActive(false);
                Collected_text.SetActive(false);
                Remaining_text.SetActive(false);
                Delivered_text.SetActive(false);
                Completed_text.SetActive(true);

                if (!demo_complete)
                {
                    Continue_Panel.SetActive(true);
                }

                else if (helloPanel.startofFirstRound && !lastmessage)
                {
                    lastmessage = true;
                    Continue_Panel.SetActive(true);
                }

                if (helloPanel.startofSecondRound)
                {
                    Finished_Panel.SetActive(true);
                }

                Timer.timerCheck = false;
                roundinsession = false;

                if (helloPanel.startofFirstRound && timerecord1)
                {
                    Countdown.GetComponent<Timer>().RecordTime1();
                    Countdown.GetComponent<Timer>().RecordCollected1();
                    timerecord1 = false;
                    print("4");
                    secondround = true;

                    point_count_1= "AF " + AF.ToString() + " AL: " + AL.ToString() + " AR: " + AR.ToString() + " PB: " + PB.ToString() + " PL: " + PL.ToString() + " PR: " + PR.ToString();

                    Countdown.GetComponent<Timer>().RecordContacts1();
                    Countdown.GetComponent<Timer>().RecordSequence1();

                }

                if (helloPanel.startofSecondRound && timerecord2)
                {
                    print("7");
                    timerecord2 = false;
                    Countdown.GetComponent<Timer>().RecordTime2();
                    Countdown.GetComponent<Timer>().RecordCollected2();
                    finalround = true;
                    round3 = true;

                    point_count_2 = "AF " + AF.ToString() + " AL: " + AL.ToString() + " AR: " + AR.ToString() + " PB: " + PB.ToString() + " PL: " + PL.ToString() + " PR: " + PR.ToString();

                    Countdown.GetComponent<Timer>().RecordContacts2();
                    Countdown.GetComponent<Timer>().RecordSequence2();

                }

                if (helloPanel.endofFirstRound)
                {
                    secondround = true;
                }

                demo_complete = true;
                print("gems collected");
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == humanAgent)
            {

                pose_1 += String.Format("{0:0.##}", other.transform.position.x) + "," + String.Format("{0:0.##}", other.transform.position.z) + "," + String.Format("{0:0.##}", other.transform.rotation.eulerAngles.y) + "," + String.Format("{0:0.##}", transform.rotation.eulerAngles.y) + "\n";
                pose_2 += String.Format("{0:0.##}", other.transform.position.x) + "," + String.Format("{0:0.##}", other.transform.position.z) + "," + String.Format("{0:0.##}", other.transform.rotation.eulerAngles.y) + "," + String.Format("{0:0.##}", transform.rotation.eulerAngles.y) + "\n";


                robot_counter += CollectToken.currentScore;
                robotAlert.SetActive(false);
                Collected_text.SetActive(true);
                Remaining_text.SetActive(true);
                Delivered_text.SetActive(true);

                if (CollectToken.currentScore > 0)
                {
                    CollectToken.currentScore = 0;
                    Delivered_Obj.SetActive(true);
                    StartCoroutine(Wait());

                    float temp_distance = 100;
                    string closest_side = "none";

                    for (int i = 0; i < robot_sides.Length; i++)
                    {
                        float collider_distance = Vector3.Distance(other.transform.position, robot_sides[i].position);

                        if (collider_distance < temp_distance)
                        {
                            temp_distance = collider_distance;
                            closest_side = robot_sides[i].name;
                        }
                    }

                    if (closest_side == "AnteriorLeft")
                    {
                        AL += 1;
                    }
                    else if (closest_side == "AnteriorRight")
                    {
                        AR += 1;
                    }
                    else if (closest_side == "AnteriorFront")
                    {
                        AF += 1;
                    }
                    else if (closest_side == "PosteriorBack")
                    {
                        PB += 1;
                    }
                    else if (closest_side == "PosteriorLeft")
                    {
                        PL += 1;
                    }
                    else if (closest_side == "PosteriorRight")
                    {
                        PR += 1;
                    }
                }
            }
        }

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(1);
            Delivered_Obj.SetActive(false);
        }

        IEnumerator Delay_secondround()
        {
            RandomCase.SecondRound();

            yield return new WaitForSeconds(2);
        }
    }
}