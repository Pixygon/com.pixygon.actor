using UnityEngine;

namespace Pixygon.Actors {
    [CreateAssetMenu(menuName = "Pixygon/Actor/Obstacle Listener Data", fileName = "New ObstacleListenerData")]
    public class ObstacleListenerData : AIListenerData {
        public bool _isCircular;
        public float _distance;
        public bool _reactOnHit;
        public Vector3 _bodyOffset;
        public Vector3 _heightOffset;
        [Range(0f, 360f)] public float _directionOffset;
        public LayerMask _hitMask;
        public bool _hitSelf;
        public bool _useTag;
        public string _tag;
        public bool _followObjectRotation;
        public bool _canBreakTargeting;
        public float _reactionTime = .1f;
    }
}