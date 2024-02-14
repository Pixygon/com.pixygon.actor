using UnityEngine;

namespace Pixygon.Actors {
    public class ActorController : MonoBehaviour
    {
        public virtual void Sleep() {
            //if(_rigid != null) _rigid.Sleep();
            //if(_isAnimNotNull) _anim.speed = 0f;
            //if(_isAnimNotNull && _anim != null) _anim.speed = 0f;
        }

        public virtual void WakeUp() {
            //if(_rigid != null) _rigid.WakeUp();
            //if(_isAnimNotNull) _anim.speed = _defaultAnimSpeed;
            //if(_isAnimNotNull && _anim != null) _anim.speed = _defaultAnimSpeed;
        }
    }
}