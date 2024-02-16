using UnityEngine;

namespace Pixygon.Actors {
    public class ActorPatrolPattern : MonoBehaviour {
        [SerializeField] private Vector3[] _positions;
        public Vector3[] Positions => _positions;
    }
}