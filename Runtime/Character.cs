using Pixygon.Actors;
using UnityEngine;

namespace Pixygon.Dialogue {
    [CreateAssetMenu(menuName = "Pixygon/Character", fileName = "New Character")]
    public class Character : ScriptableObject {
        public string _title;
        public string _description;
        public Sprite _face;
        public ActorData _connectedActor;
        public AnimatorOverrideController _faceOverride;
        public AudioClip _voice;
    }
}