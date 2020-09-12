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

    // Start is called before the first frame update
    void Start()
    {
        Completed_text.SetActive(false);
    }

    void Update()
    {
        if (gemHolder.transform.childCount == 0)
        {
            robotAlert.SetActive(false);
            Collected_text.SetActive(false);
            Remaining_text.SetActive(false);
            Completed_text.SetActive(true);
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
