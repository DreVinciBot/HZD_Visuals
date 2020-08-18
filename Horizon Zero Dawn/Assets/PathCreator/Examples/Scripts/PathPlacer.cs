using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathCreation.Examples
{

    [ExecuteInEditMode]
    public class PathPlacer : PathSceneTool
    {
        public EndOfPathInstruction endOfPathInstruction;
        public GameObject prefab;
        public GameObject holder;
        public float spacing = 3;
        public float revolve_speed = 0.13f;
        public float pulse_speed = 0.1f;
        public float pulse_rise = 0.1f;
        public float pulse_delay = 2f;
        public bool revolve = false;
        public bool wave = false;
        float[] initial_distance;
        float distanceTravelled;
        float total_distance;

        const float minSpacing = 0.1f;

        void Update()
        {
            if (pathCreator != null && prefab != null && holder != null && revolve == true)
            {
                int numChildren = initial_distance.Length;
                for (int i = numChildren - 1; i >= 0; i--)
                {
                    distanceTravelled += (revolve_speed * Time.deltaTime);

                    total_distance = distanceTravelled + initial_distance[i];

                    holder.transform.GetChild(i).gameObject.transform.position = pathCreator.path.GetPointAtDistance(total_distance, endOfPathInstruction);
                    holder.transform.GetChild(i).gameObject.transform.rotation = pathCreator.path.GetRotationAtDistance(total_distance, endOfPathInstruction);
                }
            }

            if (wave && !revolve)
            {

                int numChildren = initial_distance.Length;

                holder.transform.GetChild(numChildren - 1).gameObject.transform.position += transform.up * pulse_rise;
                holder.transform.GetChild(numChildren - 2).gameObject.transform.position += transform.up * pulse_rise * 2;
                holder.transform.GetChild(numChildren - 3).gameObject.transform.position += transform.up * pulse_rise;

                StartCoroutine(Wave());
                wave = false;
                //StartCoroutine(PulseWait());
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

                /*
                holder.transform.GetChild(0).gameObject.transform.position += transform.up * pulse_rise;

                int numChildren = initial_distance.Length;
                for (int i = 1; i <= numChildren-1; i++)
                {
                   
                    holder.transform.GetChild(i).gameObject.transform.position += transform.up * pulse_rise;
                    holder.transform.GetChild(i-1).gameObject.transform.position += transform.up * -pulse_rise;

                    yield return new WaitForSeconds(pulse_speed);                
                }
                holder.transform.GetChild(numChildren-1).gameObject.transform.position += transform.up * -pulse_rise;
               */
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
                
            if (pathCreator != null && prefab != null && holder != null)
            {
                DestroyObjects ();

                VertexPath path = pathCreator.path;
             
                spacing = Mathf.Max(minSpacing, spacing);
                float dst = 0;
       
                int count = 0;
                initial_distance = new float[(int)(path.length / spacing)+1];

                while (dst < path.length)
                {
                    Vector3 point = path.GetPointAtDistance (dst);
                    Quaternion rot = path.GetRotationAtDistance (dst);
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

        protected override void PathUpdated ()
        {
            if (pathCreator != null)
            {
                Generate ();
            }
        }
    }
}