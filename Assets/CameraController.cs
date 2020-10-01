using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 2f;
    public float dragSpeed = 40f;
    public float scrollSpeed = 200f;

    bool dragging = false;

    void Update()
    {
        Vector3 pos = transform.position;

        #region camera movement
        if (Input.GetMouseButtonDown(1))
            dragging = true;

        if (Input.GetMouseButtonUp(1))
            dragging = false;

        if (dragging)
        {
            pos.x -= Input.GetAxis("Mouse X") * dragSpeed * Time.deltaTime;
            pos.z -= Input.GetAxis("Mouse Y") * dragSpeed * Time.deltaTime;
        }
        else
        {
            if (Input.GetKey("w"))
            {
                pos.z += panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("s"))
            {
                pos.z -= panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("a"))
            {
                pos.x -= panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("d"))
            {
                pos.x += panSpeed * Time.deltaTime;
            }
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * Time.deltaTime;

        transform.position = pos;
        #endregion

        // Camera zoom
    }
}
