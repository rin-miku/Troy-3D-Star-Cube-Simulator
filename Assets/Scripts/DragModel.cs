using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragModel : MonoBehaviour
{
    public GameObject model;
    public float rotationSpeed = 50f;

    private Vector3 previousMousePosition;

    void Update()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            CheckTap();
        }
        else
        {
            CheckMouse();
        }
    }

    private void CheckMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 delta = currentMousePosition - previousMousePosition;

            float rotX = -delta.x * rotationSpeed * Time.deltaTime;
            float rotY = delta.y * rotationSpeed * Time.deltaTime;

            Quaternion rotation = Quaternion.Euler(0, rotX, rotY);

            model.transform.Rotate(rotation.eulerAngles);

            previousMousePosition = currentMousePosition;
        }
    }

    private void CheckTap()
    {
        if(Input.touchCount == 2)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 delta = currentMousePosition - previousMousePosition;

            float rotX = -delta.x * rotationSpeed * Time.deltaTime;
            float rotY = delta.y * rotationSpeed * Time.deltaTime;

            Quaternion rotation = Quaternion.Euler(0, rotX, rotY);

            model.transform.Rotate(rotation.eulerAngles);

            previousMousePosition = currentMousePosition;
        }
    }
}
