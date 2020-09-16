using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class CollectToken : MonoBehaviour
{
    public float duration;
    public Image fillImage;
    public GameObject robotAlert;
    public GameObject gemPrefab;
    public GameObject scoreSystem;
    public float t;
    public Vector3 spawnPoint;
    public int randXPosition;
    public int randZPosition;
    private string humanAgent = "HumanAgent";
    public static int currentScore = 0;
    public TMP_Text collected_text;
    public GameObject Collected_text;
    public GameObject Remaining_text;
    
    void Start()
    {
        robotAlert.SetActive(false);
        //currentScore = scoreSystem.GetComponent<ScoringSystem>().theScore;
    }

    void Update()
    {
        collected_text.text = "Collected: " + currentScore;
    }

    public void spawnGem()
    {
        GameObject a = Instantiate(gemPrefab)as GameObject;
        randXPosition = Random.Range(8, 21);
        randZPosition = Random.Range(8,21);
        spawnPoint = new Vector3(randXPosition, 1.2f, randZPosition);
        a.GetComponent<AnimationScript>().enabled = true;
        a.transform.position = spawnPoint;
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.name == humanAgent)
        {
            t = 0;
            fillImage.fillAmount = 0;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == humanAgent && !robotAlert.activeSelf)
        {
            StartCoroutine(Timer(duration));

            t += Time.deltaTime;
            if(t > duration)
            {
                Destroy(this.gameObject);
                //spawnGem();
                fillImage.fillAmount = 0;
          
                currentScore += 1;
              
                if(currentScore >= 3)
                {
                    robotAlert.SetActive(true);
                    Collected_text.SetActive(false);
                    Remaining_text.SetActive(false);
                    currentScore = 0;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == humanAgent)
        {
            StopAllCoroutines();
            t = 0;
            fillImage.fillAmount = 0;
        }
    }

    public IEnumerator Timer(float duration)
    {
        float startTime = Time.time;
        float value = 0;

        while (Time.time - startTime < duration)
        {
            value = t / duration;
            fillImage.fillAmount = value;
            yield return null;
        }
    }

    
}
