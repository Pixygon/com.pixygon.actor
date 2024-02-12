using System;

namespace Pixygon.Actors {
    public class DamageData {
        public int damage;
        public bool critical = false;
        public AilmentData[] ailmentDatas;

        public DamageData(int damage, bool critical = false, AilmentData[] ailments = null) {
            this.damage = damage;
            this.critical = critical;
            ailmentDatas = ailments ?? Array.Empty<AilmentData>();
        }
    }
}