using System;
using UnityEngine;

namespace Pixygon.Actors {
    [Serializable]
    public class PatrolPointData {
        public bool _useLocalPos;
        public Vector3 _pos;
        public bool _onlyLook;
        public float _speed = .2f;
        public float _waitTime = .5f;
    }
}