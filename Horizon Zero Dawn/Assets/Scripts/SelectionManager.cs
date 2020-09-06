using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PathCreation.Examples;
using UnityEngine.SceneManagement;

namespace PathCreation.Examples
{

    public class SelectionManager : MonoBehaviour
    {
        [SerializeField] private string selectableTag = "Selectable";
        [SerializeField] private Material unselected_material;
        [SerializeField] private Material selected_material;

        private bool arrowCheck = false;
        private Transform _selection;
        public GameObject question;
        public GameObject Selections;
        public GameObject robot;
        public Vector3 robotPosition;
        bool queryCheck = false;
        bool checkpoint1 = true;
        bool checkpoint2 = true;

        public Camera playerCamera;

        public void showArrows()
        {
            arrowCheck = !arrowCheck;
            Selections.SetActive(arrowCheck);
            question.SetActive(arrowCheck);  
            
        }

        void checkpoint()
        {
            //Do a check through a time stamp or distance traveled? Time maybe easier 
            Scene currentScene = SceneManager.GetActiveScene();

            if (currentScene.name == "Simple Path A" && queryCheck)
            {
                Vector3 Pos1 = new Vector3(0, 0, 19);
                Vector3 Pos2 = new Vector3(15, 0, 30);

                if(Vector3.Distance(robotPosition,Pos1) < 0.05  && checkpoint1)
                {
                    robot.GetComponent<PathFollower>().startFollow();
                    showArrows();
                    checkpoint1 = false;
                }

                if (Vector3.Distance(robotPosition, Pos2) < 0.05 && checkpoint2)
                {
                    robot.GetComponent<PathFollower>().startFollow();
                    showArrows();
                    checkpoint2 = false;
                }
            }

        }

        void Update()
        {
            robotPosition = robot.GetComponent<PathFollower>().currentPosition;
            checkpoint();

            if (_selection != null && _selection.CompareTag(selectableTag))
            {
                var selectionRenerer = _selection.GetComponent<Renderer>();
                selectionRenerer.material = unselected_material;
                _selection = null;
            }

            //var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var ray = playerCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var selection = hit.transform;
                if (selection.CompareTag(selectableTag))
                {
                    var selectionRenderer = selection.GetComponent<Renderer>();
                    if (selectionRenderer != null)
                    {
                        selectionRenderer.material = selected_material;

                        if (Input.GetMouseButtonDown(0))
                        {
                            print("clicked " + selection.name);
                            Cursor.lockState = CursorLockMode.Locked;
                            showArrows();

                            robot.GetComponent<PathFollower>().startFollow();              
                        }
                    }
                }
                _selection = selection;
            }
        }
    }
}