using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PathCreation.Examples
{
    public class ScoringSystem : MonoBehaviour
    {

        public TMP_Text remaining_text;
        public TMP_Text delivered_text;
        public TMP_Text collected_text;
        public GameObject gemHolder;

        public int theScore = 0;

        void Update()
        {
            remaining_text.text = "Remaining: " + gemHolder.transform.childCount;
            delivered_text.text = "Delivered: " + ComfirmGems.robot_counter;
        }
    }
}
