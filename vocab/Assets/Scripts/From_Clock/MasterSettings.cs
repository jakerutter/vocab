using UnityEngine;

public static class MasterSettings
{
    // General game settings
    public static bool includeLowerGrades = false;
    public static int selectedGrade = 3;

    public enum TextColor
    {
        White,
        Black,
        Red,
        Blue,
        Green,
        Yellow,
        Orange,
        Purple
    }

    public static TextColor textColor = TextColor.White;

    public static Color GetTextColor()
    {
        return textColor switch
        {
            TextColor.White => Color.white,
            TextColor.Black => Color.black,
            TextColor.Red => Color.red,
            TextColor.Blue => Color.blue,
            TextColor.Green => Color.green,
            TextColor.Yellow => Color.yellow,
            TextColor.Orange => new Color(1f, 0.5f, 0f, 1f),
            TextColor.Purple => new Color(0.5f, 0f, 0.5f, 1f),
            _ => Color.white
        };
    }

    public static void Save()
    {
        PlayerPrefs.SetInt(
            "SelectedGrade",
            selectedGrade
        );

        PlayerPrefs.SetInt(
            "IncludeLowerGrades",
            includeLowerGrades ? 1 : 0
        );

        PlayerPrefs.SetInt(
            "TextColor",
            (int)textColor
        );

        PlayerPrefs.Save();
    }

    public static void Load()
    {
        selectedGrade =
            PlayerPrefs.GetInt("SelectedGrade", 3);

        includeLowerGrades =
            PlayerPrefs.GetInt("IncludeLowerGrades", 0) == 1;

        textColor =
            (TextColor)PlayerPrefs.GetInt(
                "TextColor",
                (int)TextColor.White
            );
    }
}