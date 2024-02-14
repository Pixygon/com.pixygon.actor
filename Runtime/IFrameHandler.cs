using UnityEngine;

namespace Pixygon.Actors {
    public class IFrameHandler : MonoBehaviour {
        private float _iFrameLength;
        private float _iFrames;
        private float _iFrameEffectCounter;
        private bool _iFrameRed;
        private Renderer _renderer;
        private Actor _actor;

        private SpriteRenderer _spriteRenderer;

        public void Initialize(Actor actor, Renderer renderer) {
            _renderer = renderer;
            if (renderer is SpriteRenderer spriteRenderer)
                _spriteRenderer = spriteRenderer;
            _actor = actor;
            _iFrameLength = .4f; //Fix this!! actor.ActorData._iFrameLength;
        }

        public void SetIFrames() {
            _actor.Invincible = true;
            _iFrames = _iFrameLength;
        }

        public void HandleIFrames() {
            if (!_actor.Invincible) return;
            if (_iFrames > 0f) {
                _iFrames -= Time.deltaTime;
                if (_iFrameEffectCounter > 0f)
                    _iFrameEffectCounter -= Time.deltaTime;
                else {
                    _iFrameRed = !_iFrameRed;
                    _iFrameEffectCounter = .1f;
                    _spriteRenderer.color = _iFrameRed ? Color.red : Color.white;
                }
            }
            else
                StopIFrames();
        }

        public void StopIFrames() {
            _iFrameEffectCounter = 0f;
            _actor.Invincible = false;
            _iFrameRed = false;
            _spriteRenderer.color = Color.white;
        }
    }
}