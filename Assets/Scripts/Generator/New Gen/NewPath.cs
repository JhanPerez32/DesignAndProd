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
                    // Check if the trigger object has a child
                    if (triggerObject.childCount > 0)
                    {
                        // Get the first child of the trigger object
                        Transform childTransform = triggerObject.GetChild(0);

                        // Get the position and rotation of the child object
                        Vector3 childPosition = childTransform.position;
                        Quaternion childRotation = childTransform.rotation;

                        // Convert the rotation to a direction vector
                        Vector3 newDirection = childRotation * Vector3.forward;

                        // Pass the direction and position to AddNewDirection method
                        roomSpawn.AddNewDirection(newDirection, childPosition);
                    }
                    else
                    {
                        Debug.LogWarning("Trigger object does not have any children.");
                    }
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
