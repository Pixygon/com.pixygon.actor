using UnityEngine;

namespace Pixygon.Weapons {
    public class SoundCollider : MonoBehaviour {
        [SerializeField] private CircleCollider2D _soundCollider;

        private float _timer;
        
        public void Trigger(float f) {
            _soundCollider.enabled = true;
            _soundCollider.radius = f;
            _timer = .5f;
        }

        public void ToggleOff() {
            _soundCollider.enabled = false;
        }

        private void Update() {
            if (!_soundCollider.enabled) return;
            _timer -= Time.deltaTime;
            if (_timer < 0f)
                ToggleOff();
        }
    }
}