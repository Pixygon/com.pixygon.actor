using System;
using UnityEngine;
using Pixygon.Actors;

namespace Pixygon.Micro.AI {
    public class TopDownAI : AIMover {
        
        private float _force;
        private Action<bool> _listeners;
        private Transform _player;
        private int _currentPatrolGoal;
        private float _patrolWait;
        private float _lookTime;

        private bool _useNewPatrolSystem = true;
        
        private PatrolData _patrolData;
        private Vector3 _startPos;
        
        public override void Initialize(MicroActor actor) {
            Turn();
            _actor = actor;
            SetupAIListeners();
            _init = true;
            if (_actor.ActorPatrolData != null)
                _patrolData = _actor.ActorPatrolData;
            _startPos = transform.position;
        }
        protected override void SetupAIListeners() {
            foreach (var listener in _actor.Data._listeners) {
                switch (listener) {
                    case ObstacleListenerData: {
                        var obstacleListener = gameObject.AddComponent<ObstacleListener>();
                        obstacleListener.SetupListener(listener, GetReaction(listener._reaction));
                        _listeners += obstacleListener.HandleListener;
                        break;
                    }
                }
            }
        }
        private void HandlePatrol() {
            /*
            if (_patrolWait > 0f) {
                _patrolWait -= Time.deltaTime;
                return;
            }
            if (_patrolData._patrolOnlyLook) {
                if (_lookTime > 0f) {
                    _lookTime -= Time.deltaTime;
                    var lookPos = _patrolData._patrolPoints[_currentPatrolGoal] - transform.position;
                    var angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
                    var qTo = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, qTo, _patrolData._maxDegreesDelta * Time.deltaTime);
                    return;
                } else {
                    if (_currentPatrolGoal == _patrolData._patrolPoints.Length - 1)
                        _currentPatrolGoal = 0;
                    else
                        _currentPatrolGoal += 1;
                    _patrolWait = _patrolData._patrolWaitTime;
                    _lookTime = 1f;
                }
                return;
            }
            if (Vector3.Distance(_patrolData._patrolPoints[_currentPatrolGoal], transform.position) <= _patrolData._patrolGoalDistance) {
                if (_currentPatrolGoal == _patrolData._patrolPoints.Length - 1)
                    _currentPatrolGoal = 0;
                else
                    _currentPatrolGoal += 1;
                _patrolWait = _patrolData._patrolWaitTime;
            } else {
                _actor.Rigid.MovePosition(Vector3.MoveTowards(transform.position, _patrolData._patrolPoints[_currentPatrolGoal], _patrolData._patrolSpeed));
                //transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.up, _patrolPoints[_currentPatrolGoal] - transform.position, _maxRadiansDelta, _maxMagnitudeDelta));
                var lookPos = _patrolData._patrolPoints[_currentPatrolGoal] - transform.position;
                var angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
                var qTo = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, qTo, _patrolData._maxDegreesDelta * Time.deltaTime);
            }
            */
        }
        private void HandleNewPatrol() {
            if (_patrolWait > 0f) {
                _patrolWait -= Time.deltaTime;
                return;
            }
            if (_patrolData._patrolPointDatas[_currentPatrolGoal]._onlyLook) {
                if (_lookTime > 0f) {
                    _lookTime -= Time.deltaTime;
                    var lookPos = _patrolData._patrolPointDatas[_currentPatrolGoal]._pos - transform.position;
                    var angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
                    var qTo = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, qTo, _patrolData._maxDegreesDelta * Time.deltaTime);
                    return;
                } else {
                    if (_currentPatrolGoal == _patrolData._patrolPointDatas.Length - 1)
                        _currentPatrolGoal = 0;
                    else
                        _currentPatrolGoal += 1;
                    _patrolWait = _patrolData._patrolPointDatas[_currentPatrolGoal]._waitTime;
                    _lookTime = 1f;
                }
                return;
            }
            if (Vector3.Distance(_patrolData._patrolPointDatas[_currentPatrolGoal]._pos, transform.position) <= _patrolData._patrolGoalDistance) {
                if (_currentPatrolGoal == _patrolData._patrolPointDatas.Length - 1)
                    _currentPatrolGoal = 0;
                else
                    _currentPatrolGoal += 1;
                _patrolWait = _patrolData._patrolPointDatas[_currentPatrolGoal]._waitTime;
            } else {
                _actor.Rigid.MovePosition(Vector3.MoveTowards(transform.position, _patrolData._patrolPointDatas[_currentPatrolGoal]._pos, _patrolData._patrolPointDatas[_currentPatrolGoal]._speed));
                //transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.up, _patrolPoints[_currentPatrolGoal] - transform.position, _maxRadiansDelta, _maxMagnitudeDelta));
                var lookPos = _patrolData._patrolPointDatas[_currentPatrolGoal]._pos - transform.position;
                var angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
                var qTo = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, qTo, _patrolData._maxDegreesDelta * Time.deltaTime);
            }
        }
        private void HandleTargeting() {
            var target = ((_actor as MicroActorAI).FoundTarget.position);
            transform.up = (target - transform.position).normalized;
            var distance = Vector3.Distance(target, transform.position);
            switch (distance) {
                case >= 25f:
                    (_actor as MicroActorAI).ReleaseTarget();
                    break;
                case < 25f and >= 5f:
                    _actor.Rigid.MovePosition(Vector3.MoveTowards(transform.position, target, _actor.Data._speed));
                    break;
                case < 3f:
                    _actor.Rigid.MovePosition(Vector3.MoveTowards(transform.position, (target - transform.position).normalized*5f, _actor.Data._speed));
                    break;
            }
        }
        private void HandleInvestigate() {
            var target = (_actor as MicroActorAI).InvestigateTarget;
            transform.up = (target - transform.position).normalized;
            var distance = Vector3.Distance(target, transform.position);
            switch (distance) {
                case >= 25f:
                    (_actor as MicroActorAI).StopInvestigating();
                    break;
                case < 25f:
                    _actor.Rigid.MovePosition(Vector3.MoveTowards(transform.position, target, _actor.Data._speed));
                    break;
            }
        }
        protected override void HandleMovement() {
            _listeners?.Invoke(_xFlip);
            if ((_actor as MicroActorAI).FoundTarget == null) {
                if ((_actor as MicroActorAI).ShouldInvestigate) {
                    HandleInvestigate();
                    return;
                } else {
                    if (_patrolData._usePatrol)
                        HandleNewPatrol();
                    else
                        transform.Rotate(Vector3.forward, Time.deltaTime*3f);
                    return;
                }
            }
            HandleTargeting();
        }
    }
}
