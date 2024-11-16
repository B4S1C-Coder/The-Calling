using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float entityHealth = 100f;
    public Animator enemyAnimator;
    private bool isDying = false;
    public ScoreManager scoreManager;
    void Start() {
        // enemyAnimator = GetComponent<Animator>();

        if (enemyAnimator == null) {
            Debug.LogWarning("enemyAnimator is NULL.");
        }
    }

    void Update()
    {
        if (entityHealth <= 0 && !isDying) {
            // enemyAnimator.SetBool("isDead", true);
            // Destroy(gameObject);

            StartCoroutine(PlayDeathAnimation());
        }
    }

    public void TakeDamage(float damageAmount) {
        entityHealth -= damageAmount;

        if (enemyAnimator != null) {
            enemyAnimator.Rebind();
            enemyAnimator.SetTrigger("Damage");
            Debug.Log("Trigger set.");
        }
    }

    private IEnumerator PlayDeathAnimation() {
        isDying = true;
        enemyAnimator.SetTrigger("Death");

        while (enemyAnimator.IsInTransition(0) || enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Creep|Death_Action")) {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        scoreManager.AddScore(10);
        Destroy(gameObject);
    }
}
