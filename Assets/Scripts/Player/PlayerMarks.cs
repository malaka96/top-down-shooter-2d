using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMarks : MonoBehaviour
{
    public static PlayerMarks Instance;
    public int currentMarks = 0;
    public TMP_Text marksText;

    void Awake()
    {
        Instance = this;
    }

    public void AddMarks(int amount)
    {
        currentMarks += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (marksText != null)
            marksText.text = "Marks: " + currentMarks;
    }
}
