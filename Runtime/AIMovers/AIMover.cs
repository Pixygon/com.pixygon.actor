using System;
using Pixygon.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Pixygon.Actors {
    public class AIMover : MonoBehaviour {
        [SerializeField] protected SpriteRenderer _sprite;
        
        protected bool _xFlip;
        protected bool _rush;
        protected Actor _actor;
        protected bool _init;

        public Action<GameObject> AttackAction;
        public Action<GameObject> FindAction;
        public Action<Vector3> InvestigateAction;
        
        public bool Pause { get; protected set; }
        
        public virtual void Initialize(Actor actor, Action<GameObject> attackAction, Action<GameObject> findAction, Action<Vector3> investigateAction) {
            _actor = actor;
            AttackAction += attackAction;
            FindAction += findAction;
            InvestigateAction += investigateAction;
            SetupAIListeners();
            _init = true;
        }
        public virtual void SetPause(bool pause) {
            Pause = pause;
        }

        protected virtual void SetupAIListeners() {
            
        }
        protected virtual void HandleMovement() {
            
        }

        public virtual Action<GameObject> GetReaction(AIReaction reaction) {
            return reaction switch {
                AIReaction.Turn => Turn,
                AIReaction.Teleport => Teleport,
                AIReaction.Rush => Rush,
                AIReaction.Attack => Attack,
                AIReaction.Find => Find,
                AIReaction.Investigate => Investigate,
                AIReaction.None => null,
                _ => throw new ArgumentOutOfRangeException(nameof(reaction), reaction, null)
            };
        }

        protected void Turn(GameObject g = null) {
            _xFlip = !_xFlip;
            _sprite.flipX = _xFlip;
            _rush = false;
        }

        protected void Rush(GameObject g = null) {
            _rush = true;
        }

        protected void Teleport(GameObject g = null) {
            transform.position = (Vector2)transform.position + Random.insideUnitCircle.normalized * 5f;
        }
        protected void Attack(GameObject g = null) {
            //(_actor as MicroActorAI).Attack(g);
            AttackAction?.Invoke(g);
        }

        protected void Find(GameObject g = null) {
            //(_actor as MicroActorAI).Find(g);
            FindAction?.Invoke(g);
        }

        protected void Investigate(GameObject g = null) {
            //(_actor as MicroActorAI).Investigate(g.transform.position);
            InvestigateAction?.Invoke(g.transform.position);
        }

        private void Update() {
            if (PauseManager.Pause) return;
            if(!_init) return;
            HandleMovement();
        }
    }
}
