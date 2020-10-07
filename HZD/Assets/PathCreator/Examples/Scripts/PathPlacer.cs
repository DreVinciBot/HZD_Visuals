using PathCreation;
using System;
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
        public float spacing = 3;
        public float revolve_speed;
        public float pulse_speed = 0.1f;
        public float pulse_rise = 0.1f;
        private float pulse_delay = 2f;
        public float revealZone = 15f;
        public float fixed_time_zone;
        public static bool revolve;
        public bool wave = false;
        float[] initial_distance;
        float distanceTravelled;
        float total_distance;
        const float minSpacing = 0.1f;
        public bool initial_visuals;
        public bool fixed_time_new;
        public static bool initial_call = false;
        public static int count;
        public static float temp_tort = 1;
        public static float temp_reveal_length = 1;
        public static bool fixed_time_intialized;
        public static bool log_time_initialized;
        public static bool start_fixed_path;
        public static bool start_log_path;
        public static bool fixed_condition;
        public static bool log_condition;
        public bool log_time_new;

        void Start()
        {
            start_fixed_path = false;
            fixed_time_intialized = true;
            log_time_initialized = true;
            temp_reveal_length = 1;
            fixed_condition = false;
            log_condition = false;
            start_log_path = true;
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
            if (initial_visuals)
            {
                int numChildren = holder.transform.childCount;
                for (int i = numChildren - 1; i >= 0; i--)
                {
                    holder.transform.GetChild(i).GetComponent<Renderer>().enabled = initial_visuals;
                }
            }
        }

        // Function to start the WaveAction animation
        public void waveBotton()
        {
            //wave = !wave;
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
            // All visuals condition
            if (initial_visuals && initial_call)
            {
                showVisuals();
                initial_call = false;
            }

            // Log-time condition
            if (log_time_new && holder != null && pathCreator != null)
            {
                log_condition = true;
                float log_scalar = 10;
                revealZone = (float)(log_scalar * Math.Log(Math.Exp(1) * PathFollower.current_tortuosity));

                if (revealZone != temp_reveal_length)
                {
                    if (revealZone > temp_reveal_length)
                    {
                        //log_time_initialized = false;
                        currentPosition = PathFollower.currentPosition;
                        pathCreator = Path.GetComponent<GeneratePathExample>().generatedPath;
                        float robot_distance = pathCreator.path.GetClosestDistanceAlongPath(currentPosition);

                        temp_reveal_length = revealZone;
                        DestroyObjects();

                        extended_distance = revealZone + robot_distance;
                        spacing = Mathf.Max(minSpacing, spacing);
                        float dst = robot_distance;

                        if (start_log_path)
                        {
                            start_log_path = false;
                            extended_distance = revealZone;

                            dst = 1;
                        }

                        while (dst < extended_distance)
                        {
                            Vector3 offset = new Vector3(0, 0.65f, 0);
                            Vector3 point = vertex_path.GetPointAtDistance(dst) + offset;
                            Quaternion rot = vertex_path.GetRotationAtDistance(dst);
                            GameObject arrowClone = Instantiate(prefab, point, rot, holder.transform);
                            arrowClone.GetComponent<Renderer>().enabled = true;
                            arrowClone.name = dst.ToString();
                            dst += spacing;
                        }
                    }
                    else if (revealZone < temp_reveal_length)
                    {
                        temp_reveal_length = revealZone;
                    }
                    else
                    {
                        print("error");
                    }
                }

                //calculate the distance between the last arrow and the robot and compare that with the latest revealzone
                else if(holder != null)
                {
                    currentPosition = PathFollower.currentPosition;
                    float robot_distance = pathCreator.path.GetClosestDistanceAlongPath(currentPosition);
                    Vector3 lastArrow = holder.transform.GetChild(holder.transform.childCount - 1).position;
                    float dst = pathCreator.path.GetClosestDistanceAlongPath(lastArrow);

                    if (robot_distance < dst)
                    {
                        if (dst - robot_distance > revealZone)
                        {
                            //pass
                        }
                        else
                        {
                            //add new arrow
                            Vector3 offset = new Vector3(0, 0.65f, 0);
                            Vector3 point = vertex_path.GetPointAtDistance(dst + spacing) + offset;
                            Quaternion rot = vertex_path.GetRotationAtDistance(dst + spacing);
                            GameObject arrowClone = Instantiate(prefab, point, rot, holder.transform);
                            arrowClone.GetComponent<Renderer>().enabled = true;
                            arrowClone.name = dst.ToString();
                            dst += spacing;
                        }
                    }
                    else if (robot_distance > dst)
                    {
                        if (dst + (vertex_path.length - robot_distance) > revealZone)
                        {
                            //pass
                        }
                        else
                        {
                            //add new arrow
                            Vector3 offset = new Vector3(0, 0.65f, 0);
                            Vector3 point = vertex_path.GetPointAtDistance(dst + spacing) + offset;
                            Quaternion rot = vertex_path.GetRotationAtDistance(dst + spacing);
                            GameObject arrowClone = Instantiate(prefab, point, rot, holder.transform);
                            arrowClone.GetComponent<Renderer>().enabled = true;
                            arrowClone.name = dst.ToString();
                            dst += spacing;
                        }
                    }
                    else
                    {
                        print("error is arrow to distance.");
                    }
                }
                else
                {
                    //log_time_initialized = false;
                    currentPosition = PathFollower.currentPosition;
                    pathCreator = Path.GetComponent<GeneratePathExample>().generatedPath;
                    float robot_distance = pathCreator.path.GetClosestDistanceAlongPath(currentPosition);
                    DestroyObjects();

                    extended_distance = revealZone + robot_distance;
                    spacing = Mathf.Max(minSpacing, spacing);
                    float dst = robot_distance;

                    while (dst < extended_distance)
                    {
                        Vector3 offset = new Vector3(0, 0.65f, 0);
                        Vector3 point = vertex_path.GetPointAtDistance(dst) + offset;
                        Quaternion rot = vertex_path.GetRotationAtDistance(dst);
                        GameObject arrowClone = Instantiate(prefab, point, rot, holder.transform);
                        arrowClone.GetComponent<Renderer>().enabled = true;
                        arrowClone.name = dst.ToString();
                        dst += spacing;
                    }
                }
            }

            // fixed-time condition
            if (fixed_time_new && holder != null && pathCreator != null)
            {
                fixed_condition = true;
                if (fixed_time_intialized)
                {
                    fixed_time_intialized = false;
                    DestroyObjects();
                    currentPosition = PathFollower.currentPosition;
                    pathCreator = Path.GetComponent<GeneratePathExample>().generatedPath;
                    float robot_distance = pathCreator.path.GetClosestDistanceAlongPath(currentPosition);
                    extended_distance = fixed_time_zone + robot_distance;
                    spacing = Mathf.Max(minSpacing, spacing);
                    float dst = 0;
                    count = 0;

                    while (dst < fixed_time_zone)
                    {
                        Vector3 offset = new Vector3(0, 0.65f, 0);
                        Vector3 point = vertex_path.GetPointAtDistance(dst) + offset;
                        Quaternion rot = vertex_path.GetRotationAtDistance(dst);
                        GameObject arrowClone = Instantiate(prefab, point, rot, holder.transform);
                        arrowClone.GetComponent<Renderer>().enabled = true;
                        arrowClone.name = dst.ToString();
                        dst += spacing;
                    }
                }

                if (ArrowBehavior.newArrow)
                {
                    ArrowBehavior.newArrow = false;
                    Vector3 lastArrow = holder.transform.GetChild(holder.transform.childCount - 1).position;
                    float dst = pathCreator.path.GetClosestDistanceAlongPath(lastArrow) + spacing;
                    //add new arrow
                    Vector3 offset = new Vector3(0, 0.65f, 0);
                    Vector3 point = vertex_path.GetPointAtDistance(dst) + offset;
                    Quaternion rot = vertex_path.GetRotationAtDistance(dst);
                    GameObject arrowClone = Instantiate(prefab, point, rot, holder.transform);
                    arrowClone.GetComponent<Renderer>().enabled = true;
                    arrowClone.name = dst.ToString();
                }

                if (holder.transform.childCount < 4 )
                {
                    DestroyObjects();
                    currentPosition = PathFollower.currentPosition;
                    pathCreator = Path.GetComponent<GeneratePathExample>().generatedPath;
                    float robot_distance = pathCreator.path.GetClosestDistanceAlongPath(currentPosition);
                    extended_distance = fixed_time_zone + robot_distance;
                    spacing = Mathf.Max(minSpacing, spacing);
                    float dst = robot_distance;

                    while (dst < fixed_time_zone)
                    {
                        Vector3 offset = new Vector3(0, 0.65f, 0);
                        Vector3 point = vertex_path.GetPointAtDistance(dst) + offset;
                        Quaternion rot = vertex_path.GetRotationAtDistance(dst);
                        GameObject arrowClone = Instantiate(prefab, point, rot, holder.transform);
                        arrowClone.GetComponent<Renderer>().enabled = true;
                        arrowClone.name = dst.ToString();
                        dst += spacing;
                    }
                }
            }
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
                    else if (i >= 3 && i != numChildren - 1)
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
        void Generate()
        {
            if (pathCreator != null && prefab != null && holder != null && vertex_path != null)
            {
                DestroyObjects();
                spacing = Mathf.Max(minSpacing, spacing);
                float dst = 0;
                int count = 0;
                initial_distance = new float[(int)(vertex_path.length / spacing) + 1];


                Vector3 offset = new Vector3(0, 0.65f, 0);
                while (dst < vertex_path.length)
                {

                    Vector3 point = vertex_path.GetPointAtDistance(dst) + offset;
                    Quaternion rot = vertex_path.GetRotationAtDistance(dst);
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

        void DestroyObjects()
        {
            int numChildren = holder.transform.childCount;
            for (int i = numChildren - 1; i >= 0; i--)
            {
                DestroyImmediate(holder.transform.GetChild(i).gameObject, false);
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