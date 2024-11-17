using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public NavMeshAgent agent; // Reference to the NavMeshAgent component
    public float attackRange = 2.0f; // Distance to attack the player
    public float attackCooldown = 1.5f; // Time between attacks
    public PlayerHealthController playerHealth;
    private float lastAttackTime;

    private Animator enemyAnimator;

    void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform; // Ensure the player has a "Player" tag
            playerHealth = player.GetComponent<PlayerHealthController>();
        }

        enemyAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        // Move towards the player
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // If within attack range, stop moving and attack
            if (distanceToPlayer <= attackRange)
            {
                agent.isStopped = true;
                // enemyAnimator.SetBool("IsWalking", false);

                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    AttackPlayer();
                    lastAttackTime = Time.time;
                }
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);
            }
        }
    }

    void AttackPlayer()
    {
        // Logic for attacking the player
        Debug.Log("Enemy attacks the player!");

        enemyAnimator.SetTrigger("Attack");

        // For example, reduce player's health here
        PlayerHealthController playerHealth = player.GetComponent<PlayerHealthController>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(5);
        }
    }
}
