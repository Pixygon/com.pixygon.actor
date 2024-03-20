using Pixygon.Effects;
using Pixygon.ID;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Pixygon.Actors {
    [CreateAssetMenu(menuName = "Pixygon/Actors", fileName = "New ActorData", order = 0)]
    public class ActorData : IdObject {
        public AssetReference _actorRef;
        public bool _isHostile;
        public bool _isKillable;
        //public AIListenerData[] _listeners; <-- ADD
        
        public EffectData _damageFx;
        public EffectData _deathFx;

        public bool _useIframes;
        public float _iFrameLength = .6f;
        public int _hp;
        public float _speed;
        public int _score;
        public float _reactionTime = .5f;
        [Range(0f, 2f)] public float _poise = 1f;
        [Range(0f, 1f)] public float _burnResistance;
        [Range(0f, 1f)] public float _dazeResistance;
        [Range(0f, 1f)] public float _freezeResistance;
        [Range(0f, 1f)] public float _zapResistance;
    }
}