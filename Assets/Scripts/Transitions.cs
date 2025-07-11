using UnityEngine;
using System.Collections;

public class Transitions : MonoBehaviour
{
    [SerializeField] private DissolveUI disolvedObj;
    [SerializeField] private Animator transitionsAnimator;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTransition()
    {
        StartCoroutine(enumerator());
    }
    IEnumerator enumerator()
    {
        transitionsAnimator.SetTrigger("Start");
        
        yield return new WaitForSeconds(.5f);

        disolvedObj.Appear();

        yield return new WaitForSeconds(1f);

        disolvedObj.Disappear();
    }
}
