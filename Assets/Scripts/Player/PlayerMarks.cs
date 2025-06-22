using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMarks : MonoBehaviour
{
    public static PlayerMarks Instance;

    public int currentMarks = 0;
    public TMP_Text marksText;
    public TMP_Text highScoreText;

    private int highScore = 0;

    public AudioClip diamondSfx;
    public AudioClip healthBoxSfx;
    private AudioSource audioSource;

    void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();

        // Load high score from PlayerPrefs
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateUI();
    }

    public void AddMarks(int amount)
    {
        currentMarks += amount;

        if (currentMarks > highScore)
        {
            highScore = currentMarks;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        if (marksText != null)
            marksText.text = "Score: " + currentMarks;

        if (highScoreText != null)
            highScoreText.text = "High Score: " + highScore;
    }

    public void ResetMarks()
    {
        currentMarks = 0;
        UpdateUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("diamond"))
        {
            audioSource.PlayOneShot(diamondSfx);
        }
        if (collision.CompareTag("HealthBox"))
        {
            audioSource.PlayOneShot(healthBoxSfx);
        }
    }
}
