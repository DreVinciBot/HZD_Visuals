using UnityEngine.UI;
using UnityEngine;

namespace PathCreation.Examples
{
    // Example of creating a path at runtime from a set of points.

    [RequireComponent(typeof(PathCreator))]
    public class GeneratePathExample : MonoBehaviour
    {
        public EndOfPathInstruction endOfPathInstruction;
        public bool closedLoop = false;
        public Transform[] path_waypoints;
        public PathCreator generatedPath;
        public VertexPath vertexPath;
        public GameObject segment_marker_prefab;
        public GameObject segment_holder;
        public Vector3 segment_start;
        public Vector3 segment_end;
        public int num_segments = 5;
        private bool visuals_check = false;
        private bool waypoint_check = false;
        public float[,] tortuosity_segments;
        public GameObject gem;
        public GameObject gemHolder;
        public GameObject Path_Grid;
        public GameObject waypoint;
        private Vector3 point;
        private Vector3 gemPoint;
        float width = 15;
        public int row = 3;
        public int col = 5; 
        public bool complexPath = true;
        public bool simplePath = false;
        float gem_height = 6f;
        private static int waypoint_counter = -1;
        bool global_check = true;



        void Awake ()
        {
            Quaternion rot = new Quaternion(0f, 0f, 0f, 0f);

            for (int i = 0; i < col; i++)
            {
                for (int j = -2; j < row; j++)
                {
                    if (i % 2 == 0)
                    {
                        point = new Vector3(j * width, 0f, i * width);
                        gemPoint = point +  new Vector3(0f, gem_height, 0f);
                        waypoint_counter += 1;
                    }
                    else
                    {
                        point = new Vector3((j * width) + (width / 2), 0f, i * width);
                        gemPoint = point + new Vector3(0f, gem_height, 0f);
                        waypoint_counter += 1;
                    }

                    GameObject wayPointClone = Instantiate(waypoint, point, rot, Path_Grid.transform);
                    wayPointClone.name = i + "," + j;
                    wayPointClone.GetComponent<Renderer>().enabled = visuals_check;
                    GameObject gemClones = Instantiate(gem, gemPoint, Quaternion.Euler(-90, 0, 0), gemHolder.transform);
                    gemClones.GetComponent<CollectToken>().fillImage = gem.GetComponent<CollectToken>().fillImage;
                    gemClones.GetComponent<CollectToken>().robotAlert = gem.GetComponent<CollectToken>().robotAlert;
                    gemClones.GetComponent<CollectToken>().Collected_text = gem.GetComponent<CollectToken>().Collected_text;
                    gemClones.GetComponent<CollectToken>().Remaining_text = gem.GetComponent<CollectToken>().Remaining_text;
                    gemClones.GetComponent<CollectToken>().collected_text = gem.GetComponent<CollectToken>().collected_text;

                }
            }

            creatPath();    
 
        }

        void creatPath()
        {
            if (Path_Grid.transform.childCount > 0)
            {
                //Determine the number of points that should be in the Simple/Complex path

                if (complexPath)
                {
                    path_waypoints = new Transform[Path_Grid.transform.childCount];

                    for (int i = 0; i < Path_Grid.transform.childCount; i++)
                    {
                        Transform transform_object = Path_Grid.transform.GetChild(i);
                        path_waypoints[i] = transform_object;
                    }

                    //how to select waypoints randomly/shuffle order in array
                    // Started at t = 1 to ensure the robot starts at the same location every run.
                    for (int t = 1; t < path_waypoints.Length; t++)
                    {
                        Transform tmp = path_waypoints[t];
                        int r = Random.Range(t, path_waypoints.Length);
                        path_waypoints[t] = path_waypoints[r];
                        path_waypoints[r] = tmp;
                    }
                }

                //simple path
                else if(simplePath)
                {
                    path_waypoints = new Transform[4];
                    path_waypoints[0] = Path_Grid.transform.GetChild(0);
                    path_waypoints[1] = Path_Grid.transform.GetChild(4);
                    path_waypoints[2] = Path_Grid.transform.GetChild(20);
                    path_waypoints[3] = Path_Grid.transform.GetChild(24);

                    for (int t = 1; t < path_waypoints.Length; t++)
                    {
                        Transform tmp = path_waypoints[t];
                        int r = Random.Range(t, path_waypoints.Length);
                        path_waypoints[t] = path_waypoints[r];
                        path_waypoints[r] = tmp;
                    }
                }
                else
                {                             
                    path_waypoints = new Transform[Path_Grid.transform.childCount];

                    for (int i = 0; i < Path_Grid.transform.childCount; i++)
                    {
                        Transform transform_object = Path_Grid.transform.GetChild(i);
                        path_waypoints[i] = transform_object;
                    }                    
                }

                // Create a new bezier path from the waypoints.
                //BezierPath bezierPath = new BezierPath(waypoints, closedLoop, PathSpace.xyz);
                BezierPath bezierPath = new BezierPath(path_waypoints, closedLoop, PathSpace.xyz);

                GetComponent<PathCreator>().bezierPath = bezierPath;
                vertexPath = new VertexPath(bezierPath, transform);

                GlobalTortuosity();
                LocalTortuosity();

                //Starts by un-rendering the waypoints
                for (int i = path_waypoints.Length - 1; i >= 0; i--)
                {
                    path_waypoints[i].GetComponent<Renderer>().enabled = waypoint_check;
                }
            }
        }

