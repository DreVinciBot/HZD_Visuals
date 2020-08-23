using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PathCreation.Examples;

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
   
    public void showVisuals()
    {
        arrowCheck = !arrowCheck;
        Selections.SetActive(arrowCheck);
        question.SetActive(arrowCheck);
    }

    void Update()
    {     
        if(_selection != null)
        {
            var selectionRenerer = _selection.GetComponent<Renderer>();
            selectionRenerer.material = unselected_material;
            _selection = null;
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
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
                        showVisuals();
                        robot.GetComponent<PathFollower>().startFollow();
                    }
                }
            }
            _selection = selection;
        }
    }
}
