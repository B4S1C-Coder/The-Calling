using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float entityHealth = 100f;
    
    void Update()
    {
        if (entityHealth <= 0) {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damageAmount) {
        entityHealth -= damageAmount;
    }
}
