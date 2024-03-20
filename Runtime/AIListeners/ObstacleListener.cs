using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Pixygon.Actors {
    public class ObstacleListener : AIListener {
        private ObstacleListenerData _data;
        private Vector3 _pos;
        private float _reactionTime;
        private bool _isReacting;
        private GameObject _reactionTarget;
        public override void SetupListener(AIListenerData data, Action<GameObject> action) {
            base.SetupListener(data, action);
            _data = data as ObstacleListenerData;
        }
        public override void HandleListener(bool xFlip) {
            base.HandleListener(xFlip);
            if (_isReacting) {
                _reactionTime -= Time.deltaTime;
                if(_reactionTime < 0f)
                    DoReact();
                return;
            }
            var dir = new Vector3(xFlip ? -_data._bodyOffset.x : _data._bodyOffset.x, _data._bodyOffset.y, 0f);
            if (_data._followObjectRotation)
                dir = transform.right;
            dir = Quaternion.AngleAxis(_data._directionOffset, Vector3.forward) * dir;
            RaycastHit2D hit;
            if (_data._isCircular) {
                _pos = transform.position + (transform.up * _data._bodyOffset.y);
                hit = Physics2D.CircleCast(_pos, _data._distance, Vector2.zero, 0f, _data._hitMask);
                if (_data._debugMode) {
                    if (hit.collider != null)
                        Debug.Log($"{gameObject.name} Circle Hit {hit.collider.gameObject.name}");
                }
            } else {
                _pos = transform.position + _data._heightOffset + new Vector3(xFlip ? -_data._bodyOffset.x : _data._bodyOffset.x, 0f, 0f);
                hit = Physics2D.Raycast(_pos, dir, _data._distance, _data._hitMask);
                if (_data._debugMode) {
                    Debug.DrawRay(_pos, dir*_data._distance, _data._color);
                    if (hit.collider != null)
                        Debug.Log($"{gameObject.name} Ray Hit {hit.collider.gameObject.name}");
                }
            }

            if (_data._reactOnHit) {
                if (hit.collider == null) return;
                if (!_data._hitSelf && hit.collider.gameObject == gameObject) return;
                if(_data._canBreakTargeting && _data._useTag && !hit.collider.CompareTag(_data._tag)) return;
                if (_data._useTag && !hit.collider.CompareTag(_data._tag)) return;
                StartReaction(hit.collider.gameObject);
            } else {
                if (hit.collider == null) _triggerAction.Invoke(null);
            }
        }


        private void StartReaction(GameObject target) {
            _isReacting = true;
            _reactionTime = Random.Range(0f, _data._reactionTime);
            _reactionTarget = target;
        }

        private void DoReact() {
            _isReacting = false;
            _triggerAction.Invoke(_reactionTarget);
            _reactionTarget = null;
        }

        private void OnDrawGizmos() {
            if (!_data._debugMode) return;
            if (!_data._isCircular) return;
            Gizmos.color = _data._color;
            Gizmos.DrawSphere(_pos, _data._distance);
        }
    }
}