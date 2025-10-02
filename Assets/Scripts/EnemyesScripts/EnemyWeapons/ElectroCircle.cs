using UnityEngine;
using System.Collections;

public class ElectroCircle : MonoBehaviour
{
    [SerializeField] private float lifeTime;

    private Animator animator;
    private SoundPlay soundPlay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        soundPlay = GetComponent<SoundPlay>();

        StartCoroutine(Life());

        soundPlay.PlaySound(0);
    }

    private IEnumerator Life()
    {

        yield return new WaitForSeconds(lifeTime);

        animator.SetTrigger("End");

        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }
}
