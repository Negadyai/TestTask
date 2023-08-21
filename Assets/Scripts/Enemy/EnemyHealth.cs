using UnityEngine;
using Interfaces;
using System.Collections;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        public bool isDead { get; private set; }

        [SerializeField] private float _health;
        [SerializeField] private AudioSource _applyDamageSound;
        [SerializeField] private ParticleSystem _applyDamagePartcles;

        public void ApplyDamage(float damage)
        {
            if (isDead) return;

            _applyDamagePartcles.Play();
            _applyDamageSound.Play();

            if (_health - damage <= 0)
            {
                _health -= damage;
                isDead = true;
                StartCoroutine(Die());
            }
            _health -= damage;
        }

        private IEnumerator Die()
        {
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }
    }
}

