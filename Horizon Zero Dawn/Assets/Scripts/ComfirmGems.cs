using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComfirmGems : MonoBehaviour
{
    private string humanAgent = "HumanAgent";
    public GameObject robotAlert;
    public GameObject Collected_text;
    public GameObject Remaining_text;
    public GameObject gemHolder;
    public GameObject Completed_text;
    public GameObject Continue_Panel;
    public GameObject Random_scene;

    bool checkpoint = true;

    // Start is called before the first frame update
    void Start()
    {
        Completed_text.SetActive(false);
        Continue_Panel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !checkpoint)
        {
            Random_scene.GetComponent<RandomCase>().RandomCase_Selected();
        }

        if (gemHolder.transform.childCount == 0 && checkpoint)
        {
            checkpoint = false;
            robotAlert.SetActive(false);
            Collected_text.SetActive(false);
            Remaining_text.SetActive(false);
            Completed_text.SetActive(true);
            Continue_Panel.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == humanAgent)
        {
            robotAlert.SetActive(false);
            Collected_text.SetActive(true);
            Remaining_text.SetActive(true);
        }
    }
}
