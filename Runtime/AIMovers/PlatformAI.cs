using System;
using UnityEngine;

namespace Pixygon.Micro.AI {
    public class PlatformAI : AIMover {
        private float _force;
        private Action<bool> _listeners;
        
        public override void Initialize(MicroActor actor) {
            Turn();
            _actor = actor;
            SetupAIListeners();
            _init = true;
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
        protected override void HandleMovement() {
            _listeners?.Invoke(_xFlip);
            _force = (_xFlip ? -_actor.Data._speed : _actor.Data._speed) * (_rush ? 3f : 1f);
            _actor.Rigid.velocity = new Vector2(_force, _actor.Rigid.velocity.y);
        }
    }
}