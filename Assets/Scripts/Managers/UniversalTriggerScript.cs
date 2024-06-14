using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class UniversalTriggerScript : MonoBehaviour
{

    // UnityEvent that you can configure in the Inspector
    public UnityEvent onTriggerEnter;

    void OnTriggerEnter(Collider other)
    {
        // Invoke all the functions assigned to the event
        onTriggerEnter.Invoke();
    }
}
