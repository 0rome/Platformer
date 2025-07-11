using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DissolveUI : MonoBehaviour
{
    public float dissolveSpeed = 1.0f;
    public float noiseScale = 10f;

    [ColorUsage(true, true)]
    public Color color;

    private float dissolveAmount = 0f;
    private Material material;
    private Image image;
    private SoundPlay soundPlay;

    
    void Start()
    {
        soundPlay = GetComponent<SoundPlay>();

        image = GetComponent<Image>();

        material = Instantiate(image.material); // Создаём копию материала

        image.material = material;

        dissolveAmount = material.GetFloat("_DissolveAmount");

        material.SetColor("_OutlineColor", color);
        material.SetFloat("_NoiseScale", noiseScale);
    }
    
    public void Appear()
    {
        soundPlay.PlaySound(0);
        StartCoroutine(DissolveCoroutine(0.0f)); // Восстановление
    }
    public void Disappear()
    {
        StartCoroutine(DissolveCoroutine(1.0f)); // Растворение
    }
    public void VerticalAppear()
    {
        StartCoroutine(VerticalDissolveCoroutine(0.0f)); // вертикальное восстановление
    }
    public void VerticalDisappear()
    {
        StartCoroutine(VerticalDissolveCoroutine(1.1f)); // вертикальное растворение
    }

    IEnumerator DissolveCoroutine(float target)
    {
        while (!Mathf.Approximately(dissolveAmount, target))
        {
            dissolveAmount = Mathf.MoveTowards(dissolveAmount, target, dissolveSpeed * Time.deltaTime);

            // Проверяем, поддерживает ли материал этот параметр
            if (material.HasProperty("_DissolveAmount"))
            {
                material.SetFloat("_DissolveAmount", dissolveAmount);
            }

            yield return null;
        }
    }
    IEnumerator VerticalDissolveCoroutine(float target)
    {
        while (!Mathf.Approximately(dissolveAmount, target))
        {
            dissolveAmount = Mathf.MoveTowards(dissolveAmount, target, dissolveSpeed * Time.deltaTime);

            // Проверяем, поддерживает ли материал этот параметр
            if (material.HasProperty("_VerticalDissolve"))
            {
                material.SetFloat("_VerticalDissolve", dissolveAmount);
            }

            yield return null;
        }
    }
    void OnDestroy()
    {
        // Освобождаем материал, чтобы не было утечек памяти
        Destroy(material);
    }
}
