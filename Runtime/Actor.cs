using Pixygon.Core;
using Pixygon.Effects;
using UnityEngine;
using UnityEngine.Events;

namespace Pixygon.Actors {
    public class Actor : MonoBehaviour {
        [SerializeField] protected UnityEvent _onDie;
        [SerializeField] protected AilmentHandler _ailmentHandler;
        [SerializeField] protected IFrameHandler _iFrameHandler;
        [SerializeField] protected ActorController _actorController;
        [SerializeField] protected ActorStates _actorStates;
        [SerializeField] protected Renderer _renderer;
        protected ActorData _data;
        protected bool _isDead;
        protected bool _init;
        protected bool _isPaused;

        public bool Invincible;
        public ActorData ActorData => _data;
        public bool IsDead => _isDead;
        public UnityEvent OnDie => _onDie;
        public int Hp;
        public int MaxHp;
        public ActorPatrolPattern ActorPatrolPattern;

        public virtual void Initialize(ActorData data) {
            _data = data;
            MaxHp = data._hp;
            Hp = MaxHp;
            _init = true;
            _iFrameHandler.Initialize(this, _renderer);
            //_isDeathDataNotNull = _deathData != null;
            //_isDamageDataNotNull = _damageData != null;
            //_isAnimNotNull = _anim != null;
            //if(_isAnimNotNull) _anim.speed = _defaultAnimSpeed;
            if (PauseManager.Pause)
                OnPause();
        }

        private void OnEnable() {
            PauseManager.OnPause += OnPause;
            PauseManager.OnUnpause += OnUnpause;
            if (PauseManager.Pause) OnPause();
        }

        private void OnDisable() {
            PauseManager.OnPause -= OnPause;
            PauseManager.OnUnpause -= OnUnpause;
        }

        protected virtual void OnPause() {
            _isPaused = true;
            _actorController.Sleep();
        }

        protected virtual void OnUnpause() {
            _isPaused = false;
            _actorController.WakeUp();
        }

        public virtual void SetPatrolPattern(ActorPatrolPattern patrolPattern) {
            ActorPatrolPattern = patrolPattern;
        }

        public void Damage(DamageData damageData) {
            var dam = _ailmentHandler.IsDazed
                ? Mathf.RoundToInt(damageData.damage * 1.5f)
                : damageData.damage;
            Hp -= dam;
            var rank = 0;
            if (dam >= 200)
                rank = 1;
            if (dam >= 500)
                rank = 2;
            if (dam >= 800)
                rank = 3;
            EffectsManager.SpawnScoreEffect(dam, transform.position, null, damageData.critical, rank);
            foreach (var ailment in damageData.ailmentDatas) {
                _ailmentHandler.CheckSingleAilment(ailment);
            }

            _iFrameHandler.SetIFrames();
            //if (_isDamageDataNotNull) EffectsManager.SpawnEffect(_damageData.GetFullID, transform.position);
            if (Hp <= 0) Die();
        }

        protected virtual void Die() {
            if (_isDead) return;
            _isDead = true;
            Destroy(gameObject);
            //RoundManager.Instance.ObjectPoolManager.SpawnXp(transform.position);
            //if (_ailmentHandler.IsZapped && RoundManager.Instance.Player.Stats.AirZap) DoChainZap();
            //if(_isDeathDataNotNull) EffectsManager.SpawnEffect(_deathData.GetFullID, transform.position);
            _iFrameHandler.StopIFrames();
            //RoundManager.Instance.ScoreManager.KillEnemy(_data._score);
            _onDie?.Invoke();
            //GameManager.Instance.SaveData._enemiesDefeated += 1;
        }

        public void InstaKill() {
            _isDead = true;
            Destroy(gameObject);
            _iFrameHandler.StopIFrames();
        }
    }
}