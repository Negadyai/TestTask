using UnityEngine;
using Interfaces;
using Spine.Unity;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Player
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        public bool isDead { get; private set; }

        [SerializeField] private float _health;
        [SerializeField] private SkeletonAnimation _skeletonAnimation;

        public void ApplyDamage(float damage)
        {
            if (isDead) return;

            if (_health - damage <= 0)
            {
                _health -= damage;
                isDead = true;
                _skeletonAnimation.loop = false;
                _skeletonAnimation.AnimationName = "loose";
                StartCoroutine(Die());
            }

            _health -= damage;
        }

        private IEnumerator Die()
        {
            yield return new WaitForSeconds(_skeletonAnimation.skeletonDataAsset.GetSkeletonData(true).FindAnimation("loose").Duration);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

