using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public float parallaxFactor; // Коэффициент сдвига (чем меньше, тем ближе к камере)
    private Transform cam;
    private Vector3 lastCamPos;

    void Start()
    {
        cam = Camera.main.transform;
        lastCamPos = cam.position;
    }

    void LateUpdate()
    {
        Vector3 deltaMovement = cam.position - lastCamPos;
        transform.position += new Vector3(deltaMovement.x * parallaxFactor, 0, 0);
        lastCamPos = cam.position;
    }
}
