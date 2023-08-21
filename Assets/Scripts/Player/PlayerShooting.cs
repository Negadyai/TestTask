using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using System;
using Spine.Unity;
using Spine;

public class PlayerShooting : MonoBehaviour
{
    public event Action onEndAttack;

    [SerializeField] private float _damage;
    [SerializeField] private SkeletonAnimation _skeletonAnimation;
    [SerializeField] private AudioSource _shootSound;
    [SerializeField] private ParticleSystem _shootParticles;

    private IDamageable _lastTarget;

    private EventData _eventData;

    private void Start()
    {
        _eventData = _skeletonAnimation.skeleton.Data.FindEvent("shooter/fire");
        _skeletonAnimation.AnimationState.Event += EnableShootEffects;
    }

    private void OnDisable()
    {
        _skeletonAnimation.AnimationState.Event -= EnableShootEffects;
    }

    public void Attack(IDamageable target)
    {
        _lastTarget = target;
        StartCoroutine(AttackCoroutine());
    }
    private IEnumerator AttackCoroutine()
    {
        _skeletonAnimation.AnimationName = "shoot";
        yield return new WaitForSeconds(_skeletonAnimation.skeletonDataAsset.GetSkeletonData(true).FindAnimation("shoot").Duration);
        _lastTarget.ApplyDamage(_damage);
        onEndAttack?.Invoke();
    }

    private void EnableShootEffects(TrackEntry trackEntry, Spine.Event e)
    {
        bool eventMatch = (_eventData == e.Data);
        if (eventMatch)
        {
            _shootParticles.Play();
            _shootSound.Play();
        }
    }
}
