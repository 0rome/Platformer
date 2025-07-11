using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public static class PostProcessingEffects
{
    private static Volume volume;
    private static LensDistortion lensDistortion;
    private static ColorAdjustments colorAdjustments;

    private static MonoBehaviour coroutineRunner;

    public static void Initialize(Volume volumeReference, MonoBehaviour runner)
    {
        volume = volumeReference;
        coroutineRunner = runner;

        if (volume.profile.TryGet(out lensDistortion))
        {
            Debug.Log("Lens Distortion найден!");
        }
        if (volume.profile.TryGet(out colorAdjustments))
        {
            Debug.Log("Color Adjustment найден!");
        }
    }

    public static void StartLensDistortionAnimation(float minValue, float maxValue, float speed)
    {
        if (coroutineRunner == null)
        {
            Debug.LogError("PostProcessingEffects не инициализирован!");
            return;
        }
        coroutineRunner.StartCoroutine(AnimateLensDistortion(minValue, maxValue, speed));
    }
    public static void StartColorAdjustmentAnimation(float minValue, float maxValue, float speed)
    {
        if (coroutineRunner == null)
        {
            Debug.LogError("PostProcessingEffects не инициализирован!");
            return;
        }
        coroutineRunner.StartCoroutine(AnimateColorAdjustment(minValue,maxValue,speed));
    }

    private static IEnumerator AnimateLensDistortion(float minValue, float maxValue, float speed)
    {
        if (lensDistortion == null) yield break;

        float duration = 1f / speed; // ¬рем€ анимации
        float t = 0f;

        // јнимаци€ от начального до максимального значени€
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            lensDistortion.intensity.value = Mathf.Lerp(minValue, maxValue, t);
            yield return null;
        }

        // јнимаци€ обратно к начальному значению
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            lensDistortion.intensity.value = Mathf.Lerp(maxValue, minValue, t);
            yield return null;
        }
    }
    private static IEnumerator AnimateColorAdjustment(float minValue, float maxValue, float speed)
    {
        if (colorAdjustments == null) yield break;

        float duration = 1f / speed; // ¬рем€ анимации
        float t = 0f;

        // јнимаци€ от начального до максимального значени€
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            colorAdjustments.contrast.value = Mathf.Lerp(minValue, maxValue, t);
            colorAdjustments.saturation.value = Mathf.Lerp(minValue, -maxValue, t);

            yield return null;
        }

        // јнимаци€ обратно к начальному значению
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            colorAdjustments.contrast.value = Mathf.Lerp(maxValue, minValue, t);
            colorAdjustments.saturation.value = Mathf.Lerp(-maxValue, minValue, t);
            yield return null;
        }
    }
}
