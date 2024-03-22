using UnityEngine;

namespace Pixygon.Actors {
    [CreateAssetMenu(menuName = "Pixygon/Actors/New PatrolPattern", fileName = "New PatrolPattern", order = 0)]
    public class PatrolPattern : ScriptableObject {
        public PatrolPointData[] _patrolPointDatas;
    }
}