        //Function to toggle displaying the segmentation points
        public void showSegments()
        {
            visuals_check = !visuals_check;
            int numChildren = segment_holder.transform.childCount;
            for (int i = numChildren - 1; i >= 0; i--)
            {
                segment_holder.transform.GetChild(i).GetComponent<Renderer>().enabled = visuals_check;
            }
        }
        //Function to toggle displaying the waypoints
        public void showWaypoints()
        {
            waypoint_check = !waypoint_check;
            int numChildren = path_waypoints.Length;
            for (int i = numChildren - 1; i >= 0; i--)
            {
                path_waypoints[i].GetComponent<Renderer>().enabled = waypoint_check;
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

        // Calculates the Global Path's Tortuosity through each vertex point if needed, really only need last value. 
        void GlobalTortuosity()
        {
            generatedPath = GetComponent<PathCreator>();           
            float[] lengths = generatedPath.path.cumulativeLengthAtEachVertex;

            Vector3[] localPoints = generatedPath.path.localPoints;

            float[] tortuosity = new float[localPoints.Length];

            for (int i = 0; i < lengths.Length; i++)
            {
                float curve = lengths[i];
                float dist = Vector3.Distance(localPoints[i], path_waypoints[0].position);
                tortuosity[i] = curve / dist;
            }

            //print("gt: " + tortuosity[tortuosity.Length -1]);
        }

        // Calculates the path's Local Tortuosity through path segments. Currently, the path is seperated into N equal parts. 
        void LocalTortuosity()
        {
            DestroyObjects(); // Recalculate the local tortuosity for any changes to a path since path is an input. 
            if (num_segments > 0 && num_segments.GetType() == typeof(int))
            {
                float cumulative_segment_length = 0.0f;
                float total_length = generatedPath.path.length;
                float segmented_lengths = total_length / num_segments;

                GameObject segment_marker = Instantiate(segment_marker_prefab, generatedPath.path.GetPointAtDistance(0, endOfPathInstruction), generatedPath.path.GetRotationAtDistance(0), segment_holder.transform);
                if(!visuals_check)
                {
                    segment_marker.GetComponent<Renderer>().enabled = false;
                }

                tortuosity_segments = new float[num_segments+1,2];
                tortuosity_segments[0, 0] = 0.0f;

                //Try to divide the path into N equal parts for now. 
                for (int i = 0; i < num_segments; i++)
                {
                    segment_start = generatedPath.path.GetPointAtDistance(cumulative_segment_length, endOfPathInstruction);            
                    cumulative_segment_length += segmented_lengths;
                    segment_end = generatedPath.path.GetPointAtDistance(cumulative_segment_length, endOfPathInstruction);

                    float segment_distance = Vector3.Distance(segment_start, segment_end);
                    float local_tortuosity = segmented_lengths / segment_distance;
                    tortuosity_segments[i+1, 0] = cumulative_segment_length;
                    tortuosity_segments[i+1, 1] = local_tortuosity;

                    Quaternion rot = generatedPath.path.GetRotationAtDistance(cumulative_segment_length);
                    segment_marker = Instantiate(segment_marker_prefab, segment_end, rot, segment_holder.transform);
                    if (!visuals_check)
                    {
                        segment_marker.GetComponent<Renderer>().enabled = false;
                    }

                    //print(tortuosity_segments[i, 0] +  " " + tortuosity_segments[i, 1]);
                }
                
            }
            else
            {
                num_segments = 1; //Ensure that the num_segments is a positive integer.
            }
        }

        void Update()
        {
            LocalTortuosity();    

            if(global_check)
            {
                GlobalTortuosity();
                global_check = false;
            }
        }
    }
}