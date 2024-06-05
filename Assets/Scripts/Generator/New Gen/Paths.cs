using UnityEngine;

namespace Pathways
{
    public enum PathTypes
    {
        STRAIGHT,
        GOINGLEFT,
        GOINGRIGHT,
        TSECTION
    }

    public class Paths : MonoBehaviour
    {
        public PathTypes type;
        public Transform pivot;
    }
}
