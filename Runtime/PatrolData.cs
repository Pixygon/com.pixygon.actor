using System;

namespace Pixygon.Actors {
    [Serializable]
    public class PatrolData {
        public bool _usePatrol;
        public float _patrolGoalDistance = .2f;
        public float _maxDegreesDelta = 600f;
        public PatrolPointData[] _patrolPointDatas;
    }
}