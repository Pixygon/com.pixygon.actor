using UnityEngine;

namespace Pixygon.Actors {
    public class ActorPatrolPattern : MonoBehaviour {
        [SerializeField] private PatrolData _patrolData;
        public PatrolData PatrolData => _patrolData;
    }
}