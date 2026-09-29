using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ApplySettings : MonoBehaviour
{
    public TextMeshProUGUI[] questionTextObjects;

    private Text coloredText;

    void Start()
    {
        for (int i = 0; i < questionTextObjects.Length; i++)
        {
            coloredText = questionTextObjects[i].GetComponent<Text>();

            if (coloredText == null)
            {
                Debug.LogError("No text component found on the TextMeshProUGUI!");
                return;
            }

            ApplyColorToText();
        }

        //TO DO handle including the lower grades as well?
    }

    public void ApplyColorToText()
    {
        coloredText.color = EnsureOpaque(MasterSettings.textColor);
    }

    private Color EnsureOpaque(Color color)
    {
        if (color.a < 1f) // Ensure alpha is fully opaque
        {
            color.a = 1f;
        }
        return color;
    }
}
