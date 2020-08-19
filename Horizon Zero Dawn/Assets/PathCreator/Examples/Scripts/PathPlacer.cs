using PathCreation;
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
        public GameObject Path;
        public GameObject prefab;
        public GameObject holder;
        //public GameObject visuals;
        public float spacing = 3;
        public float revolve_speed = 0.13f;
        public float pulse_speed = 0.1f;
        public float pulse_rise = 0.1f;
        public float pulse_delay = 2f;
        public bool revolve;
        public bool wave;
        public bool visual_check;
        float[] initial_distance;
        float distanceTravelled;
        float total_distance;


        const float minSpacing = 0.1f;

        void Start()
        {
            vertex_path = Path.GetComponent<GeneratePathExample>().vertexPath;
            pathCreator = Path.GetComponent<GeneratePathExample>().generatedPath;
            Generate();
        }
      

        public void waveBotton()
        {
            
            wave = !wave;
            Debug.Log("Boolean");
            //pathCreator.pathUpdated += Generate;
           
        }

        void Update()
        {

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

            if (wave && !revolve && initial_distance != null)
            {

                int numChildren = initial_distance.Length;

                if (numChildren > 2)
                {
                    holder.transform.GetChild(numChildren - 1).gameObject.transform.position += transform.up * pulse_rise;
                    holder.transform.GetChild(numChildren - 2).gameObject.transform.position += transform.up * pulse_rise * 2;
                    holder.transform.GetChild(numChildren - 3).gameObject.transform.position += transform.up * pulse_rise;

                    StartCoroutine(Wave());
                    wave = false;
                    //StartCoroutine(PulseWait());
                }

            }

        }

        IEnumerator PulseWait()
        {
            yield return new WaitForSeconds(pulse_delay);
            wave = true;
        }

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
                        StartCoroutine(Wave());                 
                    }                 
                }    
            }
        }
    
        void Generate ()
        {
                
            if (pathCreator != null && prefab != null && holder != null && vertex_path != null)
            {
                DestroyObjects ();

                //VertexPath path = pathCreator.path;

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
                DestroyImmediate (holder.transform.GetChild (i).gameObject, false);
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