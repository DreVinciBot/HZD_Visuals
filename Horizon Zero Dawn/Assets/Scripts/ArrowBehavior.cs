using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PathCreation.Examples
{
    public class ArrowBehavior : MonoBehaviour
    {
        private string collidingObject = "turtlebot";
        public static bool newArrow;

        void Start()
        {
            newArrow = false;
        }

        void OnTriggerEnter(Collider other)
        {      
            if(PathPlacer.fixed_condition ||  PathPlacer.log_condition)
            {
                // If turtlebot collides with arrow, destroy the arrow
                if (other.gameObject.name == collidingObject)
                {
                    Destroy(this.gameObject);
                    newArrow = true;

                }
            }
        }
    }
}
