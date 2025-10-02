using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class BotWheelTeleportation : AbilityBase
{
    [SerializeField] private ParticleSystem teleportationEffect;

    private GameObject player;

    protected bool isCharged = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        Ability();
    }

    public override void Ability()
    {
        if (Vector2.Distance(transform.position,player.transform.position) <= 6 && isCharged)
        {
            soundPlay.PlaySound(1);
            teleportationEffect.Play();
            Teleportation();
            isCharged = false;
            StartCoroutine(charging());
        }
    }
    
    private IEnumerator charging()
    {
        yield return new WaitForSeconds(5);
        isCharged = true;
    }

    private void Teleportation()
    {
        if (transform.localScale.x > 0)
        {
            transform.position = new Vector2(transform.position.x + 6, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - 6, transform.position.y);
        }
    }
}
