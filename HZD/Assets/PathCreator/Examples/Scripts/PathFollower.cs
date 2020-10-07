using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public GameObject Path;
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float[,] pathInfo;
        public float speed;
        public static float current_tortuosity;
        public Vector3 startPosition;
        public static Vector3 currentPosition;
        float timeRunning = 0.0f;
        public static float distanceTravelled;
        float initial_distaince;
        float total_dist;
        private float length;
        private bool rotateCheck = true;
        private bool start = false;

        void Start()
        {
            if (pathCreator != null)
            {                         
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
                length = pathCreator.path.length;
            }

            Transform initialPosition = Path.GetComponent<GeneratePathExample>().path_waypoints[0];
            transform.position = initialPosition.position;
            distanceTravelled = 0;
        }

        //Function to start robot movement on path
        public void startFollow()
        {
            start = !start;
        }

        //Function to reset robot's position on beginning of path
        public void resetPosition()
        {
            distanceTravelled = 0.0f;
            total_dist = 0.0f;
            transform.position = pathCreator.path.GetPointAtDistance(total_dist, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(total_dist, endOfPathInstruction);
            if (rotateCheck) // Robot moves on its side for some reason; this rotates the object 90 degrees on z axis
            {
                transform.rotation *= Quaternion.Euler(0, 0, 90);
            }
        }

        void Update()
        {
            pathInfo = Path.GetComponent<GeneratePathExample>().tortuosity_segments;       
            
            if (pathCreator != null && start && pathInfo != null)
            {
                distanceTravelled += (speed * Time.deltaTime);          
                initial_distaince = 0f;  
                total_dist = distanceTravelled + initial_distaince;

                if (total_dist > length)
                {              
                    resetPosition();
                }

                //Tracking the robot's position through segments
                for (int i = 1; i < pathInfo.GetLength(0); i++)
                {                     
                    if (total_dist > pathInfo[i-1, 0] && total_dist < pathInfo[i, 0])
                    {
                        current_tortuosity = pathInfo[i, 1];
                        //print("Segment: " + i);
                    }         
                }

                transform.position = pathCreator.path.GetPointAtDistance(total_dist, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance(total_dist, endOfPathInstruction);
                startPosition = pathCreator.path.GetPointAtDistance(0, endOfPathInstruction);
                currentPosition = transform.position;

                float distance = Vector3.Distance(currentPosition, startPosition);
                float tortuosity = total_dist / distance;         
                if (rotateCheck) // Robot moves on its side for some reason; this rotates the object 90 degrees on z axis
                {
                    transform.rotation *= Quaternion.Euler(0, 0, 90);
                }          
            }
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged()
        {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}