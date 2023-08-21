using UnityEngine;

namespace Interfaces
{
    public interface IDamageable
    {
        public void ApplyDamage(float damage);
        public bool isDead { get; }
    }
}

