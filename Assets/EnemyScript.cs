using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float entityHealth = 100f;
    public Animator enemyAnimator;
    private bool isDying = false;
    public ScoreManager scoreManager;

    private Renderer healthSphere;
    void Start() {
        // enemyAnimator = GetComponent<Animator>();
        GameObject scoreManaGameObj = GameObject.Find("ScoreController");
        scoreManager = scoreManaGameObj.GetComponent<ScoreManager>();

        Transform sphereTransform = transform.Find("Sphere");
        healthSphere = sphereTransform.GetComponent<Renderer>();

        if (enemyAnimator == null) {
            Debug.LogWarning("enemyAnimator is NULL.");
        }
    }

    void Update()
    {
        if (entityHealth <= 0 && !isDying) {
            EntityDying();
            // StartCoroutine(PlayDeathAnimation());
        }
    }

    public void EntityDying() {
        scoreManager.AddScore(10);
        //Test
        enemyAnimator.SetBool("Dead", true);
        // Destroy(gameObject, enemyAnimator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

    public void TakeDamage(float damageAmount) {
        entityHealth -= damageAmount;
        Debug.Log("Entity Health: " + entityHealth);

        if (enemyAnimator != null) {
            // enemyAnimator.Rebind();
            enemyAnimator.SetTrigger("Damage");
            Debug.Log("Trigger set.");
        }

        
        if (entityHealth <= 40) {
            healthSphere.material.color = Color.red;
            return;
        }

        if (entityHealth <= 80) {
            healthSphere.material.color = Color.yellow;
            return;
        }
    }
}
