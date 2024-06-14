using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

namespace Pathways
{
    public class RoomSpawnRestructure : RoomSpawn
    {
        [SerializeField] private NavMeshSurface navMeshSurface;
        [SerializeField] private int totalRoomsSpawned = 0;

        // New variables to limit consecutive turns
        [SerializeField] private int maxConsecutiveTurns = 2; // Adjust as needed
        [SerializeField] private int consecutiveTurnCount = 0;
        [System.Serializable]
        public class MainRooms
        {
            public GameObject pathPrefab;
            public PathType pathType;
            [Range(0, 1)] public float spawnChance;
        }

        [System.Serializable]
        public class EventRoom
        {
            public GameObject roomPrefab;
            public int roomEventNumber;
        }

        [System.Serializable]
        public class EndRoom
        {
            public GameObject roomPrefab;
            public int roomEventNumber;
        }


        public enum PathType
        {
            Straight,
            Turn
        }

        [SerializeField] List<MainRooms> mainRooms;
        [SerializeField] List<EventRoom> eventRooms;
        

        protected override void Start()
        {
            base.Start();
        }

        public override void StartSpawn()
        {
            currentRooms = new List<GameObject>();

            Random.InitState(System.DateTime.Now.Millisecond);

            SpawnPath();
        }

        private void SpawnPath()
        {
            PathType pathType = Random.value > 0.5f ? PathType.Straight : PathType.Turn;
            if (pathType == PathType.Turn)
            {
                consecutiveTurnCount++;
                if (consecutiveTurnCount > maxConsecutiveTurns)
                {
                    pathType = PathType.Straight;
                    consecutiveTurnCount = 0; // Reset consecutive turns count
                }
            }
            else
            {
                consecutiveTurnCount = 0; // Reset consecutive turns count on straight path
            }

            SpawnRoom(PickRoom(pathType).GetComponent<Paths>());

            RebakeNavMesh();
        }

        private GameObject PickEventRoom()
        {
            foreach (EventRoom eventRoom in eventRooms)
            {
                if (eventRoom.roomEventNumber == totalRoomsSpawned + 1)
                {
                    return eventRoom.roomPrefab;
                }
            }

            return null;
        }

        private GameObject PickRandomRoom(PathType pathType)
        {
            List<GameObject> eligibleMainRooms = new List<GameObject>();
            List<GameObject> eligibleDefaultRooms = new List<GameObject>();

            if (pathType == PathType.Straight)
            {
                eligibleDefaultRooms.AddRange(straightRooms);
            }
            else if (pathType == PathType.Turn)
            {
                eligibleDefaultRooms.AddRange(turnRoom);
            }

            foreach (MainRooms mainRoom in mainRooms)
            {
                if (mainRoom.pathType == pathType && Random.value <= mainRoom.spawnChance)
                {
                    eligibleMainRooms.Add(mainRoom.pathPrefab);
                }
            }

            if (eligibleMainRooms.Count > 0)
            {
                return eligibleMainRooms[Random.Range(0, eligibleMainRooms.Count)];
            }

            if (eligibleDefaultRooms.Count > 0)
            {
                return eligibleDefaultRooms[Random.Range(0, eligibleDefaultRooms.Count)];
            }

            return null; // Handle fallback scenario as needed
        }

        private GameObject PickRoom(PathType pathType)
        {
            totalRoomsSpawned++;
            GameObject eventRoom = PickEventRoom();
            if (eventRoom != null)
                return eventRoom;
            return PickRandomRoom(pathType);
        }

        public override void AddNewDirection(Vector3 direction, Vector3 childPos)
        {
            currentRoomDir = direction;

            DeletePrevRooms();

            currentRoomLoc = childPos + direction;

            SpawnPath();
        }

        private void RebakeNavMesh()
        {
            if (navMeshSurface != null)
            {
                navMeshSurface.BuildNavMesh();
                Debug.Log("Rebuilt navmesh surface!");
            }
            else
            {
                Debug.LogWarning("NavMeshSurface is not assigned to RoomSpawnRestructure!");
            }
        }

        private void OnDestroy()
        {
            // Ensure to unregister from events or clean up when the script is destroyed
            if (navMeshSurface != null)
            {
                navMeshSurface.RemoveData();
            }
        }
    }
}
