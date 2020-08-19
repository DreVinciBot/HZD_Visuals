using UnityEngine;

namespace PathCreation.Examples
{
    // Example of creating a path at runtime from a set of points.

    [RequireComponent(typeof(PathCreator))]
    public class GeneratePathExample : MonoBehaviour
    {
        public bool closedLoop = false;
        public Transform[] waypoints;      
        public PathCreator generatedPath;
        public VertexPath vertexPath;

        void Awake ()
        {
            if (waypoints.Length > 0)
            {
                // Create a new bezier path from the waypoints.
                BezierPath bezierPath = new BezierPath(waypoints, closedLoop, PathSpace.xyz);
                GetComponent<PathCreator>().bezierPath = bezierPath;
                vertexPath = new VertexPath(bezierPath, transform);


                Tortuosity();
            }

        }

        void Tortuosity()
        {
            generatedPath = GetComponent<PathCreator>();

            //vertexPath = generatedPath.path;
            
            float[] lengths = generatedPath.path.cumulativeLengthAtEachVertex;

            print(generatedPath.path.length);

            Vector3[] localPoints = generatedPath.path.localPoints;


            for (int i = 0; i < lengths.Length; i++)
            {
                float curve = lengths[i];
                float dist = Vector3.Distance(localPoints[i], waypoints[0].position);
                float tor = curve / dist;

                //print("tor value: " + tor);
            }
        }

        void Update()
        {
            /*
            if (waypoints.Length > 0)
            {
                // Create a new bezier path from the waypoints.
                BezierPath bezierPath = new BezierPath(waypoints, closedLoop, PathSpace.xyz);
                GetComponent<PathCreator>().bezierPath = bezierPath;

                float curve_length = pathCreator.path.length;

                float dist2 = Vector3.Distance(waypoints[0].position, waypoints[waypoints.Length - 1].position);


                float tortuoisty = curve_length / dist2;
            }
            */
            
        }
    }
}