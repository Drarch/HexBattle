using UnityEngine;
using System.Collections;
using System;

public class MouseGameController : MainInput
{
    private enum MouseButton
    {
        Left = 0
        , Right
        , Middle
    }
	
	void Update ()
    {
        RotateCamera();
        TiltCamera();

        if (Input.GetMouseButtonUp((int)MouseButton.Left))
        {
            Select(Input.mousePosition);
        }
	}

    private void RotateCamera()
    {
        //Prevents camera from jumping
        if (Input.GetMouseButtonDown((int)MouseButton.Left))
        {
            PointerLastPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton((int)MouseButton.Left))
        {
            RotateCameraPointer(Input.mousePosition, Camera.main);
        }
    }

    protected void TiltCamera()
    {
        Camera camera = Camera.main;
        float distance = Input.GetAxis("Camera Tilt") / 10.0f;
        TiltCameraAxis(distance, camera);
    }
}
