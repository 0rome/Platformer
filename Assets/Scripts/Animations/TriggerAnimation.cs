using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class TriggerAnimation : MonoBehaviour
{
    [SerializeField] private float animationSpeed;

    [SerializeField] private Vector3 positionValue;
    [SerializeField] private Vector3 scaleValue;
    [SerializeField] private Vector3 rotationValue;

    private GameObject animatedObject; // Должен быть родительским объектом триггера со скриптом

    
    void Start()
    {
        animatedObject = transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
             StartAnimation();
        }
       
    }

    private void StartAnimation()
    {

        if (positionValue != new Vector3(0, 0, 0))
        {
            animatedObject.transform.DOMove(positionValue, animationSpeed);
        }
        if (scaleValue != new Vector3(0, 0, 0))
        {
            animatedObject.transform.DOScale(scaleValue, animationSpeed);
        }
        if (rotationValue != new Vector3(0, 0, 0))
        {
            animatedObject.transform.DORotate(positionValue, animationSpeed);
        }

        Destroy(gameObject);
    }
}
