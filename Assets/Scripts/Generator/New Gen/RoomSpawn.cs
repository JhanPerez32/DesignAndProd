using System.Collections.Generic;
using UnityEngine;

namespace Pathways
{

    public class RoomSpawn : MonoBehaviour
    {
        [SerializeField] int roomStartCount = 10;
        [SerializeField] int minStarightRooms = 3;
        [SerializeField] int maxStarightRooms = 10;
        [SerializeField] GameObject startingRoom;
        [SerializeField] List<GameObject> turnRoom;

        private Vector3 currentRoomLoc = Vector3.zero;
        private Vector3 currentRoomDir = Vector3.forward;
        private GameObject prevRoom;

        private List<GameObject> currentRooms;

        private void Start()
        {
            currentRooms = new List<GameObject>();

            Random.InitState(System.DateTime.Now.Millisecond);

            for(int i = 0; i <roomStartCount; i++)
            {
                SpawnRoom(startingRoom.GetComponent<Paths>());
            }

            SpawnRoom(SelectRandomGOFromList(turnRoom).GetComponent<Paths>());
        }

        public void SpawnRoom(Paths paths)
        {
            Quaternion newTileRotation = paths.gameObject.transform.rotation * Quaternion.LookRotation(currentRoomDir, Vector3.up);

            prevRoom = GameObject.Instantiate(paths.gameObject, currentRoomLoc, newTileRotation);
            currentRooms.Add(prevRoom);

            Renderer renderer = prevRoom.GetComponentInChildren<Renderer>();
            if (renderer != null)
            {
                if (paths.type == PathTypes.STRAIGHT)
                {
                    currentRoomLoc += Vector3.Scale(renderer.bounds.size, currentRoomDir);
                }    
            }
            else
            {
                Debug.LogWarning("No Renderer found in the child of the instantiated GameObject.");
            }
        }

        public void DeletPrevRooms()
        {
            while (currentRooms.Count != 1)
            {
                GameObject room = currentRooms[0];
                currentRooms.RemoveAt(0);
                Destroy(room);
            }
        }

        public void AddNewDirection(Vector3 direction)
{
    currentRoomDir = direction;
    DeletPrevRooms();

    Vector3 roomPlacementScale;

    Renderer renderer = prevRoom.GetComponentInChildren<Renderer>();
    MeshCollider[] meshColliders = startingRoom.GetComponentsInChildren<MeshCollider>();

    if (renderer == null || meshColliders.Length == 0)
    {
        Debug.LogWarning("Renderer or MeshColliders are missing.");
        return;
    }

    // Get the bounds of the renderer
    Bounds rendererBounds = renderer.bounds;

    // Initialize a combined bounds structure
    Bounds combinedMeshBounds = new Bounds(meshColliders[0].bounds.center, meshColliders[0].bounds.size);

    // Combine the bounds of all mesh colliders
    for (int i = 1; i < meshColliders.Length; i++)
    {
        combinedMeshBounds.Encapsulate(meshColliders[i].bounds);
    }

    if (prevRoom.GetComponent<Paths>().type == PathTypes.TSECTION)
    {
        roomPlacementScale = Vector3.Scale(rendererBounds.size / 2 + (Vector3.one * combinedMeshBounds.size.z / 2), currentRoomDir);
    }
    else
    {
        roomPlacementScale = Vector3.Scale((rendererBounds.size - (Vector3.one * -20)) + (Vector3.one * combinedMeshBounds.size.z / 2), currentRoomDir);
    }                                                           //I don't know if this is Proper, but it managed to
                                                                //snap on the continuation of the Path, Vector3.one * -20

    currentRoomLoc += roomPlacementScale;

    int currentPathLength = Random.Range(minStarightRooms, maxStarightRooms);
    for (int i = 0; i < currentPathLength; i++)
    {
        SpawnRoom(startingRoom.GetComponent<Paths>());
    }

    SpawnRoom(SelectRandomGOFromList(turnRoom).GetComponent<Paths>());
}

        private GameObject SelectRandomGOFromList(List<GameObject> list)
        {
            if (list.Count == 0)
            {
                return null;
            }
            return list[Random.Range(0, list.Count)];
        }
    }
}
