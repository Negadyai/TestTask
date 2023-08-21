using UnityEngine;
using Interfaces;

namespace Enemy
{
    [RequireComponent(typeof(EnemyMovement), typeof(EnemyAtack), typeof(EnemyHealth))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float _maxDistanceToPlayer;
        private EnemyMovement _movement;
        private EnemyAtack _attack;

        private bool _attacked;

        private void Awake()
        {
            _movement = GetComponent<EnemyMovement>();
            _attack = GetComponent<EnemyAtack>();

            _attack.onEndAttack += OnEndAttack;

            _movement.Move();
        }

        private void OnDisable()
        {
            _attack.onEndAttack -= OnEndAttack;
        }

        private void Update()
        {
            if (_attacked) return;

            if (Vector2.Distance(transform.position, _movement.target.transform.position) <= _maxDistanceToPlayer)
            {
                if (_movement.isMoving)
                {
                    _movement.Stop();
                }
                _movement.target.TryGetComponent(out IDamageable damageable);
                _attack.Attack(damageable);
                _attacked = true;
            }
        }

        private void OnEndAttack()
        {
            
        }
    }
}

