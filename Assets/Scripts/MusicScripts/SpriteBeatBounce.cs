using UnityEngine;

public class SpriteBeatBounce : MonoBehaviour
{
    [Header("Музыка")]
    [SerializeField] private AudioSource musicSource; // общий AudioSource на сцене

    [Header("Эффект")]
    [SerializeField] private float intensity = 10f;   // сила увеличения
    [SerializeField] private float smoothSpeed = 10f; // плавность
    [SerializeField] private int sampleSize = 64;     // размер выборки

    private float[] samples;
    private Vector3 baseScale;

    void Start()
    {
        if (musicSource == null)
        {
            Debug.LogError("SpriteBeatBounce: назначь общий AudioSource с музыкой!");
            enabled = false;
            return;
        }

        samples = new float[sampleSize];
        baseScale = transform.localScale;
    }

    void Update()
    {
        // Берём данные из AudioSource
        musicSource.GetOutputData(samples, 0);

        float sum = 0f;
        for (int i = 0; i < samples.Length; i++)
            sum += samples[i] * samples[i];

        float rms = Mathf.Sqrt(sum / samples.Length); // громкость (амплитуда)

        // Считаем масштаб
        float scale = 1f + rms * intensity;

        // Плавно применяем
        Vector3 targetScale = baseScale * scale;
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * smoothSpeed);
    }
}
