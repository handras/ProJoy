using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    [Range(0, 50)]
    public float CameraSpeed;

    public float CameraZoomSpeed;

    [Tooltip("The lower the value the \"closer down\" the camera can get")]
    public float CameraZoomMin;

    [Tooltip("The higher the value the \"further up\" the camera can get")]
    public float CameraZoomMax;

    public int dragTreshold;

    Map map;

    Vector3 lastPosition;
    Camera theCamera;

    private enum InputState
    {
        Initial,
        Dragging,
        Zoom
    }

    InputState state;

    private void Start()
    {
        theCamera = Camera.main;
        map = GameObject.Find("Map").GetComponent<Map>();
        state = InputState.Initial;
    }

    void Update()
    {
        switch (state)
        {
            case InputState.Initial:
                inputStateInitial();
                break;
            case InputState.Dragging:
                inputStateDragging();
                break;
            case InputState.Zoom:
                inputStateZoom();
                break;
        }

        
    }

    private void inputStateInitial()
    {
        // currently not bother with more than two touch
        if (Input.touchCount >= 3)
        {
            return;
        }
        // two fingers means zooming
        if (Input.touchCount == 2)
        {
            state = InputState.Zoom;
            return;
        }
        // get position of the pointer
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("left (0) mouse button is pressed down");
            // save it in screen coordinate
            lastPosition = Input.mousePosition;
        }
        // if moving is greater than the treshold, it is a dragging gesture
        else if (Input.GetMouseButton(0))
        {
            Debug.Log("left (0) mouse button is held down");
            Vector3 diff = lastPosition - Input.mousePosition;
            if(diff.magnitude > dragTreshold)
            {
                state = InputState.Dragging;
                // save the last position in world coordinate
                lastPosition = theCamera.ScreenToWorldPoint(lastPosition);
            }
        }
        
        // if none of the above, then it was a simple click for selection
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 pos = theCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hitInfo = Physics2D.Raycast(pos, Vector2.zero);
            if (hitInfo.collider != null)
            {
                HexTile h = hitInfo.collider.gameObject.GetComponentInParent<HexTile>();
                map.SelectTile(h);
            }
        }
    }

    private void inputStateDragging()
    {
        // the original down posotion was saved in the initial state
        if (Input.GetMouseButton(0))
        {
            Vector3 mouseWorldPosition = theCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 diff = lastPosition - mouseWorldPosition;
            theCamera.transform.Translate(diff * Time.deltaTime * CameraSpeed, Space.World);
            //lastPosition = mouseWorldPosition;    
        }
        // if the button is not pressed, the dragging ends
        else if (Input.GetMouseButtonUp(0))
        {
            state = InputState.Initial;
        }
    }

    private void inputStateZoom()
    {
        // if not exactly 2 fingers are on the screen then end the zoom gesture
        if(Input.touchCount != 2)
        {
            state = InputState.Initial;
            return;
        }
        Touch touch1 = Input.GetTouch(0);
        Touch touch2 = Input.GetTouch(1);

        Vector2 prev1 = touch1.position - touch1.deltaPosition;
        Vector2 prev2 = touch2.position - touch2.deltaPosition;

        float prevDistance = (prev1 - prev2).magnitude;
        float currDistance = (touch1.position - touch2.position).magnitude;

        float distanceDelta = prevDistance - currDistance;
        Debug.Log(distanceDelta);

        theCamera.orthographicSize += distanceDelta * CameraZoomSpeed;
        theCamera.orthographicSize = Mathf.Clamp(theCamera.orthographicSize, CameraZoomMin, CameraZoomMax);
    }

}
