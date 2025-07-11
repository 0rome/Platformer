using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MenuController : MonoBehaviour
{
    public List<GameObject> Planets = new List<GameObject>(); 

    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void NextPlanet(int planetIndex)
    {
        StartCoroutine(nextPlanet(planetIndex));
    }
    private IEnumerator nextPlanet(int planetIndex)
    {
        Planets[planetIndex - 1].GetComponent<Animator>().SetTrigger("End");

        yield return new WaitForSeconds(0.5f);

        Planets[planetIndex].SetActive(true);
        Planets[planetIndex].GetComponent<Animator>().SetTrigger("Start");
    }
    public void PreviousPlanet(int planetIndex)
    {
        StartCoroutine (previousPlanet(planetIndex));
    }
    private IEnumerator previousPlanet(int planetIndex)
    {
        Planets[planetIndex - 1].GetComponent<Animator>().SetTrigger("End");

        yield return new WaitForSeconds(0.5f);

        Planets[planetIndex - 2].GetComponent<Animator>().SetTrigger("Start");
    }


    public void OpenLevelsMenu()
    {
        animator.SetTrigger("OpenLevels");
    }
    public void OpenMainMenu()
    {
        animator.SetTrigger("CloseLevels");
    }
    public void OpenSettingsMenu()
    {
        animator.SetTrigger("OpenSettings");
    }
    public void CloseSettingsMenu()
    {
        animator.SetTrigger("CloseSettings");
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }
}
