using Spine.Unity;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;

        [SerializeField] private SkeletonAnimation _skeletonAnimation;

        private bool _isMoving;

        private void Update()
        {
            if (!_isMoving) return;

            transform.position += Vector3.right * _moveSpeed * Time.deltaTime;
        }

        public void Move()
        {
            _isMoving = true;
            _skeletonAnimation.AnimationName = "walk";
        }
        public void Stop()
        {
            _isMoving = false;
            _skeletonAnimation.AnimationName = "idle";
        }
    }
}

