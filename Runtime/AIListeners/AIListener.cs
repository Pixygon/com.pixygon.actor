using System;
using UnityEngine;

namespace Pixygon.Actors {
    public class AIListener : MonoBehaviour {
        public Action<GameObject> _triggerAction;
        public bool Active { get; set; }
        public virtual void SetupListener(AIListenerData data, Action<GameObject> action) {
            Active = true;
            _triggerAction = action;
        }
        public virtual void HandleListener() {
            if (!Active) return;
        }
        public virtual void HandleListener(bool xFlip) {
            if (!Active) return;
        }
    }
}