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
    public GameObject Random_scene;
    public static int robot_counter = 0;

    public static bool demo_complete = false;
    public static bool start_round = false;
    public static bool roundinsession = true;

    static bool firstround = false;
    static bool secondround = false;

    // Start is called before the first frame update
    void Start()
    {
        Completed_text.SetActive(false);
        Continue_Panel.SetActive(false);
        Delivered_Obj.SetActive(false);
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
            start_round = true;

            print("gems 2");
        }

        if (Input.GetKeyDown(KeyCode.Space) && demo_complete && firstround && helloPanel.endofFirstRound && secondround)
        {
            RandomCase.SecondRound();
        }

        if (gemHolder.transform.childCount == 0 && roundinsession)
        {
            PlayerMovement.playerCheck = false;         
            robotAlert.SetActive(false);
            Collected_text.SetActive(false);
            Remaining_text.SetActive(false);
            Delivered_text.SetActive(false);
            Completed_text.SetActive(true);
            Continue_Panel.SetActive(true);

            roundinsession = false;

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
}