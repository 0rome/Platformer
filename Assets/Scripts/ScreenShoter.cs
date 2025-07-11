using UnityEngine;

public class ScreenShoter : MonoBehaviour
{
    [SerializeField] private string fileName;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            ScreenCapture.CaptureScreenshot(fileName + ".png");
            Debug.Log("Скриншот сделан!");
        }
    }
}