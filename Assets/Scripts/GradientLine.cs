using System;
using UnityEngine;

public class GradientLine : MonoBehaviour
{
    public float widthLine = 0.1f;
    public GameObject prefabsLine;
    public Transform parentLines;
    public Color color1, color2;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        CreateLines();
    }

    float Interpolate(float a, float b, float t)
    {
        return a * t + b * (1f - t);
    }

    Color InterpolateColor(Color c1, Color c2, float t)
    {
        return new Color(Interpolate(c1.r, c2.r, t), Interpolate(c1.g, c2.g, t), Interpolate(c1.b, c2.b, t));
    }

    void CreateLines()
    {
        // Розрахунок висоти та ширини екрана
        float screenHeight = cam.orthographicSize * 2f;
        float screenWidth = screenHeight * cam.aspect;

        // Визначення кількості ліній, що створюються
        int linesCount = Mathf.CeilToInt(screenHeight / widthLine);  // Кількість ліній на основі висоти (відстань між лініями - 0.5f)

        float hHalft = screenHeight / 2f;
        float h0 = -hHalft / 2;

        float gradImpact = 1f / (linesCount - 1);

        for (int i = 0; i < linesCount; i++)
        {
            float yPosition = h0 + widthLine * i / 2;
            float y1Position = yPosition + widthLine;

            GameObject objLine = Instantiate(prefabsLine, new Vector3(0, yPosition, 0), Quaternion.identity);
            objLine.transform.SetParent(parentLines, false);

            LineRenderer line = objLine.GetComponent<LineRenderer>();
            if (line == null)
            {
                Debug.LogError("LineRenderer not found in prefab!");
                return;
            }

            float thickLineWidth = screenWidth;
            line.startWidth = thickLineWidth;
            line.endWidth = thickLineWidth;

            Gradient gradient = new Gradient();
            GradientColorKey[] colorKey = new GradientColorKey[2];
            GradientAlphaKey[] alphaKey = new GradientAlphaKey[2];

            float grad = i * gradImpact;
            float gradNext = (i + 1) * gradImpact;

            Color c1 = InterpolateColor(color1, color2, grad);
            Color c2 = InterpolateColor(color1, color2, gradNext);

            colorKey[0] = new GradientColorKey(c1, 0f); 
            colorKey[1] = new GradientColorKey(c2, 1f); 

            alphaKey[0] = new GradientAlphaKey(1f, 0f); 
            alphaKey[1] = new GradientAlphaKey(1f, 1f); 

            gradient.SetKeys(colorKey, alphaKey);
            line.colorGradient = gradient;

         
            line.positionCount = 2;
            line.SetPosition(0, new Vector3(0, yPosition, 0));
            line.SetPosition(1, new Vector3(0, y1Position, 0));
        }
    }
}
