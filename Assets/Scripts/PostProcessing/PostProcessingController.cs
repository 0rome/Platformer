using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingController : MonoBehaviour
{
    public Volume volume; // ������ �� Volume � �����

    private Bloom bloom;
    private Vignette vignette;
    private ColorAdjustments colorAdjustments;

    void Start()
    {
        if (volume.profile.TryGet(out bloom))
        {
            Debug.Log("Bloom ������!");
        }

        if (volume.profile.TryGet(out vignette))
        {
            Debug.Log("Vignette �������!");
        }

        if (volume.profile.TryGet(out colorAdjustments))
        {
            Debug.Log("Color Adjustments ������!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            bloom.intensity.value = bloom.intensity.value > 0 ? 0 : 10;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            vignette.intensity.value = vignette.intensity.value > 0 ? 0 : 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            colorAdjustments.saturation.value = colorAdjustments.saturation.value > 0 ? -100 : 0;
        }
    }
}
