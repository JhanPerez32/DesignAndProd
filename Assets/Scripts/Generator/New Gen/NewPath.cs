using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathways
{
    public class NewPath : MonoBehaviour
    {
        Paths paths;
        RoomSpawn roomSpawn;

        public Transform triggerObject;

        private void Start()
        {
            roomSpawn = GameObject.FindObjectOfType<RoomSpawn>();
            paths = GameObject.FindObjectOfType<Paths>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (paths != null)
            {
                if (triggerObject != null)
                {
                    // Get the rotation of the trigger object
                    Quaternion triggerRotation = triggerObject.rotation;

                    // Convert the rotation to a direction vector
                    Vector3 newDirection = triggerRotation * Vector3.forward;

                    // Pass the direction to AddNewDirection method
                    roomSpawn.AddNewDirection(newDirection);
                }
                else
                {
                    Debug.LogWarning("Trigger object reference is not set in NewPath script.");
                }
            }
            else
            {
                Debug.LogWarning("Paths is not assigned in NewPath script.");
            }
        }
    }
}
