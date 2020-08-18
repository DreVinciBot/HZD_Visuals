using UnityEngine;

namespace PathCreation.Examples
{
    // Example of creating a path at runtime from a set of points.

    [RequireComponent(typeof(PathCreator))]
    public class GeneratePathExample : MonoBehaviour
    {
        public bool closedLoop = false;
        public Transform[] waypoints;
        public PathCreator pathCreator;
        float x = 0f;


        void Start ()
        {

            
            if (waypoints.Length > 0)
            {
                // Create a new bezier path from the waypoints.
                BezierPath bezierPath2 = new BezierPath (waypoints, closedLoop, PathSpace.xyz);
                GetComponent<PathCreator> ().bezierPath = bezierPath2;

                //BezierPath bezier = new BezierPath(waypoints[0].position, closedLoop, PathSpace.xyz);

                //GetComponent<PathCreator> ().bezierPath = bezier;

               

                float[] lengths = pathCreator.path.cumulativeLengthAtEachVertex;

                print(pathCreator.path.length);

                Vector3[] localPoints = pathCreator.path.localPoints;

               
                for (int i = 0; i < lengths.Length; i++)
                {
                    float curve = lengths[i];
                    float dist = Vector3.Distance(localPoints[i], waypoints[0].position);
                    float tor = curve / dist;

                    //print("tor value: " + tor);
                }
            }

        }

        void Update()
        {

            if (waypoints.Length > 0)
            {
                // Create a new bezier path from the waypoints.
                BezierPath bezierPath = new BezierPath(waypoints, closedLoop, PathSpace.xyz);
                GetComponent<PathCreator>().bezierPath = bezierPath;
          
            }

            float curve_length = pathCreator.path.length;

            float dist2 = Vector3.Distance(waypoints[0].position, waypoints[waypoints.Length - 1].position);

            //float angle = Vector3.Angle(waypoints[0].up, waypoints[0].transform.position - waypoints[2].transform.position);

            Vector3 direction = waypoints[waypoints.Length -1].position - waypoints[0].position;


            Quaternion rotation = Quaternion.LookRotation(direction);


            for (int i = 0; i < waypoints.Length; i++)
            {
               

            }
           // waypoints[0].transform.rotation = rotation;

            //float dot = Vector3.Dot(waypoints[2].position, waypoints[0].position);


            //print("direction: " + direction);

            //var angle3d = Vector3.Angle(Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized, Vector3.ProjectOnPlane(waypoints[0].position - transform.position, Vector3.up).normalized



            float tortuoisty = curve_length / dist2;
            print("Tortuosity: " + tortuoisty + "dist: " + dist2 + " direction : " + direction );
            //print("angle3d: " + angle3d);
            
        }
    }
}