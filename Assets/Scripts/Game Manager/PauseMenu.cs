using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public PlayerController playerController;

    private bool isPaused = false;

    private PlayerHealth health;

    private void Start()
    {
        health = playerController.gameObject.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !health.IsGameOver())
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        playerController.setPause(false);
        // Optional: Unlock cursor for gameplay
        //Cursor.lockState = CursorLockMode.Locked;
       // Cursor.visible = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        playerController.setPause(true);
        // Optional: Unlock cursor for UI
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Reset time before reloading
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        //Debug.Log("Quitting game...");
        Application.Quit();
    }
}
