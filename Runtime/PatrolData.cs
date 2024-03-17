using System;
using UnityEngine;

namespace Pixygon.Actors {
    [Serializable]
    public class PatrolData {
        public bool _usePatrol;
        public float _patrolGoalDistance = .5f;
        public float _patrolSpeed = .2f;
        public float _maxDegreesDelta = 600f;
        public bool _patrolOnlyLook;
        public float _patrolWaitTime;
        public Vector3[] _patrolPoints;
        [ContextMenuItem("Upgrade to new system", "UpgradeToNewSystem")]
        public PatrolPointData[] _patrolPointDatas;
        
        

        private void UpgradeToNewSystem() {
            _patrolPointDatas = new PatrolPointData[_patrolPoints.Length];
            for (var i = 0; i < _patrolPoints.Length; i++) {
                _patrolPointDatas[i] = new PatrolPointData() {
                    _pos = _patrolPoints[i],
                    _speed = _patrolSpeed,
                    _onlyLook = _patrolOnlyLook,
                    _useLocalPos = false,
                    _waitTime = _patrolWaitTime
                };
            }
        }
    }
}