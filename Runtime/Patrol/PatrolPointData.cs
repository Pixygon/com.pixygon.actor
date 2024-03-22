using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pixygon.Actors {
    [Serializable]
    public class PatrolPointData {
        [FormerlySerializedAs("_useLocalPos")] public bool _useWorldPos;
        public Vector3 _pos;
        public bool _onlyLook;
        public float _speed = .2f;
        public float _waitTime = .5f;

        public PatrolPointData() {
            _useWorldPos = true;
            _pos = Vector3.zero;
            _onlyLook = false;
            _speed = .2f;
            _waitTime = .5f;
        }
    }
}