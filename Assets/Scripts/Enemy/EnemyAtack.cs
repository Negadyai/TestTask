using Spine;
using Spine.Unity;
using System;
using UnityEngine;
using Interfaces;
using System.Collections;

namespace Enemy
{
    public class EnemyAtack : MonoBehaviour
    {
        public event Action onEndAttack;

        [SerializeField] private SkeletonAnimation _skeletonAnimation;
        [SerializeField] private float _damage;
        private IDamageable _lastTarget;

        public void Attack(IDamageable target)
        {
            _lastTarget = target;
            StartCoroutine(AttackCoroutine());
        }

        private IEnumerator AttackCoroutine()
        {
            _skeletonAnimation.AnimationName = "angry";
            yield return new WaitForSeconds(_skeletonAnimation.skeletonDataAsset.GetSkeletonData(true).FindAnimation("angry").Duration);
            _lastTarget.ApplyDamage(_damage);
            _skeletonAnimation.AnimationName = "win";
        }
    }
}

