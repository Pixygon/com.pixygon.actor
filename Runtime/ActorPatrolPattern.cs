using UnityEngine;

namespace Pixygon.Actors {
    public class ActorPatrolPattern : MonoBehaviour {
        [SerializeField] private Vector3[] _positions;
        [SerializeField] private PatrolData _patrolData;
        public Vector3[] Positions => _positions;
        public PatrolData PatrolData => _patrolData;
    }
}