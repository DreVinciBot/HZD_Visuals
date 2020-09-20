using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public static bool roundinsession = true;

    static bool firstround = false;
    static bool secondround = false;
    static bool finalround = false;
    static bool finalmessage = false;

    static bool timerecord1 = true;
    static bool timerecord2 = true;
    

    // Start is called before the first frame update
    void Start()
    {
        Completed_text.SetActive(false);
        TimesUp_text.SetActive(false);
        Continue_Panel.SetActive(false);
        Delivered_Obj.SetActive(false);
        Finished_Panel.SetActive(false);
        robot_counter = 0;

        PlayerMovement.playerCheck = false;
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
        if (Input.GetKeyDown(KeyCode.Space) && demo_complete && firstround && helloPanel.endofFirstRound && secondround)
        {
            print("5");
            StartCoroutine(Delay_secondround());
            round2 = true;
            secondround = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && helloPanel.endofSecondRound && !finalmessage && finalround)
        {
            finalmessage = true;
            Debug.Log("Quit Game!");
            Application.Quit();

        }

        if(Timer.timesup && roundinsession)
        {
            
            Timer.timesup = false;
            PlayerMovement.playerCheck = false;
            robotAlert.SetActive(false);
            Collected_text.SetActive(false);
            Remaining_text.SetActive(false);
            Delivered_text.SetActive(false);
            TimesUp_text.SetActive(true);

            if (!helloPanel.endofSecondRound)
            {
                Continue_Panel.SetActive(true);
            }
            else
            {
                Finished_Panel.SetActive(true);
            }

            Timer.timerCheck = false;
            roundinsession = false;

            if (helloPanel.startofFirstRound && timerecord1)
            {
                Countdown.GetComponent<Timer>().RecordTime1();
                timerecord1 = false;
                print("4");
            }

            if (helloPanel.startofSecondRound && timerecord2)
            {
                print("6");
                timerecord2 = false;
                Countdown.GetComponent<Timer>().RecordTime2();
            }


            if (secondround)
            {
                finalround = true;
            }


            if (firstround)
            {
                secondround = true;
            }



            demo_complete = true;
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

            if(!helloPanel.endofSecondRound)
            {
                Continue_Panel.SetActive(true);
            }
            else
            {
                Finished_Panel.SetActive(true);
            }
            
            Timer.timerCheck = false;
            roundinsession = false;

            if (helloPanel.startofFirstRound && timerecord1)
            {
                Countdown.GetComponent<Timer>().RecordTime1();
                timerecord1 = false;
                print("4");
            }

            if (helloPanel.startofSecondRound && timerecord2)
            {
                print("6");
                timerecord2 = false;
                Countdown.GetComponent<Timer>().RecordTime2();
            }      

            
            if(secondround)
            {
                finalround = true;
            }

            
            if(firstround)
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
            robot_counter += CollectToken.currentScore;
            robotAlert.SetActive(false);
            Collected_text.SetActive(true);
            Remaining_text.SetActive(true);
            Delivered_text.SetActive(true);

            if(CollectToken.currentScore > 0)
            {
                CollectToken.currentScore = 0;
                Delivered_Obj.SetActive(true);
                StartCoroutine(Wait());   
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
        yield return new WaitForSeconds(2);
        RandomCase.SecondRound();
    }

}