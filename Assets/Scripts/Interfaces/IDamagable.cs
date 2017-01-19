using UnityEngine;

namespace Assets.Scripts.Interfaces {
    interface IDamagable {
        void ReceiveDamage(float damageDone, Collider2D fromWho );
        void Die();
    }
}
