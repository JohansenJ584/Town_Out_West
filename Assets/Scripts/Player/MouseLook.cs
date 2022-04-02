using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    float xRotation = 0f;
    public Transform playerBody;
    //public Transform playerSword;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0F, 0F);
        
        playerBody.Rotate(Vector3.up * mouseX);

        //playerSword.Rotate(Vector3.up * mouseX);
        //playerSword.localRotation =  Quaternion.Euler(xRotation, 0F, 0F);
    }
}
