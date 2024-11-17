using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicController : MonoBehaviour
{
    public GameObject gamePauseScreen;
    public GameObject scoreScreen;
    public GameObject mainScreen;
    public GameObject gameOverScreen;
    public Text finalScoreText;
    public ScoreManager scoreManager;
    private bool skipStartScreen = false;
    private bool gamePaused = false;
    private bool mainScreenActive = true;
    private bool gameOverScreenActive = false;
    void Start() {
        if (!skipStartScreen) {
            Time.timeScale = 0; // Pause the game
            mainScreen.SetActive(true);
        }
    }

    public void GameOver() {
        Time.timeScale = 0;
        finalScoreText.text = scoreManager.GetScore().ToString();
        scoreScreen.SetActive(false);
        gamePauseScreen.SetActive(false);
        mainScreen.SetActive(false);
        mainScreenActive = false;
        gameOverScreen.SetActive(true);
        gameOverScreenActive = true;
    }

    public void StartGame() {
        Time.timeScale = 1;
        mainScreen.SetActive(false);
        mainScreenActive = false;
        scoreScreen.SetActive(true);
    }

    public void ResumeGame() {
        Time.timeScale = 1;
        gamePauseScreen.SetActive(false);
        scoreScreen.SetActive(true);
    }

    public void PauseGame() {
        Time.timeScale = 0;
        scoreScreen.SetActive(false);
        gamePauseScreen.SetActive(true);
    }

    public void QuitGame() {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void RestartGame() {
        skipStartScreen = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameOverScreen.SetActive(false);
        gameOverScreenActive = false;
    }

    void Update() {
        // Pause/Resume the game when escape is pressed
        if (!mainScreenActive && Input.GetKeyDown(KeyCode.Escape)) {
            if (gamePaused) {
                ResumeGame();
                gamePaused = false;
            } else {
                PauseGame();
                gamePaused = true;
            }
        }

        if (mainScreenActive && Input.GetKeyDown(KeyCode.Return)) {
            StartGame();
        }

        if ((gamePaused || mainScreenActive) && Input.GetKeyDown(KeyCode.Q)) {
            QuitGame();
        }

        if (gameOverScreenActive && Input.GetKeyDown(KeyCode.M)) {
            RestartGame();
        }
    }
}
