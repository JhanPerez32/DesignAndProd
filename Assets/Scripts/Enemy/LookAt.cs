using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    private void LateUpdate()
    {
        Vector3 targetPosition = transform.position + Camera.main.transform.rotation * Vector3.forward;
        Vector3 upDirection = Camera.main.transform.rotation * Vector3.up;

        transform.LookAt(targetPosition, upDirection);
    }
}