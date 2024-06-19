using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMouseLook : MonoBehaviour
{
    float mouseSensitivity;

    public Transform playerBody;

    float xRot = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (PlayerManager.Instance != null)
        {
            mouseSensitivity = PlayerManager.Instance.mouseLookSensitivity;
        }
        else
        {
            Debug.LogError("Player Manager Instance is Null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update mouse sensitivity from PlayerManager to reflect any changes
        if (PlayerManager.Instance != null)
        {
            mouseSensitivity = PlayerManager.Instance.mouseLookSensitivity;
        }
        else
        {
            Debug.LogError("Player Manager Instance is Null");
        }

        // Check if sensitivity is greater than zero before applying movement
        if (mouseSensitivity > 0f)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRot -= mouseY;
            xRot = Mathf.Clamp(xRot, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
