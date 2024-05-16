using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSection : MonoBehaviour
{
    RoomSpawner roomSpawner;

    private void Start()
    {
        roomSpawner = GameObject.FindObjectOfType<RoomSpawner>();
    }

    private void OnTriggerExit(Collider other)
    {
        roomSpawner.SpawnRoom();
        Destroy(gameObject, 2);
    }
}
