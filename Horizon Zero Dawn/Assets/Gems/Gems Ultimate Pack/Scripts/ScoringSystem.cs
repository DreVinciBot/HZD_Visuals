using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoringSystem : MonoBehaviour
{

    public TMP_Text remaining_text;
    public TMP_Text collected_text;
    public GameObject gemHolder;

    public int theScore = 0;

    void Update()
    {   
        remaining_text.text = "Gems Left: " + gemHolder.transform.childCount;
        collected_text.text = "Collected: " + (25 - gemHolder.transform.childCount);
    }
}
