using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    [Range(0, 50)]
    public float CameraSpeed;
    
    Vector3 lastPosition;
    Camera theCamera;

    private void Start()
    {
        theCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("left (0) mouse button is pressed down");
            lastPosition = theCamera.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            Debug.Log("left (0) mouse button is held down");
            Vector3 mouseWorldPosition = theCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 diff = lastPosition - mouseWorldPosition;
            theCamera.transform.Translate(diff*Time.deltaTime*CameraSpeed, Space.World);
            //lastPosition = mouseWorldPosition;
        }
    }
}
