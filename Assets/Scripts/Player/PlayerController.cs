using UnityEngine;
using Interfaces;

namespace Player
{
    [RequireComponent(typeof(PlayerMovement), typeof(PlayerShooting), typeof(PlayerHealth))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private LayerMask _targetLayers;

        private PlayerMovement _movement;
        private PlayerShooting _shooting;
        private PlayerHealth _health;

        private bool _facingRight = true;

        private void Awake()
        {
            _movement = GetComponent<PlayerMovement>();
            _shooting = GetComponent<PlayerShooting>();
            _health = GetComponent<PlayerHealth>();

            _shooting.onEndAttack += OnEndAttack;

            _movement.Move();
        }

        private void OnDisable()
        {
            _shooting.onEndAttack -= OnEndAttack;
        }

        private void Update()
        {
            if (_health.isDead) return;

            Shoot();
        }

        private void Shoot()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (_facingRight && mousePosition.x < transform.position.x || !_facingRight && mousePosition.x > transform.position.x)
                {
                    Flip();
                }

                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 0.1f, _targetLayers);

                if (hit.transform == null) return;

                if (hit.transform.TryGetComponent(out IDamageable damageable))
                {
                    if (damageable.isDead) return;

                    _movement.Stop();
                    _shooting.Attack(damageable);
                }
            }
        }

        private void OnEndAttack()
        {
            _movement.Move();
            if (!_facingRight && transform.localScale.x < 0)
            {
                Flip();
            }
        }
        private void Flip()
        {
            _facingRight = !_facingRight;
            Vector3 scaller = transform.localScale;
            scaller.x *= -1;
            transform.localScale = scaller;
        }
    }
}

