using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        public Player.PlayerController target { get; private set; }
        public bool isMoving { get; private set; }

        [SerializeField] private float _moveSpeed;
        [SerializeField] private SkeletonAnimation _skeletonAnimation;

        private void Awake()
        {
            target = FindObjectOfType<Player.PlayerController>();
        }

        private void Start()
        {
            if (target.transform.position.x > transform.position.x)
            {
                Flip();
            }
        }

        private void Update()
        {
            if (!isMoving) return;

            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, _moveSpeed * Time.deltaTime);
        }

        public void Move()
        {
            isMoving = true;
            _skeletonAnimation.AnimationName = "run";
        }

        public void Stop()
        {
            isMoving = false;
            _skeletonAnimation.AnimationName = "idle";
        }

        private void Flip()
        {
            Vector3 scaller = transform.localScale;
            scaller.x *= -1;
            transform.localScale = scaller;
        }
    }

}
