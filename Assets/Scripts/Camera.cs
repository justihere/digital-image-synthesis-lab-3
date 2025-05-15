using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cam;

    void Start()
    {
       
        if (cam == null)
        {
            Debug.LogError("Camers does not find!");
            return;
        }

        if (!cam.orthographic)
        {
            Debug.LogError("Camera is not on ortographic rejym!");
            return;
        }

        // Настраиваем правильные размеры
        float height = cam.orthographicSize * 2f;  // Полная высота экрана
        float width = height * cam.aspect;        // Полная ширина экрана
        Debug.Log($"Size of gamingdisplay (width: {width}, height: {height})");

        // Устанавливаем правильное положение камеры
        cam.transform.position = new Vector3(0, 0, -10);
    }
}
