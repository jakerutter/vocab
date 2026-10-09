using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    [Header("Toggles")]
    [SerializeField] private Toggle includeLowerGradesToggle;

    [Header("Dropdowns")]
    [SerializeField] private TMP_Dropdown textColorDropdown;

    private void Start()
    {
        PlayerPrefs.DeleteKey("TextColor");
        
        // Initialize UI from current settings without firing callbacks
        includeLowerGradesToggle.SetIsOnWithoutNotify(
            MasterSettings.includeLowerGrades
        );

        textColorDropdown.SetValueWithoutNotify(
            (int)MasterSettings.textColor
        );

        textColorDropdown.RefreshShownValue();

        // Add listeners after initialization
        includeLowerGradesToggle.onValueChanged.AddListener(
            OnIncludeLowerGradesToggleChanged
        );

        textColorDropdown.onValueChanged.AddListener(
            OnTextColorChanged
        );
    }

    private void OnIncludeLowerGradesToggleChanged(bool value)
    {
        MasterSettings.includeLowerGrades = value;
    }

    private void OnTextColorChanged(int value)
    {
        MasterSettings.textColor =
            (MasterSettings.TextColor)value;
    }
}