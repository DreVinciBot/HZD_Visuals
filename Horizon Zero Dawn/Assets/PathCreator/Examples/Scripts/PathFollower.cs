using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        float timeRunning = 0.0f;
        float distanceTravelled;
        float initial_distaince;
        float total_dist;
        private float length;
        private bool rotateCheck = true;
        private bool start = false;

        void Start()
        {
            if (pathCreator != null)
            {
                length = pathCreator.path.length;
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;           
            }
        }

        public void startFollow()
        {
            start = !start;
        }

        public void resetPosition()
        {
            distanceTravelled = 0.0f;
            total_dist = 0.0f;
            transform.position = pathCreator.path.GetPointAtDistance(total_dist, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(total_dist, endOfPathInstruction);
            if (rotateCheck)
            {
                transform.rotation *= Quaternion.Euler(0, 0, 90);
            }
        }

        void Update()
        {
            if (pathCreator != null && start)
            {
                //timeRunning += Time.deltaTime;
                distanceTravelled += (speed * Time.deltaTime);          
                initial_distaince = 0f;  
                total_dist = distanceTravelled + initial_distaince;

                transform.position = pathCreator.path.GetPointAtDistance(total_dist, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance(total_dist, endOfPathInstruction);
                if (rotateCheck)
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