﻿using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathCreation.Examples
{
    [ExecuteInEditMode]
    public class PathPlacer : MonoBehaviour
    {
        public EndOfPathInstruction endOfPathInstruction;
        public VertexPath vertex_path;
        public PathCreator pathCreator;
        public GameObject robot;
        public GameObject Path;
        public GameObject prefab;
        public GameObject holder;
        public GameObject visualizations;
        public Vector3 currentPosition;
        private float extended_distance;
        public float visual_render_threshold = 0.8f;
        public float spacing = 3;
        private float revolve_speed = 0.13f;
        public float pulse_speed = 0.1f;
        public float pulse_rise = 0.1f;
        private float pulse_delay = 2f;
        public float revealZone = 15f;
        public float minimuum_revael_length = 8.0f;
        public float maximum_revael_length = 15f;
        public float SI_max_threshold = 1.5f;
        public float SI_mim_threshold = 1.0f;
        public float current_tortuosity;
        public bool revolve;
        private bool wave;
        private bool visuals_check = false;
        float[] initial_distance;
        float distanceTravelled;
        float total_distance;
        const float minSpacing = 0.1f;

        void Start()
        {
            vertex_path = Path.GetComponent<GeneratePathExample>().vertexPath;
            pathCreator = Path.GetComponent<GeneratePathExample>().generatedPath;
            Generate();
           
            if (initial_distance != null)
            {
                int numChildren = initial_distance.Length;

                if (numChildren > 2)
                {
                    holder.transform.GetChild(numChildren - 1).gameObject.transform.position += transform.up * pulse_rise;
                    holder.transform.GetChild(numChildren - 2).gameObject.transform.position += transform.up * pulse_rise * 2;
                    holder.transform.GetChild(numChildren - 3).gameObject.transform.position += transform.up * pulse_rise;
                }
            }
        }

        //Function to toggle displaying the arrows    
        public void showVisuals()
        {
            visuals_check = !visuals_check;
            int numChildren = holder.transform.childCount;
            for (int i = numChildren - 1; i >= 0; i--)
            {
                holder.transform.GetChild(i).GetComponent<Renderer>().enabled = visuals_check;
            }
        }

        void updateVisual()
        {
            if (visuals_check == false)
            {
                int numChildren = holder.transform.childCount;
                for (int i = numChildren - 1; i >= 0; i--)
                {
                    Vector3 arrowPosition = holder.transform.GetChild(i).position;

                    float arrow_distance = pathCreator.path.GetClosestDistanceAlongPath(arrowPosition);
                    float robot_distance = pathCreator.path.GetClosestDistanceAlongPath(currentPosition);
                    extended_distance = revealZone + robot_distance;

                    // removes arrow once robot has reached the arrows location
                    if (Vector3.Distance(arrowPosition, currentPosition) < visual_render_threshold)
                    {
                        holder.transform.GetChild(i).GetComponent<Renderer>().enabled = false;
                    }
                    // renders arrow if arrows are within the reveal zone
                    if (arrow_distance > robot_distance && arrow_distance < extended_distance)
                    {
                        holder.transform.GetChild(i).GetComponent<Renderer>().enabled = true;
                    }
                    // unrender arrows if out of the reveal zone
                    if (arrow_distance < robot_distance || arrow_distance > extended_distance)
                    {
                        holder.transform.GetChild(i).GetComponent<Renderer>().enabled = false;
                    }
                }
            }           
        }

        // Function to start the WaveAction animation
        public void waveBotton()
        {
            wave = !wave;
            waveAction();
        }

        //Funtion to continue the WaveAction unless stop button is pressed. 
        public void waveAction()
        {
            if (wave)
            {
                StartCoroutine(Wave());
            }
        }

        // Reset the position of the arrows by regenerating them for the WaveAction.
        public void resetAction()
        {
            Generate();
            if (initial_distance != null)
            {
                int numChildren = initial_distance.Length;

                if (numChildren > 2)
                {
                    holder.transform.GetChild(numChildren - 1).gameObject.transform.position += transform.up * pulse_rise;
                    holder.transform.GetChild(numChildren - 2).gameObject.transform.position += transform.up * pulse_rise * 2;
                    holder.transform.GetChild(numChildren - 3).gameObject.transform.position += transform.up * pulse_rise;
                }
            }
        }

        void Update()
        {
            if (robot != null)
            {
                currentPosition = robot.GetComponent<PathFollower>().currentPosition;
                current_tortuosity = robot.GetComponent<PathFollower>().current_tortuosity;
                tortuosityArrows();
                updateVisual();
            }

            //This is the revolve action
            if (pathCreator != null && prefab != null && holder != null && revolve == true && initial_distance != null)
            {
                int numChildren = initial_distance.Length;

                for (int i = numChildren - 1; i >= 0; i--)
                {
                    distanceTravelled += (revolve_speed * Time.deltaTime);

                    total_distance = distanceTravelled + initial_distance[i];

                    holder.transform.GetChild(i).gameObject.transform.position = vertex_path.GetPointAtDistance(total_distance, endOfPathInstruction);
                    holder.transform.GetChild(i).gameObject.transform.rotation = vertex_path.GetRotationAtDistance(total_distance, endOfPathInstruction);
                }
            }             
        }

        //function to determine how many visuals to show for each segment
        void tortuosityArrows()
        {
            /* classification of rivers, https://en.wikipedia.org/wiki/Sinuosity
             * SI <1.05: almost straight
                1.05 ≤ SI <1.25: winding
                1.25 ≤ SI <1.50: twisty
                1.50 ≤ SI: meandering
             */

            //Using a linear model to calculate slope and y-intercept
            float dy = (maximum_revael_length - minimuum_revael_length);
            float dx = (SI_max_threshold - SI_mim_threshold);

            float m = (dy / dx);
            float b = minimuum_revael_length - (m * SI_mim_threshold);

            revealZone = (m * current_tortuosity) + b;

            print("Reveal Distance: " + revealZone + " Tort: " + current_tortuosity);
        }

        // Function to cause multiple waves for visual effect.
        IEnumerator PulseWait()
        {
            yield return new WaitForSeconds(pulse_delay);          
        }

        // Creating the Wave by moving 5 arrows along their up axis by a constant value (pulse_rise)
        IEnumerator Wave()
        {
            if (pathCreator != null && prefab != null && holder != null)
            {     
                int numChildren = initial_distance.Length;
                for (int i = 0; i <= numChildren - 1; i++)
                {
                    if (i == 0)
                    {
                        holder.transform.GetChild(i).gameObject.transform.position += transform.up * pulse_rise;
                        holder.transform.GetChild(numChildren - 1).gameObject.transform.position += transform.up * pulse_rise;
                        holder.transform.GetChild(numChildren - 2).gameObject.transform.position += transform.up * -pulse_rise;
                        holder.transform.GetChild(numChildren - 3).gameObject.transform.position += transform.up * -pulse_rise;
                        yield return new WaitForSeconds(pulse_speed);
                    }
                    else if (i == 1)
                    {
                        holder.transform.GetChild(i).gameObject.transform.position += transform.up * pulse_rise;
                        holder.transform.GetChild(i - 1).gameObject.transform.position += transform.up * pulse_rise;
                        holder.transform.GetChild(numChildren - 1).gameObject.transform.position += transform.up * -pulse_rise;
                        holder.transform.GetChild(numChildren - 2).gameObject.transform.position += transform.up * -pulse_rise;
                        yield return new WaitForSeconds(pulse_speed);
                    }
                    else if (i == 2)
                    {
                        holder.transform.GetChild(i).gameObject.transform.position += transform.up * pulse_rise;
                        holder.transform.GetChild(i - 1).gameObject.transform.position += transform.up * pulse_rise;
                        holder.transform.GetChild(i - 2).gameObject.transform.position += transform.up * -pulse_rise;
                        holder.transform.GetChild(numChildren - 1).gameObject.transform.position += transform.up * -pulse_rise;
                        yield return new WaitForSeconds(pulse_speed);
                    }
                    else if (i >= 3 &&  i != numChildren -1)
                    {
                        holder.transform.GetChild(i).gameObject.transform.position += transform.up * pulse_rise;
                        holder.transform.GetChild(i - 1).gameObject.transform.position += transform.up * pulse_rise;
                        holder.transform.GetChild(i - 2).gameObject.transform.position += transform.up * -pulse_rise;
                        holder.transform.GetChild(i - 3).gameObject.transform.position += transform.up * -pulse_rise;
                        yield return new WaitForSeconds(pulse_speed);
                    }
                    else if (i == numChildren - 1)
                    {
                        holder.transform.GetChild(i).gameObject.transform.position += transform.up * pulse_rise;
                        holder.transform.GetChild(i - 1).gameObject.transform.position += transform.up * pulse_rise;
                        holder.transform.GetChild(i - 2).gameObject.transform.position += transform.up * -pulse_rise;
                        holder.transform.GetChild(i - 3).gameObject.transform.position += transform.up * -pulse_rise;
                        yield return new WaitForSeconds(pulse_speed);
                        waveAction(); //call waveAction to continue wave action                 
                    }                 
                }    
            }
        }
    
        // Generate the arrows along the gernerate path once recieved
        void Generate ()
        {               
            if (pathCreator != null && prefab != null && holder != null && vertex_path != null)
            {
                DestroyObjects();
                spacing = Mathf.Max(minSpacing, spacing);
                float dst = 0;
                int count = 0;
                initial_distance = new float[(int)(vertex_path.length / spacing)+1];

                while (dst < vertex_path.length)
                {
                    Vector3 point = vertex_path.GetPointAtDistance (dst);
                    Quaternion rot = vertex_path.GetRotationAtDistance (dst);
                    //Instantiate (prefab, point, rot, holder.transform);
                    GameObject arrowClone = Instantiate(prefab, point, rot, holder.transform);
                    arrowClone.GetComponent<Renderer>().enabled = false;
                    arrowClone.name = dst.ToString();
                    initial_distance[count] = dst;
                    dst += spacing;
                    count++;
                }             
            }
        }

        void DestroyObjects ()
        {
            int numChildren = holder.transform.childCount;
            for (int i = numChildren - 1; i >= 0; i--)
            {
                DestroyImmediate(holder.transform.GetChild (i).gameObject, false);
            }
        }
   
        /*
        protected override void PathUpdated ()
        {
            if (pathCreator != null)
            {
                
            }
        }
        */       
    }
}