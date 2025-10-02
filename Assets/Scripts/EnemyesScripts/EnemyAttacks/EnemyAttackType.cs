using System;
using UnityEngine;

public abstract class EnemyAttackType : Enemy
{
    protected SoundPlay soundPlay;

    protected AbilityBase abilityBase;

    public abstract void Attack();

    protected virtual void Start()
    {
        soundPlay = transform.Find("Sounds").GetComponent<SoundPlay>();
        animator = GetComponent<Animator>();

        TryGetComponent(out abilityBase);
    }
}
