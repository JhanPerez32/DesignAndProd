using Pathways;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoomDelete : MonoBehaviour
{
    public RoomSpawnRestructure RoomSpawnRestructure;

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (RoomSpawnRestructure != null) {
                RoomSpawnRestructure.DeletePrevRooms(this.gameObject);
                Destroy(this.gameObject, 2f);
            }
            else
            {
                Debug.LogError("RoomSpawn Script is not Assigned");
            }
        }
    }
}
