using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    [Range(0, 50)]
    public float CameraSpeed;

    public float CameraZoomSpeed = .5f;
    [Tooltip("The lower the value the \"closer down\" the camera can get")]
    public float CameraZoomMin = 1.25f;
    [Tooltip("The higher the value the \"further up\" the camera can get")]
    public float CameraZoomMax = 10f;

    Map map;

    Vector3 lastPosition;
    Camera theCamera;

    private void Start()
    {
        theCamera = Camera.main;
        map = GameObject.Find("Map").GetComponent<Map>();
    }

    void Update()
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

        if (Input.touchCount == 2)
        {
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
}
