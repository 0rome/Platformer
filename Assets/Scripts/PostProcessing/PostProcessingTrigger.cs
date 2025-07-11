using UnityEngine;

public class PostProcessingTrigger : MonoBehaviour
{
    public float minValue;
    public float maxValue;
    public float Speed;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PostProcessingEffects.StartLensDistortionAnimation(minValue, maxValue, Speed);
        }
    }
}
