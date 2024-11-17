using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public float health = 100f;
    public Text healthText;
    public LogicController logicController;

    public void Start() {
        UpdateHealthText();
    }

    public void TakeDamage(float Damage) {
        health -= Damage;
        Debug.Log("Player health: " + health);

        UpdateHealthText();

        if (health <= 0) {
            Die();
        }
    }

    private void Die() {
        Debug.Log("Player is no more.");
        logicController.GameOver();
    }

    private void UpdateHealthText() {
        healthText.text = health.ToString();
    }
}
