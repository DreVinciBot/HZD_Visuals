using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public Camera main_cam;
    public Camera topdown_cam;
    bool switchCheck = true;

    void Start()
    {
        main_cam.enabled = switchCheck;
        topdown_cam.enabled = !switchCheck;
    }

    public void switchCamera()
    {
        switchCheck = !switchCheck;
        main_cam.enabled = switchCheck;
        topdown_cam.enabled = !switchCheck;
    }


}
