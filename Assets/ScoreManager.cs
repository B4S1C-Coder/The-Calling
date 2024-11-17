using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    private int score = 0;
    void Start() {
        if (scoreText == null) {
            GameObject scoreObject = GameObject.FindGameObjectWithTag("Score");
            if (scoreObject != null) {
                scoreText = scoreObject.GetComponent<Text>();
            }

            if (scoreText == null) {
                Debug.LogError("No Text component found with the 'Score' tag. Make sure a UI Text object exists in the scene with the correct tag.");
            }
        }
        
        UpdateScoreText();
    }

    public int GetScore() {
        return score;
    }

    public void AddScore(int points) {
        score += points;
        UpdateScoreText();
    }

    public void ResetScore() {
        score = 0;
        UpdateScoreText();
    }

    private void UpdateScoreText() {
        scoreText.text = score.ToString();
    }
}
