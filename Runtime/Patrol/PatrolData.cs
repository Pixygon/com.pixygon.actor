using System;
using UnityEngine;

namespace Pixygon.Actors {
    [Serializable]
    public class PatrolData {
        public bool _usePatrol;
        public float _patrolGoalDistance = .2f;
        public float _maxDegreesDelta = 600f;
        [SerializeField] private PatrolPattern _patrolPattern;
        [SerializeField] private PatrolPointData[] _patrolPointDatas;

        public PatrolPointData[] PatrolPointData =>
            _patrolPattern == null ? _patrolPointDatas : _patrolPattern._patrolPointDatas;
    }
}