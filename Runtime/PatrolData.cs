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
        public PatrolPointData[] _patrolPointDatas;
    }
}