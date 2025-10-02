using UnityEngine;
using System.Collections;

public class ShadowSkill : AbilityBase
{
    [SerializeField] private int reloadTime;
    [SerializeField] private GameObject projectileObject;

    private bool isCharged = true;


    protected override void Start()
    {
        base.Start();
    }
    public override void Ability()
    {
        if (isCharged)
        {
            Instantiate(projectileObject, transform.position, Quaternion.identity);
            StartCoroutine(charging());
            soundPlay.PlaySound(2);
            isCharged = false;
        }
    }

    private IEnumerator charging()
    {
        yield return new WaitForSeconds(reloadTime);
        isCharged = true;
    }
    private void OnEnable()
    {
        currentEnemy.OnAttack += Ability;
    }
    private void OnDisable()
    {
        currentEnemy.OnAttack -= Ability;
    }
}
