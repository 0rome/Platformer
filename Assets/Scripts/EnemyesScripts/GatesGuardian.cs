using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.UIElements;

public class GatesGuardian : MonoBehaviour
{
    [SerializeField] private Gates gates;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private DialogueNodeSO startingNode;

    [SerializeField] private float radius = 5f;              // радиус действия
    [SerializeField] private LayerMask playerLayer;


    private bool isTriggered;
    private bool gateIsOpened;
    private EnemyHealth enemyHealth;

    public EnemyNeutrality[] allEnemyes;

    private void Awake()
    {
        allEnemyes = FindObjectsByType<EnemyNeutrality>(FindObjectsSortMode.None);
    }
    private void Start()
    {
        GetComponentInChildren<EnemyWeaponAim>().enabled = false;
        enemyHealth = GetComponentInChildren<EnemyHealth>();
    }
    private void Update()
    {
        CheckPlayer();
        
    }

    private void CheckPlayer()
    {
        Collider2D dialogueRadius = Physics2D.OverlapCircle(transform.position, radius, playerLayer);
        if (dialogueRadius != null && !isTriggered)
        {
            if (dialogueRadius.GetComponent<Inventory>().takenWeapons.Count <= 0 && gateIsOpened == false)
            {
                dialogueManager.StartDialogue(startingNode);
            }
            isTriggered = true;
        }

        Collider2D findRadius = Physics2D.OverlapCircle(transform.position, radius * 3, playerLayer);
        if (findRadius != null)
        {
            if (findRadius.GetComponent<Inventory>().takenWeapons.Count > 0)
            {
                GetComponentInChildren<EnemyWeaponAim>().enabled = true;
            }
        }
    }
    
    private void OnEnable()
    {
        DialogueManager.OnAllCorrect += Correct;
        DialogueManager.OnAnyWrong += Wrong;
        PlayerHealth.OnDead += ForgetAllStart;
    }
    private void OnDisable()
    {
        DialogueManager.OnAllCorrect -= Correct;
        DialogueManager.OnAnyWrong -= Wrong;
        PlayerHealth.OnDead -= ForgetAllStart;

        if (gates != null) gates.OpenGates();

    }

    private void Correct()
    {
        if (gates != null) gates.OpenGates();

        gateIsOpened = true;

        for (int i = 0; i < allEnemyes.Length; i++)
        {
            allEnemyes[i].IsNeutral = true;
        }
    }
    private void Wrong()
    {
        GetComponentInChildren<EnemyWeaponAim>().enabled = true;
    }

    private void ForgetAllStart()
    {
        StartCoroutine(ForgetAll());
    }
    private IEnumerator ForgetAll()
    {
        yield return new WaitForSeconds(2f);

        isTriggered = false;
        GetComponentInChildren<EnemyWeaponAim>().enabled = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawWireSphere(transform.position, radius * 3);
    }
}
