using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : MonoBehaviour
{
    float rotation_x = 0f;
    float rotation_y = 0f;

    public float sensitivity = 15f;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        rotation_y += Input.GetAxis("Mouse X") * sensitivity;
        rotation_x += Input.GetAxis("Mouse Y") * sensitivity;
        transform.localEulerAngles = new Vector3(-rotation_x, rotation_y, 0);
    }
}
