using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GradeSelector : MonoBehaviour
{
    [SerializeField] private Button lessButton;
    [SerializeField] private Button moreButton;
    [SerializeField] private TextMeshProUGUI gradeText;

    [SerializeField] private int minGrade = 3;
    [SerializeField] private int maxGrade = 12;
    [SerializeField] private int currentGrade = 3;

    public int CurrentGrade => currentGrade;

    public event Action<int> OnGradeChanged;

    private void Awake()
    {
        lessButton.onClick.AddListener(PreviousGrade);
        moreButton.onClick.AddListener(NextGrade);

        MasterSettings.selectedGrade = currentGrade;
        UpdateDisplay();
    }

    private void PreviousGrade()
    {
        currentGrade--;

        if (currentGrade < minGrade)
            currentGrade = maxGrade;

        GradeChanged();
    }

    private void NextGrade()
    {
        currentGrade++;

        if (currentGrade > maxGrade)
            currentGrade = minGrade;

        GradeChanged();
    }

    private void GradeChanged()
    {
        UpdateDisplay();
        OnGradeChanged?.Invoke(currentGrade);

        MasterSettings.selectedGrade = currentGrade;
    }

    private void UpdateDisplay()
    {
      gradeText.text = currentGrade.ToString();
    }
}