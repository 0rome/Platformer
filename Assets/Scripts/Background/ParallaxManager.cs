using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ParallaxManager : MonoBehaviour
{
    public List<GameObject> backgrounds; // Список всех фонов
    private int currentBackgroundIndex = 0;
    public float fadeDuration = 1f; // Время плавного перехода

    void Start()
    {
        SetBackground(currentBackgroundIndex);
    }

    public void ChangeBackground(int index)
    {
        if (index >= 0 && index < backgrounds.Count && index != currentBackgroundIndex)
        {
            StartCoroutine(FadeBackgrounds(index));
        }
    }

    private IEnumerator FadeBackgrounds(int newIndex)
    {
        GameObject oldBackground = backgrounds[currentBackgroundIndex];
        GameObject newBackground = backgrounds[newIndex];

        newBackground.SetActive(true);
        yield return StartCoroutine(FadeIn(newBackground));

        yield return StartCoroutine(FadeOut(oldBackground));
        oldBackground.SetActive(false);

        currentBackgroundIndex = newIndex;
    }

    private IEnumerator FadeIn(GameObject obj)
    {
        SpriteRenderer[] renderers = obj.GetComponentsInChildren<SpriteRenderer>();
        Light2D[] lights = obj.GetComponentsInChildren<Light2D>();

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = elapsedTime / fadeDuration;
            SetAlphaForChildren(renderers, alpha);
            SetIntensityForLights(lights, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SetAlphaForChildren(renderers, 1f);
        SetIntensityForLights(lights, 1f);
    }

    private IEnumerator FadeOut(GameObject obj)
    {
        SpriteRenderer[] renderers = obj.GetComponentsInChildren<SpriteRenderer>();
        Light2D[] lights = obj.GetComponentsInChildren<Light2D>();

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = 1f - (elapsedTime / fadeDuration);
            SetAlphaForChildren(renderers, alpha);
            SetIntensityForLights(lights, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SetAlphaForChildren(renderers, 0f);
        SetIntensityForLights(lights, 0f);
    }

    private void SetAlphaForChildren(SpriteRenderer[] renderers, float alpha)
    {
        foreach (SpriteRenderer sr in renderers)
        {
            Color col = sr.color;
            col.a = alpha;
            sr.color = col;
        }
    }

    private void SetIntensityForLights(Light2D[] lights, float intensity)
    {
        foreach (Light2D light in lights)
        {
            light.intensity = intensity;
        }
    }

    private void SetBackground(int index)
    {
        for (int i = 0; i < backgrounds.Count; i++)
        {
            bool isActive = (i == index);
            backgrounds[i].SetActive(isActive);

            SpriteRenderer[] renderers = backgrounds[i].GetComponentsInChildren<SpriteRenderer>();
            Light2D[] lights = backgrounds[i].GetComponentsInChildren<Light2D>();

            SetAlphaForChildren(renderers, isActive ? 1f : 0f);
            SetIntensityForLights(lights, isActive ? 1f : 0f);
        }
    }
}
