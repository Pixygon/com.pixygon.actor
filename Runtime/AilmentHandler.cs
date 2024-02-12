    using Pixygon.Core;
    using UnityEngine;

    namespace Pixygon.Actors {
        public class AilmentHandler : MonoBehaviour {
            [SerializeField] private GameObject _burnObject;
            [SerializeField] private GameObject _dazeObject;
            [SerializeField] private GameObject _freezeObject;
            [SerializeField] private GameObject _zapObject;
            [SerializeField] private Actor _actor;
            public bool IsBurning;
            public bool IsDazed;
            public bool IsFreezed;
            public bool IsZapped;
            private float _burnTimer;
            private float _dazeTimer;
            private float _freezeTimer;
            private float _zapTimer;
            private float _burnTick;

            private bool _hasAnyAilment;
            
            public void CheckSingleAilment(AilmentData ailment) {
                var resistance = ailment.causedAilment switch {
                    Ailments.Burn => (float)_actor.ActorData._burnResistance,
                    Ailments.Daze => _actor.ActorData._dazeResistance,
                    Ailments.Freeze => _actor.ActorData._freezeResistance,
                    Ailments.Zap => _actor.ActorData._zapResistance,
                    _ => 0f
                };
                if (Random.value < ailment.ailmentActivationChance - resistance)
                    SetAilment(ailment.causedAilment, ailment.ailmentDuration);
            }

            private void SetAilment(Ailments ailment, float duration) {
                _hasAnyAilment = true;
                switch (ailment) {
                    case Ailments.Burn:
                        _burnTimer = duration;
                        _burnObject.SetActive(true);
                        IsBurning = true;
                        break;
                    case Ailments.Daze:
                        _dazeTimer = duration;
                        _dazeObject.SetActive(true);
                        IsDazed = true;
                        break;
                    case Ailments.Freeze:
                        _freezeTimer = duration;
                        _freezeObject.SetActive(true);
                        IsFreezed = true;
                        break;
                    case Ailments.Zap:
                        _zapTimer = duration;
                        _zapObject.SetActive(true);
                        IsZapped = true;
                        break;
                }
            }

            private void Update() {
                if (PauseManager.Pause) return;
                HandleAilment();
            }

            private void HandleAilment() {
                if (!_hasAnyAilment) return;
                if (_burnTimer < 0 && IsBurning) {
                    _burnObject.SetActive(false);
                    IsBurning = false;
                }
                else if (IsBurning) {
                    _burnTimer -= Time.deltaTime;
                    if (_burnTick < 0) {
                        _actor.Damage(new DamageData(Mathf.RoundToInt(_actor.MaxHp * .05f)));
                        _burnTick = .5f;
                    }
                    else {
                        _burnTick -= Time.deltaTime;
                    }
                }

                if (_dazeTimer < 0 && IsDazed) {
                    _dazeObject.SetActive(false);
                    IsDazed = false;
                }
                else if (IsDazed) _dazeTimer -= Time.deltaTime;

                if (_freezeTimer < 0 && IsFreezed) {
                    _freezeObject.SetActive(false);
                    IsFreezed = false;
                }
                else if (IsFreezed) _freezeTimer -= Time.deltaTime;

                if (_zapTimer < 0 && IsZapped) {
                    _zapObject.SetActive(false);
                    IsZapped = false;
                }
                else if (IsZapped) _zapTimer -= Time.deltaTime;

                if (!IsBurning && !IsDazed && !IsFreezed & !IsZapped)
                    _hasAnyAilment = false;
            }
        }
    }