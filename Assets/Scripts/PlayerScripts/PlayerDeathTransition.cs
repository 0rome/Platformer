using UnityEngine;
using System.Collections;

public class PlayerDeathTransition : MonoBehaviour
{
    private Animator transitionsAnimator;

    private SoundPlay soundPlay;

    private void Start()
    {
        transitionsAnimator = GetComponent<Animator>();

        soundPlay = GetComponent<SoundPlay>();
    }
    public void StartTransition()
    {
        StartCoroutine(transition());
    }
   private IEnumerator transition()
    {
        transitionsAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(0.25f);

        soundPlay.PlaySound(0);
    }
}
