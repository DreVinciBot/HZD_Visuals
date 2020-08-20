using UnityEngine;

namespace PathCreation.Examples
{
    // Example of creating a path at runtime from a set of points.

    [RequireComponent(typeof(PathCreator))]
    public class GeneratePathExample : MonoBehaviour
    {
        public EndOfPathInstruction endOfPathInstruction;
        public bool closedLoop = false;
        public Transform[] waypoints;      
        public PathCreator generatedPath;
        public VertexPath vertexPath;
        public GameObject prefab;
        public GameObject segment_holder;
        public Vector3 segment_start;
        public Vector3 segment_end;
        public int num_segments = 10;
        private bool visuals_check = false;
        private bool waypoint_check = false;
        public float[,] tortuosity_segments;


        void Awake ()
        {
            if (waypoints.Length > 0)
            {
                // Create a new bezier path from the waypoints.
                BezierPath bezierPath = new BezierPath(waypoints, closedLoop, PathSpace.xyz);
                GetComponent<PathCreator>().bezierPath = bezierPath;
                vertexPath = new VertexPath(bezierPath, transform);

                GlobalTortuosity();
                LocalTortuosity();
              
                for (int i = waypoints.Length - 1; i >= 0; i--)
                {
                    waypoints[i].GetComponent<Renderer>().enabled = waypoint_check;
                }
            }
        }

        public void showSegments()
        {
            visuals_check = !visuals_check;
            int numChildren = segment_holder.transform.childCount;
            for (int i = numChildren - 1; i >= 0; i--)
            {
                segment_holder.transform.GetChild(i).GetComponent<Renderer>().enabled = visuals_check;
            }
        }

        public void showWaypoints()
        {
            waypoint_check = !waypoint_check;
            int numChildren = waypoints.Length;
            for (int i = numChildren - 1; i >= 0; i--)
            {
                waypoints[i].GetComponent<Renderer>().enabled = waypoint_check;
            }
        }

        void DestroyObjects()
        {
            int numChildren = segment_holder.transform.childCount;
            for (int i = numChildren - 1; i >= 0; i--)
            {
                DestroyImmediate(segment_holder.transform.GetChild(i).gameObject, false);
            }
        }

        void GlobalTortuosity()
        {
            generatedPath = GetComponent<PathCreator>();           
            float[] lengths = generatedPath.path.cumulativeLengthAtEachVertex;

            Vector3[] localPoints = generatedPath.path.localPoints;

            float[] tortuosity = new float[localPoints.Length];

            for (int i = 0; i < lengths.Length; i++)
            {
                float curve = lengths[i];
                float dist = Vector3.Distance(localPoints[i], waypoints[0].position);
                tortuosity[i] = curve / dist;
            }
        }

        void LocalTortuosity()
        {
            //Split the path into segments, then calculate the Tortuosity for each. 
            DestroyObjects();
            if (num_segments > 0)
            {
                float cumulative_segment_length = 0.0f;
                float total_length = generatedPath.path.length;
                float segmented_lengths = total_length / num_segments;

                GameObject segment_marker = Instantiate(prefab, generatedPath.path.GetPointAtDistance(0, endOfPathInstruction), generatedPath.path.GetRotationAtDistance(0), segment_holder.transform);
                if(!visuals_check)
                {
                    segment_marker.GetComponent<Renderer>().enabled = false;
                }

                tortuosity_segments = new float[num_segments,2];

                //Try to divide the path into N equal parts for now. 
                for (int i = 0; i < num_segments; i++)
                {
                    segment_start = generatedPath.path.GetPointAtDistance(cumulative_segment_length, endOfPathInstruction);            
                    cumulative_segment_length += segmented_lengths;
                    segment_end = generatedPath.path.GetPointAtDistance(cumulative_segment_length, endOfPathInstruction);

                    float segment_distance = Vector3.Distance(segment_start, segment_end);
                    float local_tortuosity = segmented_lengths / segment_distance;
                    tortuosity_segments[i, 0] = cumulative_segment_length;
                    tortuosity_segments[i, 1] = local_tortuosity;

                    Quaternion rot = generatedPath.path.GetRotationAtDistance(cumulative_segment_length);
                    segment_marker = Instantiate(prefab, segment_end, rot, segment_holder.transform);
                    if (!visuals_check)
                    {
                        segment_marker.GetComponent<Renderer>().enabled = false;
                    }

                    //print(tortuosity_segments[i, 0] +  " " + tortuosity_segments[i, 1]);
                }
                
            }

            else
            {
                num_segments = 1;
            }
        }

        void Update()
        {
            LocalTortuosity();    
        }
    }
}