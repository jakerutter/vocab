using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TMPro namespace

public class SettingsManager : MonoBehaviour
{
    // Toggles
    public Toggle includeLowerGradesToggle;
    // TextMeshPro Dropdowns
    public TMP_Dropdown textColorDropdown;

    void Start()
    {
        // Initialize toggles
        includeLowerGradesToggle.isOn = MasterSettings.includeLowerGrades;

        // Add listeners to save changes
        includeLowerGradesToggle.onValueChanged.AddListener(OnIncludeLowerGradesToggleChanged);
    
        textColorDropdown.onValueChanged.AddListener(OnTextColorChanged);
    }

    public void ApplySettings()
    {
            //to do? fetch the active text color and apply it to text before displaying -- or update the tmpro element once
            // hoursTargetImage = hourHandsRectTransform.GetComponent<Image>();

            // if (hoursTargetImage == null)
            // {
            //     Debug.LogError("No Image component found on the target hours RectTransform!");
            //     return;
            // }

            // ApplyColorToHoursRectTransform();
    }


    // Toggle change listeners
    private void OnIncludeLowerGradesToggleChanged(bool value)
    {
        MasterSettings.includeLowerGrades = value;
    }

    // TMP_Dropdown change listeners
    private void OnTextColorChanged(int value)
    {
        MasterSettings.textColor = DropdownIndexToColor(value);

        ApplySettings();
    }

    // Convert TMP_Dropdown index to Color
    private Color DropdownIndexToColor(int index)
    {
        switch (index)
        {
            case 0: return new Color(0, 0, 0, 1); // Black
            case 1: return new Color(1, 1, 1, 1); // White
            case 2: return new Color(1, 0, 0, 1); // Red
            case 3: return new Color(0, 0, 1, 1); // Blue
            case 4: return new Color(0, 1, 0, 1); // Green
            case 5: return new Color(1, 1, 0, 1); // Yellow
            case 6: return new Color(1, 0.5f, 0, 1); // Orange
            case 7: return new Color(0.5f, 0, 0.5f, 1); // Purple
            default: return new Color(0, 0, 0, 1); // Default to Black
        }
    }

    // Convert Color to TMP_Dropdown index
    private int ColorToDropdownIndex(Color color)
    {
        if (color == Color.black) return 0; // "Black"
        if (color == Color.white) return 1; // "White"
        if (color == Color.red) return 2;   // "Red"
        if (color == Color.blue) return 3; // "Blue"
        if (color == Color.green) return 4; // "Green"
        if (color == Color.yellow) return 5;   // "Yellow"
        if (ApproximatelyEqual(color, new Color(1, 0.5f, 0, 1))) return 6; // "Orange"
        if (ApproximatelyEqual(color, new Color(0.5f, 0, 0.5f, 1))) return 7; // "Purple"
        return 0; // Default to "Black"
    }

    private bool ApproximatelyEqual(Color a, Color b, float tolerance = 0.01f)
    {
        return Mathf.Abs(a.r - b.r) < tolerance &&
            Mathf.Abs(a.g - b.g) < tolerance &&
            Mathf.Abs(a.b - b.b) < tolerance &&
            Mathf.Abs(a.a - b.a) < tolerance;
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
